using System.Collections.Generic;
using GameOfVlad.Entities.Interfaces;
using GameOfVlad.Game.Physics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace GameOfVlad.GameRenderer.Modificators;

public class PhysicRendererModificator : IRendererModificator
{
    public Gravity Gravity { get; set; }


    private readonly PhysicsV2 _physics = new();

    public void Update(IGameObject gameObject, GameTime gameTime)
    {
        if (gameObject is IPhysicalGameObject obj)
        {
            var forces = new List<ForceDelegate>();

            if (obj is ITrustForcePhysicalGameObject trustForcedGameObject)
            {
                ForceDelegate thrustForce = Force.CreateThrustForce(trustForcedGameObject);
                forces.Add(thrustForce);
            }

            if (this.Gravity != null)
            {
                ForceDelegate gravityForce = Force.CreateGravityForce(Gravity.Center, Gravity.Strength);
                forces.Add(gravityForce);
            }

            ForceDelegate combinedForces = Force.Combine(forces);

            _physics.ApplyForces(obj, combinedForces, gameTime);
        }
    }

    public void Draw(IGameObject gameObject, GameTime gameTime, SpriteBatch spriteBatch)
    {
        
    }
}