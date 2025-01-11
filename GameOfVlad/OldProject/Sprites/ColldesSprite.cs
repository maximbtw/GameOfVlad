using System;
using System.Collections.Generic;
using GameOfVlad.Game.Levels;
using GameOfVlad.OldProject.Sprites.Mobs;
using GameOfVlad.OldProject.Sprites.Shells;
using GameOfVlad.Utils;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace GameOfVlad.OldProject.Sprites
{
    public class ColldesSprite : Sprite
    {
        public Mob Parent;
        public Wall Wall;
        public Vector2 Direction;
        public Vector2 Velocity;
        public float Rotation;
        public float Speed;


        public bool Dead = false;
        public int HPBar;

        public List<Lazer> LazerHit;
        public List<GhostBullet> GhostBulletHit;
        public List<Bomb> BombHit;

        public readonly Color[] TextureData;
        public Rectangle RectangleRotation
        {
            get
            {
                return new Rectangle((int)Location.X - (int)Origin.X, (int)Location.Y -
                    (int)Origin.Y, Texture.Width, Texture.Height);
            }
        }
        public Rectangle Rectangle
        {
            get
            {
                return new Rectangle((int)Location.X, (int)Location.Y, Texture.Width, Texture.Height);
            }
        }
        public Matrix Transform
        {
            get
            {
                return Matrix.CreateTranslation(new Vector3(-Origin, 0)) *
                  Matrix.CreateRotationZ(Rotation) *
                  Matrix.CreateTranslation(new Vector3(Location, 0));
            }
        }
        public Rectangle CollisionArea
        {
            get
            {
                return new Rectangle(RectangleRotation.X, RectangleRotation.Y, (int)MathHelper.Max(RectangleRotation.Width, RectangleRotation.Height),
                    (int)MathHelper.Max(RectangleRotation.Width, RectangleRotation.Height));
            }
        }

        public ColldesSprite(ContentManager content, Texture2D texture, Level level, Mob parent) 
        {
            Content = content;
            Texture = texture;
            Level = level;
            Parent = parent;

            Size = new Size(Texture.Width, Texture.Height);

            TextureData = new Color[Texture.Width * Texture.Height];
            Texture.GetData(TextureData);
        }


        public ColldesSprite(ContentManager content, Texture2D texture, Vector2 location, Level level)
                            : base(content, texture, location, level)
        {
            LazerHit = new List<Lazer>();
            GhostBulletHit = new List<GhostBullet>();
            BombHit = new List<Bomb>();

            TextureData = new Color[Texture.Width * Texture.Height];
            Texture.GetData(TextureData);
        }

        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(Texture, Location, null, Color, Rotation, Origin, 1, SpriteEffects.None, 0f);
        }

        public virtual void WasShot(int damage)
        {
            HPBar -= damage;
            if (HPBar < 1)
                Dead = true;
        }

        public virtual bool WasHit(ColldesSprite mob)
        {
            mob.WasShot(this.HPBar);
            return false;
        }

        public bool Intersects(ColldesSprite sprite)
        {
            var transformAToB = this.Transform * Matrix.Invert(sprite.Transform);
            var stepX = Vector2.TransformNormal(Vector2.UnitX, transformAToB);
            var stepY = Vector2.TransformNormal(Vector2.UnitY, transformAToB);
            var yPosInB = Vector2.Transform(Vector2.Zero, transformAToB);

            for (int yA = 0; yA < this.RectangleRotation.Height; yA++)
            {
                var posInB = yPosInB;
                for (int xA = 0; xA < this.RectangleRotation.Width; xA++)
                {
                    var xB = (int)Math.Round(posInB.X);
                    var yB = (int)Math.Round(posInB.Y);
                    if (0 <= xB && xB < sprite.RectangleRotation.Width &&
                        0 <= yB && yB < sprite.RectangleRotation.Height)
                    {
                        var th = xA + yA * this.RectangleRotation.Width;
                        var sp = xB + yB * sprite.RectangleRotation.Width;
                        if (this.TextureData.Length > th && th >= 0 && 
                            sprite.TextureData.Length > sp && sp >= 0)
                        {
                            var colourA = this.TextureData[th];
                            var colourB = sprite.TextureData[sp];
                            if (colourA.A != 0 && colourB.A != 0)
                                return true;
                        }
                    }
                    posInB += stepX;
                }
                yPosInB += stepY;
            }
            return false;
        }

        #region Colloision Collides.Collide(this, sprite)
        protected bool IsTouchingLeft(Wall wall)
        {
            return this.Rectangle.Right + this.Velocity.X >= wall.RectangleWall.Left &&
              this.Rectangle.Left < wall.RectangleWall.Left &&
              this.Rectangle.Bottom > wall.RectangleWall.Top &&
              this.Rectangle.Top < wall.RectangleWall.Bottom;
        }

        protected bool IsTouchingRight(Wall wall)
        {
            return this.Rectangle.Left + this.Velocity.X <= wall.RectangleWall.Right - 18 &&
              this.Rectangle.Right > wall.RectangleWall.Right - 18 &&
              this.Rectangle.Bottom > wall.RectangleWall.Top &&
              this.Rectangle.Top < wall.RectangleWall.Bottom;
        }

        protected bool IsTouchingTop(Wall wall)
        {
            return this.Rectangle.Bottom + this.Velocity.Y >= wall.RectangleWall.Top &&
              this.Rectangle.Top < wall.RectangleWall.Top &&
              this.Rectangle.Right > wall.RectangleWall.Left &&
              this.Rectangle.Left < wall.RectangleWall.Right - 18;
        }

        protected bool IsTouchingBottom(Wall wall)
        {
            return this.Rectangle.Top + this.Velocity.Y <= wall.RectangleWall.Bottom &&
              this.Rectangle.Bottom > wall.RectangleWall.Bottom &&
              this.Rectangle.Right > wall.RectangleWall.Left &&
              this.Rectangle.Left < wall.RectangleWall.Right - 18 && Collides.Collide(this, wall);
        }

        #endregion
    }
}
