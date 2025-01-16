using System;
using GameOfVlad.Utils;
using GameOfVlad.Utils.GameObject;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace GameOfVlad.Game.WeaponSystem.Projectiles;

public class DartcasterProjectile(ContentManager contentManager) : ProjectileBase(contentManager), IProjectile
{
    public Vector2 Velocity { get; set; }

    private const float LifeTime  = 3f;

    private TextureAnimation<DartcasterProjectile> _animation;
    
    private readonly Timer _timer = new();

    protected override void LoadCore()
    {
        this.Texture = LoadProjectileTexture("dartcaster-projectile-01-24x24");
        this.Color = Color.GreenYellow;

        _animation = new TextureAnimation<DartcasterProjectile>(
            gameObject: this,
            textures:
            [
                LoadProjectileTexture("dartcaster-projectile-01-24x24"),
                LoadProjectileTexture("dartcaster-projectile-02-24x24"),
                LoadProjectileTexture("dartcaster-projectile-03-24x24"),
                LoadProjectileTexture("dartcaster-projectile-04-24x24"),
                LoadProjectileTexture("dartcaster-projectile-05-24x24"),
                LoadProjectileTexture("dartcaster-projectile-06-24x24"),
                LoadProjectileTexture("dartcaster-projectile-07-24x24"),
                LoadProjectileTexture("dartcaster-projectile-08-24x24"),
            ],
            timePerFrame: 0.1f);

        base.LoadCore();
    }

    public override void Update(GameTime gameTime)
    {
        UpdateMovement(gameTime);
        
        _timer.Update(gameTime);
        if (_timer.Time >= LifeTime)
        {
            Destroy();
        }
        
        base.Update(gameTime);
    }

    public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
    {
        UpdateAppearance(gameTime);
        
        base.Draw(gameTime, spriteBatch);
    }

    private void UpdateMovement(GameTime gameTime)
    {
        // Параметры движения
        const float gravity = 9.8f; // Ускорение свободного падения (м/с²)
        const float gravityScale = 100f; // Масштаб гравитации (настраивается под вашу игровую систему)

        // Преобразуем время кадра в секунды
        float deltaTime = (float)gameTime.ElapsedGameTime.TotalSeconds;

        this.Velocity = new Vector2(this.Velocity.X, this.Velocity.Y + gravity * gravityScale * deltaTime);

        this.Position += this.Velocity * deltaTime;
        this.Rotation = MathF.Atan2(this.Velocity.Y, this.Velocity.X);
    }

    protected override void OnHit()
    {

    }

    private void UpdateAppearance(GameTime gameTime)
    {
        _animation.Update(gameTime);
    }

    private Texture2D LoadProjectileTexture(string textureName)
    {
        return ContentManager.Load<Texture2D>($"2025/Sprites/Game/Weapons/Projectiles/Dartcaster/{textureName}");
    }
}