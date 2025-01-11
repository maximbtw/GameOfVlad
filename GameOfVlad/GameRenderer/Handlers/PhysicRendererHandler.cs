using System.Collections.Generic;
using GameOfVlad.Game.Physics;
using GameOfVlad.GameObjects.Entities.Interfaces;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace GameOfVlad.GameRenderer.Handlers;

public class PhysicRendererHandler : BaseRendererObjectHandler<IPhysicalGameObject>, IRendererObjectHandler
{
    public Gravity Gravity { get; set; }
    public float DragCoefficient { get; set; } = 0.1f;
    

    private readonly PhysicsV2 _physics = new();

    protected override void UpdateCore(GameTime gameTime, IPhysicalGameObject obj)
    {
        var forces = new List<ForceDelegate>();

        if (obj is ITrustForcePhysicalGameObject trustForcedGameObject)
        {
            ForceDelegate thrustForce = Force.CreateThrustForce(trustForcedGameObject);
            ForceDelegate dragForce = Force.CreateDragForce(trustForcedGameObject, this.DragCoefficient);
            forces.Add(thrustForce);
            forces.Add(dragForce);
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