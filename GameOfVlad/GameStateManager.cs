using System;
using GameOfVlad.Scenes;
using GameOfVlad.Services.Graphic;
using GameOfVlad.Services.Scene;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace GameOfVlad;

public class GameStateManager(ISceneService sceneService, IGraphicService graphicService)
{
    private IScene _currentScene;

    public void Update(GameTime gameTime)
    {
        IScene newScene = sceneService.GetCurrentScene();
        if (newScene != _currentScene)
        {
            SwitchScene(newScene);
        }
        
        _currentScene.Update(gameTime);
    }
    
    public void Draw(GameTime gameTime, SpriteBatch spriteBatch)
    {
        _currentScene.Draw(gameTime, spriteBatch);
    }

    private void SwitchScene(IScene scene)
    {
        _currentScene?.Terminate();
        
        ContentManager content = graphicService.GetContentManager();
        
        content.Unload();
        
        _currentScene = scene;
        
        _currentScene.Init(content);
    }
}