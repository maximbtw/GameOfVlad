using System;
using System.Collections.Generic;
using GameOfVlad.GameRenderer;
using GameOfVlad.Utils;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace GameOfVlad.GameObjects;

public abstract class GameObject
{
    public Guid Guid { get; } = Guid.NewGuid();

    public virtual IRendererObject Parent { get; set; } = null;

    public virtual IEnumerable<IRendererObject> Children { get; set; } = [];

    public virtual bool Destroyed { get; set; } = false;
    public virtual bool IsActive { get; set; } = true;
    public virtual bool Visible { get; set; } = true;
    public Texture2D Texture { get; set; }
    public Vector2 Position { get; set; } = Vector2.Zero;
    public Color Color { get; set; } = Color.White;
    public float Rotation { get; set; } = 0f;
    public Vector2 Scale { get; set; } = Vector2.One;
    public SpriteEffects SpriteEffects { get; set; } = SpriteEffects.None;
    public float LayerDepth { get; set; } = 0f;
    public virtual Vector2 Origin => new(Size.Width / 2f, Size.Height / 2f);

    public virtual Size Size
    {
        get => _size ?? new Size(Texture?.Width ?? 0, Texture?.Height ?? 0);
        set => _size = value;
    }

    public virtual Vector2 DrawPosition => this.Position + this.Origin;
    
    public virtual bool Loaded => _loaded;

    public virtual Rectangle? SourceRectangle => null;

    private Size? _size;
    private bool _loaded;

    public void Load()
    {
        if (_loaded)
        {
            throw new Exception("Object is already loaded");
        }

        LoadCore();
        
        _loaded = true;
    }

    public void Unload()
    {
        UnloadCore();
        
        _loaded = false;
    }

    public virtual void Draw(GameTime gameTime, SpriteBatch spriteBatch)
    {
        spriteBatch.Draw(
            this.Texture,
            this.DrawPosition,
            this.SourceRectangle,
            this.Color,
            this.Rotation,
            this.Origin,
            this.Scale,
            this.SpriteEffects,
            this.LayerDepth
        );
    }

    public virtual void Update(GameTime gameTime)
    {
    }
    
    protected virtual void LoadCore()
    {
    }

    protected virtual void UnloadCore()
    {
    }
}