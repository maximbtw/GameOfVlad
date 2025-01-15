using System;
using System.Collections.Generic;
using GameOfVlad.Utils;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace GameOfVlad.GameObjects.Effects.Generators.ParticleGeneration;

public class ParticleGeneratorConfiguration
{
    public Func<Vector2> GetSpawnPosition { get; init; }
    
    public Func<bool> CanProduceParticle { get; init; } = () => true;
    
    public Func<Vector2> GetDirection { get; init; } = () => Vector2.One;
    
    public float SpawnRate { get; init; } = 10;
    
    public float ParticleLifetime { get; init; } = 10;
    
    public List<Color> Colors { get; init; } = [Color.White];

    public Range<int> OffsetAngleRange { get; init; } = Range<int>.Create(-20, 20);
    
    public Range<int> SpeedRange { get; init; } = Range<int>.Create(10, 100);
    
    public Range<int> RotationSpeedRange { get; init; } = Range<int>.Create(-5, 5);
    
    public Range<float> ScaleRange { get; init; } = Range<float>.Create(0.5f, 1.5f);

    public Texture2D[] Textures { get; init; }
}