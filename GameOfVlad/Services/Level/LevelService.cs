using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using GameOfVlad.Game;
using GameOfVlad.Services.Graphic;
using Microsoft.Xna.Framework;

namespace GameOfVlad.Services.Level;

public class LevelService(IServiceProvider serviceProvider) : ILevelService
{
    private readonly Dictionary<LevelType, ILevel> _levelIndex = new();
    
    private LevelType _currentLevel;
    
    public void LoadLevelsToCache()
    {
        _levelIndex.Clear();
        
        var assembly = Assembly.GetExecutingAssembly();
        foreach (Type type in assembly.GetTypes())
        {
            bool isLevel = type.GetInterfaces().Contains(typeof(ILevel));
            if (isLevel)
            {
                var level = (ILevel)Activator.CreateInstance(type, serviceProvider);

                LevelType levelType = level!.LevelType;
                
                _levelIndex.Add(levelType, level);
            }
        }
    }
    
    public void SetLevel(LevelType levelType)
    {
        _currentLevel = levelType;
    }

    public ILevel GetCurrentLevel()
    {
        if (_levelIndex.TryGetValue(_currentLevel, out ILevel level))
        {
            return level;
        }
        
        throw new KeyNotFoundException($"The level type {_currentLevel} was not found.");
    }
}