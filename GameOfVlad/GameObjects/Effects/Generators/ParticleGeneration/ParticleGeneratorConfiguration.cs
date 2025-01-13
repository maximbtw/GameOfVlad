using System;
using System.Collections.Generic;
using GameOfVlad.Utils;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace GameOfVlad.GameObjects.Effects.Generators.ParticleGeneration;

public class ParticleGeneratorConfiguration
{
    public Func<Vector2> GetSpawnPosition { get; set; }
    public Func<bool> CanProduceParticle { get; set; } = () => true;
    public Func<Vector2> GetDirection { get; set; } = () => Vector2.Zero;
    public float SpawnRate { get; set; } = 10;
    public float ParticleLifetime { get; set; } = 10;
    public List<Color> Colors { get; set; } = [Color.White];

    public Range<int> OffsetAngleRange { get; set; } = Range<int>.Create(-20, 20);
    public Range<int> SpeedRange { get; set; } = Range<int>.Create(10, 100);
    public Range<int> RotationSpeedRange { get; set; } = Range<int>.Create(-5, 5);
    public Range<float> ScaleRange { get; set; } = Range<float>.Create(0.5f, 1.5f);

    public Texture2D[] Textures { get; set; }
}