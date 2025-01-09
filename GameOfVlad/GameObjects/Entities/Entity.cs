using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace GameOfVlad.GameObjects.Entities;

public abstract class Entity(ContentManager contentManager) : IDisposable
{
    public IGameObject Parent { get; set; }
    public IEnumerable<IGameObject> Children { get; set; }
    public bool IsActive { get; set; } = true;
    public bool Destroyed { get; set; } = false;
    public Texture2D Texture { get; set; }
    public Vector2 Position { get; set; }
    public Color Color { get; set; } = Color.White;
    
    public Guid Guid => _guid;
    
    protected readonly ContentManager ContentManager = contentManager;
    
    private readonly Guid _guid = Guid.NewGuid();
    
    public virtual void Init()
    {
    }

    public virtual void Terminate()
    {
       Texture?.Dispose();
    }
    
    public virtual void Update(GameTime gameTime)
    {
    }

    public virtual void Draw(GameTime gameTime, SpriteBatch spriteBatch)
    {
        spriteBatch.Draw(this.Texture, this.Position, this.Color);
    }

    public void Dispose()
    {
        Terminate();
    }
    
    
}