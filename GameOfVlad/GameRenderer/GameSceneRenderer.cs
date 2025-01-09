using System;
using System.Collections.Generic;
using GameOfVlad.Scenes;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace GameOfVlad.GameRenderer;

public class GameSceneRenderer(ContentManager contentManager)
{
    private readonly Stack<IScene> _scenes = new();

    public void Update(GameTime gameTime)
    {
        IScene currentScene = _scenes.Peek();
        
        currentScene.Update(gameTime);
    }
    
    public void Draw(GameTime gameTime, SpriteBatch spriteBatch)
    {
        IScene currentScene = _scenes.Peek();
        
        currentScene.Draw(gameTime, spriteBatch);
    }

    public void PushScene(IScene scene)
    {
        if (_scenes.TryPeek(out IScene currentScene))
        {
            if (currentScene.Type == scene.Type)
            {
                PopScene();
            }
        }
        
        scene.Load(contentManager);
        _scenes.Push(scene);
    }

    public void PopScene()
    {
        if (_scenes.Count <= 1)
        {
            throw new InvalidOperationException("There is no scene to load");
        }
        
        IScene currentScene = _scenes.Pop();
        currentScene.Unload();
    }
}