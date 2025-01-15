using System.Collections.Generic;
using GameOfVlad.GameObjects;
using GameOfVlad.GameObjects.Effects.Generators;
using GameOfVlad.GameObjects.Entities.Asteroid;
using GameOfVlad.GameObjects.Entities.Enemies.Forkfighter;
using GameOfVlad.GameObjects.Entities.Planet;
using GameOfVlad.GameObjects.Entities.Player;
using GameOfVlad.GameRenderer.Handlers;
using GameOfVlad.Utils;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace GameOfVlad.Game.Levels;

public class Level1(ContentManager contentManager) : LevelBase(contentManager), ILevel
{
    public Rectangle LevelBounds => new(0, 0, 5000, 5000);
    public LevelType LevelType => LevelType.Level1;
    
    private PlayerV2 _player;

    protected override void LoadCore()
    {
        InitPlayer();
        
        RegisterRendererHandlers(
            new PhysicRendererHandler(),
            new LevelBorderRendererHandler(this.LevelBounds),
            new GameObjectCollisionRendererHandler(),
            new PlayerFinderRendererHandler(_player)
        );
    }

    protected override IEnumerable<IGameObject> InitGameObjectsCore()
    {
        yield return new BackgroundGenerator(
            this.EffectDrawer,
            this.ContentManager.Load<Texture2D>("2025/Backgrounds/Game/Starfields/Starfield_04-512x512"), 
            this.LevelBounds);

        yield return new StarfallGenerator(this.ContentManager, this.EffectDrawer, this.LevelBounds);
        
        var planet = new Planet(this.ContentManager, PlanetType.Earth)
        {
            Position = new Vector2(1000, 1000),
        };

        planet.OnPlayerCollisionWithPlanet += OnLevelCompleted;

        yield return planet;
        
        yield return new AsteroidGenerator(this.ContentManager, this.EffectDrawer, this.LevelBounds)
        {
            MeteoriteScaleRange = Range<float>.Create(0.4f, 0.75f)
        };
        
        yield return _player;
        yield return CreateHealthBar(_player);

        yield return new Forkfighter(this.ContentManager, this.EffectDrawer)
        {
            Position = new Vector2(1000, 1000),
        };
    }

    private void InitPlayer()
    {
        var player = new PlayerV2(this.ContentManager, this.EffectDrawer, this.ProjectileDrawer)
        {
            Texture = this.ContentManager.Load<Texture2D>("Sprite/Rocket/Rocket"),
            Position = new Vector2(100, 100),
            TrustPower = 10000,
            Mass = 100,
            MaxHP = 1000
        };

        player.OnPlayerDeath += OnPlayerDead;

        _player = player;
    }
}