using System;
using System.Collections.Generic;
using GameOfVlad.GameObjects.Effects;
using GameOfVlad.GameRenderer;
using GameOfVlad.Utils;
using GameOfVlad.Utils.GameObject;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace GameOfVlad.GameObjects.Entities.Asteroid;

public class AsteroidGenerator(ContentManager contentManager, IEffectDrawer effectDrawer, Rectangle levelBounds)
    : GameObject, IGameObject
{
    public int DrawOrder => (int)DrawOrderType.Effect;
    public int UpdateOrder => 1;

    public override IEnumerable<IRendererObject> ChildrenAfter
    {
        get => _meteorites;
        set => throw new NotSupportedException();
    }

    public int SpawnFrequency { get; set; } = 75;
    public Range<int> MeteoriteSpeedRange { get; set; } = Range<int>.Create(200, 500);
    public Range<float> MeteoriteScaleRange { get; set; } = Range<float>.Create(1, 1);

    private const int SpawnOffset = 2000;

    private readonly Random _random = new();
    private readonly List<Asteroid> _meteorites = new();

    private readonly Texture2D[] _textures =
    [
        contentManager.Load<Texture2D>("2025/Sprites/Game/Asteroids/asteroid-01-64x64"),
        contentManager.Load<Texture2D>("2025/Sprites/Game/Asteroids/asteroid-02-64x64"),
        contentManager.Load<Texture2D>("2025/Sprites/Game/Asteroids/asteroid-03-64x64"),
        contentManager.Load<Texture2D>("2025/Sprites/Game/Asteroids/asteroid-04-64x64"),
        contentManager.Load<Texture2D>("2025/Sprites/Game/Asteroids/asteroid-05-64x64"),
        contentManager.Load<Texture2D>("2025/Sprites/Game/Asteroids/asteroid-06-64x64")
    ];

    private TickUpdater _spawnUpdater;

    protected override void LoadCore()
    {
        _spawnUpdater = new TickUpdater(this.SpawnFrequency, () =>
        {
            Asteroid star = GenerateMeteorite();
            _meteorites.Add(star);
        });

        base.LoadCore();
    }

    public override void Update(GameTime gameTime)
    {
        _spawnUpdater.Update();

        Console.WriteLine(_meteorites.Count);

        _meteorites.RemoveAll(star => star.Destroyed);

        base.Update(gameTime);
    }

    public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
    {
    }

    private Asteroid GenerateMeteorite()
    {
        Vector2 position = LevelHelper.GeneratePositionBehindLevel(_random, levelBounds, SpawnOffset);

        // Рассчитываем направление внутрь уровня
        var levelCenter =
            // ReSharper disable PossibleLossOfFraction
            new Vector2(levelBounds.X + levelBounds.Width / 2, levelBounds.Y + levelBounds.Height / 2);

        Vector2 directionToCenter = levelCenter - position;
        directionToCenter.Normalize();

        Vector2 rotatedDirection =
            GameHelper.GetOffsetDirectionByRandom(_random, directionToCenter, Range<int>.Create(-5, 5));

        float rotationSpeed = -2f + (float)_random.NextDouble() * (2f - -2f);

        Vector2 scale = Vector2.One * (MeteoriteScaleRange.MinValue + (float)_random.NextDouble() *
            (MeteoriteScaleRange.MaxValue - MeteoriteScaleRange.MinValue));

        int speed = _random.Next(MeteoriteSpeedRange.MinValue, MeteoriteSpeedRange.MaxValue);
        Vector2 velocity = rotatedDirection * speed;

        var meteorite = new Asteroid(contentManager, effectDrawer, levelBounds)
        {
            Texture = _textures[_random.Next(0, _textures.Length)],
            Position = position,
            Velocity = velocity,
            Scale = scale,
            RotationSpeed = rotationSpeed,
            Parent = this,
        };

        return meteorite;
    }
}