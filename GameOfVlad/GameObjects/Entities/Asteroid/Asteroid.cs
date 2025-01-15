using System;
using System.Collections.Generic;
using GameOfVlad.GameObjects.Effects;
using GameOfVlad.GameObjects.Entities.Interfaces;
using GameOfVlad.GameObjects.Entities.Player;
using GameOfVlad.GameObjects.Interfaces;
using GameOfVlad.GameRenderer;
using GameOfVlad.Utils;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;

namespace GameOfVlad.GameObjects.Entities.Asteroid;

internal partial class Asteroid(ContentManager contentManager, IEffectDrawer effectDrawer, Rectangle levelBounds)
    : HealthGameObject, IHealth
{
    public int DrawOrder => (int)DrawOrderType.Effect;
    public int UpdateOrder => 1;

    public override IEnumerable<IRendererObject> ChildrenBefore
    {
        get { yield return _fireConstantEffectParticleGenerator; }
        set => throw new NotSupportedException();
    }

    public Vector2 Velocity { get; init; }
    public float RotationSpeed { get; init; }
    

    private const int SpawnOffset = 2000;

    protected override void LoadCore()
    {
        InitEffects();

        base.LoadCore();
    }

    public override void Update(GameTime gameTime)
    {
        this.Position += this.Velocity * (float)gameTime.ElapsedGameTime.TotalSeconds;
        this.Rotation += this.RotationSpeed * (float)gameTime.ElapsedGameTime.TotalSeconds;

        bool needToDestroy = LevelHelper.IsPositionBehindLevel(this.Position, levelBounds, SpawnOffset);

        if (needToDestroy)
        {
            Destroy();
        }

        base.Update(gameTime);
    }

    public void OnCollision(IColliderGameObject other)
    {

    }

    public void OnCollisionEnter(IColliderGameObject other)
    {
        if (other is PlayerV2 player)
        {
            player.TakeDamage(amount: DamageStorage.MeteoriteDamage);
            
            PlayDestroyEffect();
            Destroy();
        }
    }

    public void OnCollisionExit(IColliderGameObject other)
    {

    }

    protected override void OnZeroHP()
    {
        PlayDestroyEffect();
        
        base.OnZeroHP();
    }
}
