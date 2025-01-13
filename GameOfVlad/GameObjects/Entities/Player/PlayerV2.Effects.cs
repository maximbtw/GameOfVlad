using GameOfVlad.GameObjects.Effects.Generators;
using GameOfVlad.GameObjects.Effects.Generators.ParticleGeneration;
using GameOfVlad.Utils;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace GameOfVlad.GameObjects.Entities.Player;

public partial class PlayerV2
{
    private ParticleGenerator _trustPowerParticleGenerator;

    private void CreateParticleEffects()
    {
        _trustPowerParticleGenerator = new ParticleGenerator(
            effectDrawer,
            new ParticleGeneratorConfiguration
            {
                CanProduceParticle = () => _trustPowerActive,
                GetSpawnPosition = () =>
                {
                    Vector2[] corners = this.GetCorners();

                    Vector2 bottomLeft = corners[0];
                    Vector2 bottomRight = corners[3];

                    return new Vector2((bottomLeft.X + bottomRight.X) / 2, (bottomLeft.Y + bottomRight.Y) / 2);
                },
                GetDirection = () => GameHelper.GetDirectionByRotationInRadians(this.Rotation) * -1,
                SpawnRate = 50,
                ParticleLifetime = 10,
                SpeedRange = Range<int>.Create(200, 500),
                OffsetAngleRange = Range<int>.Create(-45, 45),
                Colors =
                [
                    Color.OrangeRed,
                    Color.OrangeRed,
                    Color.OrangeRed,
                    Color.White,
                    Color.DarkRed,
                    Color.DarkRed,
                    Color.DarkRed,
                ],
                Textures =
                [
                    contentManager.Load<Texture2D>("Sprite/Meteorit/ParticleEffect1"),
                    contentManager.Load<Texture2D>("Sprite/Meteorit/ParticleEffect2"),
                    contentManager.Load<Texture2D>("Sprite/Meteorit/ParticleEffect3")
                ]
            })
        {
            Parent = this
        };
    }
}