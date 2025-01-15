using System;
using GameOfVlad.GameObjects.Entities.Player;
using GameOfVlad.GameObjects.Interfaces;
using GameOfVlad.Utils;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;

namespace GameOfVlad.GameObjects.Entities.Planet;

public class Planet(ContentManager contentManager, PlanetType type) : ColliderGameObject, IColliderGameObject
{
    public int DrawOrder => (int)DrawOrderType.BackgroundEntity;
    public int UpdateOrder => 1;

    public event Action OnPlayerCollisionWithPlanet;

    public override Size Size
    {
        get => new(Texture?.Width / 2 ?? 0, Texture?.Height / 2 ?? 0);
        set => throw new NotSupportedException();
    }
    
    private PlanetAnimation _planetAnimation;

    protected override void LoadCore()
    {
        _planetAnimation = PlanetAnimation.CreateAnimation(contentManager, this, type);
        base.LoadCore();
    }
    
    public override void Update(GameTime gameTime)
    {
        _planetAnimation.Update(gameTime);
        
        base.Update(gameTime);
    }

    public override Vector2[] GetCorners()
    {
        var position  = new Vector2(this.Position.X + this.Size.Width /2, this.Position.Y + this.Size.Height /2);
        
        Vector2 topLeft = position;
        Vector2 topRight = position + new Vector2(this.Size.Width, 0);
        Vector2 bottomLeft = position + new Vector2(0, this.Size.Height);
        Vector2 bottomRight = position + new Vector2(this.Size.Width, this.Size.Height);

        Vector2 center = position + this.Origin;

        return
        [
            RotatePoint(topLeft, center, Rotation),
            RotatePoint(topRight, center, Rotation),
            RotatePoint(bottomRight, center, Rotation),
            RotatePoint(bottomLeft, center, Rotation)
        ];
    }

    public void OnCollision(IColliderGameObject other)
    {
    }

    public void OnCollisionEnter(IColliderGameObject other)
    {
        if (other is PlayerV2)
        {
            OnPlayerCollisionWithPlanet?.Invoke();   
        }
    }

    public void OnCollisionExit(IColliderGameObject other)
    {
    }
}