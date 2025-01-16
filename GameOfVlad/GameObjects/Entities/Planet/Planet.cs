using System;
using GameOfVlad.GameObjects.Entities.Player;
using GameOfVlad.GameObjects.Interfaces;
using GameOfVlad.Utils;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;

namespace GameOfVlad.GameObjects.Entities.Planet;

public class Planet(ContentManager contentManager, PlanetType type) : ColliderGameObject, IColliderGameObject
{
    public override float LayerDepth => (float)DrawOrderType.BackgroundEntity / 100f;
    public int UpdateOrder => 1;

    public event Action OnPlayerCollisionWithPlanet;

    protected override Size ColliderSize => new(this.Texture.Width * 0.5f, this.Texture.Height * 0.5f);
    
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