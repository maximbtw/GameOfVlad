using System;
using System.Collections.Generic;
using GameOfVlad.GameObjects.UI.Interfaces;
using GameOfVlad.GameRenderer;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace GameOfVlad.GameObjects.UI.Effects;

public class StarfallGenerator(ContentManager contentManager, Rectangle levelBounds)
    : UiComponent(contentManager), IUiComponent
{
    public int DrawOrder => (int)DrawOrderType.Background;
    public int UpdateOrder => 1;

    public override IEnumerable<IRendererObject> ChildrenAfter
    {
        get => _stars;
        set => throw new NotSupportedException();
    }
    
    public int SpawnFrequency { get; set; } = 10;
    public int MinStarSpeed { get; set; } = 100;
    public int MaxStarSpeed { get; set; } = 1000;

    private const int SpawnOffset = 1000;

    private readonly Random _random = new();
    private readonly List<Star> _stars = new();
    private readonly Texture2D[] _texture =
    [
        contentManager.Load<Texture2D>("Sprite/Stars/1"),
        contentManager.Load<Texture2D>("Sprite/Stars/2"),
        contentManager.Load<Texture2D>("Sprite/Stars/3"),
        contentManager.Load<Texture2D>("Sprite/Stars/4")
    ];

    private int _tick;

    public override void Update(GameTime gameTime)
    {
        _tick++;
        if (this._tick % SpawnFrequency == 0)
        {
            Star star = GenerateStar();
            _stars.Add(star);
        }

        _stars.RemoveAll(star => star.Destroyed);

        base.Update(gameTime);
    }

    public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
    {
    }

    private Star GenerateStar()
    {
        Vector2 position = GenerateStarPosition();

        // Рассчитываем направление внутрь уровня
        var levelCenter =
            new Vector2(levelBounds.X + levelBounds.Width / 2, levelBounds.Y + levelBounds.Height / 2);
        
        Vector2 directionToCenter = levelCenter - position;
        directionToCenter.Normalize(); // Нормализуем направление к центру

        // Добавляем случайный угол погрешности от -5 до +5 градусов
        float angleOffset = (float)(_random.NextDouble() * 90 - 5); // Угол в градусах
        float radiansOffset = MathF.PI * angleOffset / 180f; // Конвертируем в радианы

        // Применяем поворот вектора направления
        float cos = MathF.Cos(radiansOffset);
        float sin = MathF.Sin(radiansOffset);
        var rotatedDirection = new Vector2(
            directionToCenter.X * cos - directionToCenter.Y * sin,
            directionToCenter.X * sin + directionToCenter.Y * cos
        );
        
        int speed = _random.Next(MinStarSpeed, MaxStarSpeed);
        Vector2 velocity = rotatedDirection * speed;
        
        var star = new Star(this.ContentManager, levelBounds)
        {
            Texture = _texture[_random.Next(0, _texture.Length)],
            Position = position,
            Velocity = velocity,
            Parent = this,
        };

        return star;
    }

    private Vector2 GenerateStarPosition()
    {
        int minX = levelBounds.X - SpawnOffset;
        int maxX = levelBounds.X + levelBounds.Width + SpawnOffset;

        int minY = levelBounds.Y - SpawnOffset;
        int maxY = levelBounds.Y + levelBounds.Height + SpawnOffset;

        int x, y;
        
        var isBoundedByHorizontal = _random.Next(0, 2) == 0;
        if (isBoundedByHorizontal)
        {
            x = _random.Next(0, 2) == 0 ? minX : maxX;
            y = _random.Next(minY, maxY);
        }
        else
        {
            x = _random.Next(minX, maxX);
            y = _random.Next(0, 2) == 0 ? minY : maxY;
        }

        return new Vector2(x, y);
    }

    private class Star(ContentManager contentManager, Rectangle levelBounds) : UiComponent(contentManager), IUiComponent
    {
        public int DrawOrder => (int)DrawOrderType.Background;
        public int UpdateOrder => 1;

        public override Vector2 DrawPosition => this.Position;
        public Vector2 Velocity { get; set; } 

        public override void Update(GameTime gameTime)
        {
            this.Position += this.Velocity * (float)gameTime.ElapsedGameTime.TotalSeconds;

            bool needToDestroy =
                this.Position.X > levelBounds.X + levelBounds.Width + SpawnOffset ||
                this.Position.X < levelBounds.X - SpawnOffset ||
                this.Position.Y > levelBounds.Y + levelBounds.Height + SpawnOffset ||
                this.Position.Y < levelBounds.Y - SpawnOffset;

            if (needToDestroy)
            {
                Destroyed = true;
            }

            base.Update(gameTime);
        }
    }
}