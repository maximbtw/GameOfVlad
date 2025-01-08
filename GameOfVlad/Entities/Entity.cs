using System;
using System.Collections.Generic;
using System.ComponentModel;
using GameOfVlad.Utils;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace GameOfVlad.Entities;

public abstract class Entity(IServiceProvider serviceProvider) : IGameObject
{
    private Size? _size;

    protected readonly IServiceProvider ServiceProvider = serviceProvider;
    
    public IGameObject Parent { get; set; }
    public IEnumerable<IGameObject> Children { get; set; }
    public bool IsActive { get; set; } = true;

    public Texture2D Texture { get; set; } 
    public Vector2 Position { get; set; } = Vector2.Zero;

    public Size Size
    {
        get => _size ?? new Size(this.Texture.Width, this.Texture.Height);
        set => _size = value;
    }

    public Color Color { get; set; } = Color.White;

    public virtual int UpdateOrder { get; }
    public virtual int DrawOrder { get; }

    public void Update(GameTime gameTime)
    {
        UpdateCore(gameTime);
    }

    public void Draw(GameTime gameTime, SpriteBatch spriteBatch)
    {
        DrawCore(gameTime, spriteBatch);
    }

    public void Init(ContentManager content)
    {
        InitCore(content);
    }

    public void Terminate()
    {
        TerminateCore();
    }

    protected virtual void DrawCore(GameTime gameTime, SpriteBatch spriteBatch)
    {
        spriteBatch.Draw(this.Texture, this.Position, this.Color);
    }

    protected virtual void UpdateCore(GameTime gameTime)
    {
    }

    protected virtual void InitCore(ContentManager content)
    {
    }

    protected virtual void TerminateCore()
    {
    }
}