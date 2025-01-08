 using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using GameOfVlad.GameEffects;
using System;
using GameOfVlad.Game.Levels;
using GameOfVlad.Sprites.Mobs;
using GameOfVlad.Sprites.Mobs.Boss;
using GameOfVlad.Utils;

namespace GameOfVlad.Sprites.Shells
{
    public class Bullet : Shell
    {
        public ParticleEngine Effect;
        public bool Hit = false;

        public Bullet(ContentManager content,Texture2D texture, Level level, Mob parent)
                      : base(content, texture, level, parent)
        {
            Damage = 1;
            HPBar = 1;
            Speed = 10f;
            Color = Color.White;
            Origin = new Vector2(texture.Width / 2, texture.Height / 2);
        }

        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(Texture, Location, null, Color, Rotation, Origin, 1, SpriteEffects.None, 0f);
            if (Effect != null)
                Effect.Draw(spriteBatch);
        }

        public override void Update(GameTime gameTime)
        {
            if (Homing)
            {
                var point = (Parent is Player) ? Level.MousePosition : Level.Player.Location;
                Direction = point - Location;
                if (Direction != Vector2.Zero)
                    Direction.Normalize();
                Rotation = (float)Math.Atan2(Direction.Y, Direction.X);
            }

            if (HPBar<1 || 
                Location.X < -750 || 
                Location.Y < -750 ||
                Location.X > Level.LevelSize.Width + 750 || 
                Location.Y > Level.LevelSize.Height + 750)
                Dead = true;

            Location += Speed * Direction;

            if (Effect != null)
            {
                Effect.EmitterLocation = Location;
                Effect.Update(gameTime);
            }
        }

        public override bool WasHit(ColldesSprite mob)
        {
            var sprite = mob;
            if(mob is Player)
            {
                var player = (Player)mob;
                if (player.Shield.Activ)
                {
                    sprite = player.Shield._Shield;
                }
            }

            if (Parent != sprite && Collides.Collide(this, sprite))
            {
                Dead = true;
                var effect = Level.HitBulletEffect.Clone() as HitBulletEffect;
                effect.EmitterLocation = Location;
                Level.EffectsBulletHit.Add(effect);

                if(this.Parent is Observer && sprite is SunFish)
                    sprite.WasShot(-Damage);
                else
                    sprite.WasShot(Damage);
                return true;
            }
            return false;
        }

        public override void Create(Vector2 direction)
        {

        }
    }
}
