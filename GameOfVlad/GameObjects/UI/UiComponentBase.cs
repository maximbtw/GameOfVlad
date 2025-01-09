using System;
using System.Collections.Generic;
using GameOfVlad.GameObjects.UI.Interfaces;
using GameOfVlad.Services.Camera;
using GameOfVlad.Utils.Draw;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace GameOfVlad.GameObjects.UI;

public abstract class UiComponentBase : IDisposable
{
    public IGameObject Parent { get; set; }
    public virtual IEnumerable<IGameObject> Children { get; set; }
    public bool IsActive { get; set; } = true;
    public bool Destroyed { get; set; } = false;
    public Vector2 Position { get; set; }
    public Texture2D Texture { get; set; }
    public Color Color { get; set; } = Color.White;

    public Guid Guid { get; } = Guid.NewGuid();

    public virtual Vector2 CameraPosition
    {
        get
        {
            if (this.Parent != null)
            {
                Vector2 parentPosition = ((IUiComponent)this.Parent).CameraPosition;

                return new Vector2(parentPosition.X + this.Position.X, parentPosition.Y + this.Position.Y);
            }

            return CameraService.PositionByCamera(this.Position);
        }
    }

    public virtual Rectangle BoundingBox => this.Texture == null
        ? Rectangle.Empty
        : new Rectangle((int)CameraPosition.X, (int)CameraPosition.Y, Texture.Width, Texture.Height);

    protected readonly ContentManager ContentManager;
    
    protected ICameraService CameraService => this.ContentManager.ServiceProvider.GetRequiredService<ICameraService>();
    private readonly UiColliderDrawer _drawCollider;

    protected UiComponentBase(ContentManager contentManager)
    {
        this.ContentManager = contentManager;
        
        _drawCollider = new UiColliderDrawer((IUiComponent)this);
    }

    public virtual void Init()
    {
    }

    public virtual void Terminate()
    {
        _drawCollider.Dispose();
    }
    
    public virtual void Update(GameTime gameTime)
    {
    }
    
    public virtual void Draw(GameTime gameTime, SpriteBatch spriteBatch)
    {
        if (Settings.ShowCollider)
        {
            _drawCollider.DrawCollider(spriteBatch);
        }
        
        spriteBatch.Draw(this.Texture, this.CameraPosition, this.Color);
    }

    public void Dispose()
    {
        Terminate();
    }
}