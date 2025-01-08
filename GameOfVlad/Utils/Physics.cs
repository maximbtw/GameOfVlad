using System;
using System.Collections.Generic;
using GameOfVlad.Sprites;
using Microsoft.Xna.Framework;

namespace GameOfVlad.Utils
{
    public delegate Vector2 RocketForce(Player rocket);
    public delegate Vector2 Gravity(Size size, Vector2 location);
    public class Forces
    {
        public static RocketForce GetThrustForce(float forceValue)
        {
            return r =>
            {
                return new Vector2((float)(forceValue * (Math.Cos(MathHelper.ToRadians(90) - r.Rotation))),
                    -(float)(forceValue * Math.Sin(MathHelper.ToRadians(90) - r.Rotation)));
            };
        }

        public static RocketForce ConvertGravityToForce(Gravity gravity, Size size)
        {
            return r => gravity(size, r.Location);
        }

        public static RocketForce Sum(params RocketForce[] forces)
        {
            return r =>
            {
                var sum = Vector2.Zero;
                foreach (var force in forces)
                    sum += force(r);
                return sum;
            };
        }
    }

    public class Physics
    {
        private Queue<Vector2> vectors;
        private Vector2 sumVecotrs;
        public Vector2 VelosityRocket { get; private set; }

        private readonly int maxCount = 200;

        private static readonly float mass = 1f;
        private static readonly float maxVelocity = 300f;

        public Physics()
        {
            vectors = new Queue<Vector2>();
            vectors.Clear();
        }

        public void MoveRocket(Player rocket)
        {
            var force = Forces.Sum(Forces.GetThrustForce(1.0f),
                Forces.ConvertGravityToForce(rocket.Level.Gravity, rocket.Level.LevelSize));
            var velocity = GetVelocity(rocket, force);

            vectors.Enqueue(velocity);
            sumVecotrs += velocity;
            if (vectors.Count == maxCount)
                sumVecotrs -= vectors.Dequeue();

            VelosityRocket = sumVecotrs / vectors.Count;
        }

        private Vector2 GetVelocity(Player rocket, RocketForce force)
        {
            var velocity = force(rocket) * 1.5f / mass;
            if (velocity.Length() > maxVelocity && velocity != Vector2.Zero)
                velocity.Normalize();
            return velocity;
        }
    }
}
