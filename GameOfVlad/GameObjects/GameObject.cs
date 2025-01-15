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

    public virtual IRendererObject Parent { get; set; }

    public virtual IEnumerable<IRendererObject> ChildrenAfter  { get; set; } = [];
    public virtual IEnumerable<IRendererObject> ChildrenBefore  { get; set; } = [];

    public virtual bool Destroyed { get; set; } 
    public virtual bool IsActive { get; set; } = true;
    public virtual bool Visible { get; set; } = true;
    public Texture2D Texture { get; set; }
    public Vector2 Position { get; set; } = Vector2.Zero;
    public Color Color { get; set; } = Color.White;
    public float Rotation { get; set; } 
    public Vector2 Scale { get; set; } = Vector2.One;
    public SpriteEffects SpriteEffects { get; set; } = SpriteEffects.None;
    public float LayerDepth { get; set; } = 0f;
    
    public virtual Vector2 Origin => new(this.Size.Width * 0.5f, this.Size.Height * 0.5f);
    public virtual Size Size => new(this.Texture?.Width ?? 0, this.Texture?.Height ?? 0);
    public virtual Vector2 CenterPosition => this.Position + this.Origin;
    public virtual bool Loaded => _loaded;

    public virtual Rectangle? SourceRectangle => null;
    
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
            this.CenterPosition,
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
    
    public virtual void Destroy()
    {
        this.Destroyed = true;
    }
    
    protected virtual void LoadCore()
    {
    }

    protected virtual void UnloadCore()
    {
    }
    
    public virtual bool OnDestroyCompleted(GameTime gameTime)
    {
        return true;
    }

    public virtual void OnDestroyDraw(GameTime gameTime, SpriteBatch spriteBatch)
    {
    }
}