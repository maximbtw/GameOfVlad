using GameOfVlad.GameObjects.Effects.Generators.ParticleGeneration;
using GameOfVlad.Utils;
using GameOfVlad.Utils.GameObject;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace GameOfVlad.GameObjects.Entities.Enemies.Forkfighter;

public partial class Forkfighter
{
    private TextureAnimation<Forkfighter> _movementAnimation;

    private void InitEffects()
    {
        _movementAnimation = new TextureAnimation<Forkfighter>(
            gameObject: this, 
            textures: [
                contentManager.Load<Texture2D>("2025/Sprites/Game/Enemies/Forkfighter/enemy-forkfighter-01-125x137"),
                contentManager.Load<Texture2D>("2025/Sprites/Game/Enemies/Forkfighter/enemy-forkfighter-02-125x137"),
                contentManager.Load<Texture2D>("2025/Sprites/Game/Enemies/Forkfighter/enemy-forkfighter-03-125x137"),
                contentManager.Load<Texture2D>("2025/Sprites/Game/Enemies/Forkfighter/enemy-forkfighter-04-125x137"),
                contentManager.Load<Texture2D>("2025/Sprites/Game/Enemies/Forkfighter/enemy-forkfighter-05-125x137"),
                contentManager.Load<Texture2D>("2025/Sprites/Game/Enemies/Forkfighter/enemy-forkfighter-06-125x137"),
                contentManager.Load<Texture2D>("2025/Sprites/Game/Enemies/Forkfighter/enemy-forkfighter-07-125x137"),
                contentManager.Load<Texture2D>("2025/Sprites/Game/Enemies/Forkfighter/enemy-forkfighter-08-125x137"),
            ], 
            timePerFrame: 0.1f);
    }

    private void PlayAttackEffect()
    {
        var conf = new ParticleGeneratorConfiguration
        {
            GetSpawnPosition = () => this.CenterPosition,
            GetDirection = () => GameHelper.CalculateDirection(this.CenterPosition, _player.CenterPosition),
            SpawnRate = 10,
            ParticleLifetime = 1f,
            SpeedRange = Range<int>.Create(500, 600),
            OffsetAngleRange = Range<int>.Create(-20, 20),
            ScaleRange = Range<float>.Create(1f, 2f),
            RotationSpeedRange = Range<int>.Create(0, 0),
            Textures =
            [
                contentManager.Load<Texture2D>("2025/Sprites/Game/Enemies/Forkfighter/attack-particle-forkfighter-01-35x14")
            ]
        };


        var attackGenerator = new TemporaryParticleGenerator(effectDrawer, conf, 0.2f);
        
        effectDrawer.AddEffect(attackGenerator);
    }
}