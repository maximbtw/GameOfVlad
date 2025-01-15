using GameOfVlad.GameObjects.Effects.Generators.ParticleGeneration;
using GameOfVlad.Utils;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Color = Microsoft.Xna.Framework.Color;

namespace GameOfVlad.GameObjects.Entities.Asteroid;

internal partial class Asteroid
{
    private ParticleGenerator _fireConstantEffectParticleGenerator;

    private void InitEffects()
    {
        var conf = new ParticleGeneratorConfiguration
        {
            CanProduceParticle = () => true,
            GetSpawnPosition = () => this.DrawPosition,
            GetDirection = () => Vector2.Normalize(this.Velocity) * -1,
            SpawnRate = 25,
            ParticleLifetime = 5,
            SpeedRange = Range<int>.Create(0, 100),
            OffsetAngleRange = Range<int>.Create(-10, 10),
            Colors = [Color.White, Color.Orange,],
            ScaleRange = Range<float>.Create(0.8f * this.Scale.X, 1f * this.Scale.X), // Как парвило x == y
            RotationSpeedRange = Range<int>.Create(-20, 20),
            Textures =
            [
                contentManager.Load<Texture2D>("Sprite/Meteorit/ParticleEffect1"),
                contentManager.Load<Texture2D>("Sprite/Meteorit/ParticleEffect2"),
                contentManager.Load<Texture2D>("Sprite/Meteorit/ParticleEffect3")
            ]
        };

        _fireConstantEffectParticleGenerator = new ParticleGenerator(effectDrawer, conf)
        {
            Parent = this
        };
    }

    private void PlayDestroyEffect()
    {
        Vector2 position = this.DrawPosition;
        
        var conf = new ParticleGeneratorConfiguration
        {
            GetSpawnPosition = () => position,
            GetDirection = () => Vector2.One,
            SpawnRate = 100,
            ParticleLifetime = 2f,
            SpeedRange = Range<int>.Create(200, 200),
            OffsetAngleRange = Range<int>.Create(-180, 180),
            Colors = [Color.White, Color.Gray, Color.DarkRed, Color.White],
            ScaleRange = Range<float>.Create(0.8f * this.Scale.X, 1f * this.Scale.X), // Как парвило x == y
            RotationSpeedRange = Range<int>.Create(-20, 20),
            Textures =
            [
                contentManager.Load<Texture2D>("Sprite/Meteorit/ParticleEffect1"),
                contentManager.Load<Texture2D>("Sprite/Meteorit/ParticleEffect2"),
                contentManager.Load<Texture2D>("Sprite/Meteorit/ParticleEffect3")
            ]
        };
        
        var temporaryParticleGenerator = new TemporaryParticleGenerator(effectDrawer, conf, generationTime: 0.075f);
        
        effectDrawer.AddEffect(temporaryParticleGenerator);
    }
}