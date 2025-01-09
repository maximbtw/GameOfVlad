using System.Collections.Generic;
using GameOfVlad.GameObjects;
using GameOfVlad.GameRenderer;
using GameOfVlad.Scenes.Game;
using GameOfVlad.Utils;
using Microsoft.Xna.Framework.Content;

namespace GameOfVlad.Game;

public interface ILevel
{
    Size LevelSize { get; }
    
    LevelType LevelType { get; }

    void Load();

    void Unload();
    
    IEnumerable<IGameObject> GetGameObjects();
    
    IEnumerable<IGameObjectRendererModificator> GetLevelModificators();

    void GameStateChanged(GameState state);
}