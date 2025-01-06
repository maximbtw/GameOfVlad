using System;
using GameOfVlad.Scenes;
using GameOfVlad.Scenes.Game;
using GameOfVlad.Scenes.MainMenu;
using GameOfVlad.Scenes.Map;
using Microsoft.Extensions.DependencyInjection;

namespace GameOfVlad.Services.Scene;

public class SceneService(IServiceProvider serviceProvider) : ISceneService
{
    private IScene _currentScene;
    
    public void SetScene(SceneType sceneType)
    {
        switch (sceneType)
        {
            case SceneType.MainMenu:
                _currentScene = serviceProvider.GetRequiredService<MainMenuScene>();
                break;
            case SceneType.Map:
                _currentScene = serviceProvider.GetRequiredService<MapScene>();
                break;
            case SceneType.Game:
                _currentScene = serviceProvider.GetRequiredService<GameScene>();
                break;
            case SceneType.Gallery:
                break;
            case SceneType.Settings:
                break;
            default:
                throw new ArgumentOutOfRangeException(nameof(sceneType), sceneType, null);
        }
    }

    public IScene GetCurrentScene() => _currentScene;
}