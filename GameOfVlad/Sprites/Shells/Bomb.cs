using GameOfVlad.Game.Levels;
using GameOfVlad.GameEffects;
using GameOfVlad.Sprites.Guns;
using GameOfVlad.Sprites.Mobs;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;


namespace GameOfVlad.Sprites.Shells
{
    public class Bomb : Shell
    {
        public float Distance;
        public bool Explosion;
        public bool ThisMine = false;

        public ParticleEngine Effect;
        public Vector2 DLocation { get { return new Vector2(Location.X + Size.Width / 2, Location.Y + Size.Height / 2); }}

        int TTK;
        public int ttk = 50;

        public Bomb(ContentManager content, Texture2D texture, Level level, Mob parent) 
            : base (content, texture, level, parent)
        {
            Explosion = false;
            TTK = 0;
        }

        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            if (Explosion)
            {
                Effect.Draw(spriteBatch);
            }
            else
            {
                base.Draw(gameTime, spriteBatch);
            }
        }

        public override void Update(GameTime gameTime)
        {
            if (Explosion)
            {
                Effect.EmitterLocation = DLocation;
                Effect.Update(gameTime);
                TTK++;
                if (TTK > ttk)
                {
                    Dead = true;
                    Effect.ClearEffect();
                }
            }
        }

        public override bool WasHit(ColldesSprite mob)
        {
            foreach (var shell in mob.BombHit)
                if (shell == this)
                    return false;

            if (Parent != mob || !ThisMine)
            {
                var hit = (mob.Location - DLocation).Length() < Distance;
                if (Explosion && hit)
                {
                    mob.BombHit.Add(this);
                    mob.WasShot(Damage);
                    return true;
                }
                else if (hit && ThisMine)
                {
                    Explosion = true;
                }
            }
            return false;
        }

        public override void Create(Vector2 direction)
        {

        }
    }
}
