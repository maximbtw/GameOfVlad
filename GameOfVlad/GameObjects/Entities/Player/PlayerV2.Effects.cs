using GameOfVlad.GameObjects.UI.Effects;
using GameOfVlad.Utils;
using Microsoft.Xna.Framework;

namespace GameOfVlad.GameObjects.Entities.Player;

public partial class PlayerV2
{
    private ParticleGenerator _trustPowerParticleGenerator;

    private void CreateParticleEffects()
    {
        _trustPowerParticleGenerator = new ParticleGenerator(new ParticleGenerator.Configuration(contentManager)
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
            Colors = [
                Color.OrangeRed, 
                Color.OrangeRed, 
                Color.OrangeRed, 
                Color.White,     
                Color.DarkRed,
                Color.DarkRed,
                Color.DarkRed,
            ]
        })
        {
            Parent = this
        };
    }
}