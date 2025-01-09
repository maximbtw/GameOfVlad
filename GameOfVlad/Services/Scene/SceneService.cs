using System;
using GameOfVlad.GameRenderer;
using GameOfVlad.Scenes;
using GameOfVlad.Scenes.Game;
using GameOfVlad.Scenes.MainMenu;
using GameOfVlad.Scenes.Map;
using GameOfVlad.Services.Camera;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;

namespace GameOfVlad.Services.Scene;

public class SceneService(IServiceProvider serviceProvider, GameSceneRenderer sceneRenderer) : ISceneService
{
    private ICameraService CameraService => serviceProvider.GetService<ICameraService>();
    
    public void PushScene(SceneType sceneType)
    {
        var contentManager = new ContentManager(serviceProvider);
        contentManager.RootDirectory = "Content";
        
        IScene scene = sceneType switch
        {
            SceneType.MainMenu => new MainMenuScene(contentManager),
            SceneType.Map => new MapScene(contentManager),
            SceneType.Game => new GameScene(contentManager),
            SceneType.Gallery => throw new NotImplementedException(),
            SceneType.Settings => throw new NotImplementedException(),
            _ => throw new ArgumentOutOfRangeException(nameof(sceneType), sceneType, null)
        };
     
        CameraService.SetCameraPosition(Vector2.Zero);
        sceneRenderer.PushScene(scene);
    }

    public void PopScene()
    {
        CameraService.SetCameraPosition(Vector2.Zero);
        sceneRenderer.PopScene();
    }
}