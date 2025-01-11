using System;
using GameOfVlad.Game.Levels;
using GameOfVlad.OldProject.Sprites.Shells;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace GameOfVlad.OldProject.Sprites.Mobs
{
    public class Librarian : Mob
    {
        private Animation animationLeft;
        private Animation animationRight;
        private Lazer Lazer;

        public float Time = 0;
        public float TimeToShot = 3f;

        public Librarian(ContentManager content, Texture2D texture, Vector2 location, Level level)
            : base(content, texture, location, level)
        {
            animationLeft = new Animation(Content, "Sprite/Librarian/Left/", 7, 8);
            animationRight = new Animation(Content, "Sprite/Librarian/Right/", 7, 8);

            Origin = new Vector2(Texture.Width / 2, Texture.Height / 2);
            Lazer = new Lazer(Content,
                              Content.Load<Texture2D>("Sprite/Rocket/WoodGoblinGun/Bullet"),
                              Level,
                              this){ 
                              Param = new Lazer.Params( 13 , 1f, 0.005f ,1f , 0.005f, 1 ,0) };
            HPBar = 20;
        }

        public override void Update(GameTime gameTime)
        {
            var dir = Level.Player.Location - Location;
            if (dir != Vector2.Zero)
                dir.Normalize();
            var rot = (float)Math.Atan2(-dir.Y, -dir.X);
            if (Rotation < rot)
            {
                Rotation += 0.005f;
            }
            if (Rotation > rot)
            {
                Rotation -= 0.005f;
            }
            Direction = new Vector2((float)Math.Cos(Rotation + Math.PI),
                                   -(float)Math.Sin(-Rotation + Math.PI));

            if (Math.Abs(Rotation) > 1.5) 
            {
                animationRight.Update(gameTime);
                if (animationRight.Next)
                {
                    Texture = animationRight.GetTexture;
                }
            }
            else
            {
                animationLeft.Update(gameTime);
                if (animationLeft.Next)
                {
                    Texture = animationLeft.GetTexture;
                }
            }

            Time += (float)gameTime.ElapsedGameTime.TotalSeconds;
            if (Time > TimeToShot)
            {
                Time = 0;

                var lazer = Lazer.Clone() as Lazer;
                lazer.Create(Direction);
                Level.Shells.Add(lazer);
            }
        }
    }
}
