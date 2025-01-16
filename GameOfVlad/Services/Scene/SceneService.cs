using System;
using GameOfVlad.Scenes;
using GameOfVlad.Scenes.Game;
using GameOfVlad.Scenes.MainMenu;
using GameOfVlad.Scenes.Map;
using GameOfVlad.Services.Audio;
using GameOfVlad.Services.Camera;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Xna.Framework.Content;

namespace GameOfVlad.Services.Scene;

public class SceneService(IServiceProvider serviceProvider, GameSceneRenderer sceneRenderer) : ISceneService
{
    private ICameraService CameraService => serviceProvider.GetService<ICameraService>();
    
    private IAudioService AudioService => serviceProvider.GetService<IAudioService>();
    
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
        
        this.AudioService.Load(scene);
        this.CameraService.ResetCamera();
        
        sceneRenderer.PushScene(scene);
    }

    public void PopScene()
    {
        this.AudioService.Unload();
        this.CameraService.ResetCamera();
        
        sceneRenderer.PopScene();
    }
}