using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace GameOfVlad.GameObjects.UI;

public abstract class UiComponentBase(IServiceProvider serviceProvider) : IDisposable
{
    public IGameObject Parent { get; set; }
    public virtual IEnumerable<IGameObject> Children { get; set; }
    public bool IsActive { get; set; } = true;
    public bool Destroyed { get; set; } = false;
    public Vector2 Position { get; set; }
    public Texture2D Texture { get; set; }
    public Color Color { get; set; } = Color.White;

    public virtual Rectangle BoundingBox => this.Texture == null
        ? Rectangle.Empty
        : new Rectangle((int)Position.X, (int)Position.Y, Texture.Width, Texture.Height);

    protected readonly IServiceProvider ServiceProvider = serviceProvider;
    
    public virtual void Init(ContentManager content)
    {
    }

    public virtual void Terminate()
    {
        this.Texture.Dispose();
    }
    
    public virtual void Update(GameTime gameTime)
    {
    }
    
    public virtual void Draw(GameTime gameTime, SpriteBatch spriteBatch)
    {
        spriteBatch.Draw(this.Texture, this.BoundingBox, this.Color);
    }

    public void Dispose()
    {
        Terminate();
    }
}