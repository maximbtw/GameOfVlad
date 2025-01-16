using GameOfVlad.GameObjects;
using GameOfVlad.GameObjects.Effects;
using GameOfVlad.Services.Audio;
using GameOfVlad.Utils;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;

namespace GameOfVlad.Game.WeaponSystem;

public abstract class WeaponBase<TProjectile>(
    ContentManager contentManager,
    IEffectDrawer effectDrawer,
    IProjectileDrawer projectileDrawer)
    where TProjectile : IProjectile
{
    public abstract float FireRate { get; set; }

    protected readonly ContentManager ContentManager = contentManager;
    protected readonly IEffectDrawer EffectDrawer = effectDrawer;

    protected IAudioService AudioService => this.ContentManager.ServiceProvider.GetRequiredService<IAudioService>();

    private readonly Timer _fireTimer = new();

    private bool _canFire;

    public void Update(GameTime gameTime)
    {
        _fireTimer.Update(gameTime);
        if (_fireTimer.Time >= this.FireRate)
        {
            _fireTimer.Reset();
            _canFire = true;
        }
    }

    public void Shoot(IGameObject parent, Vector2 destinationPoint)
    {
        if (!_canFire)
        {
            return;
        }

        TProjectile projectile = CreateShot(parent, destinationPoint);

        projectileDrawer.AddProjectile(projectile);

        _fireTimer.Reset();
        _canFire = false;
    }

    protected abstract TProjectile CreateShot(IGameObject parent, Vector2 destinationPoint);
}