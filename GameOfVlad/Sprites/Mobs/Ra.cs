using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using GameOfVlad.Sprites.Shells;
using Microsoft.Xna.Framework;
using GameOfVlad.Tools;
using System;
using GameOfVlad.Game.Levels;

namespace GameOfVlad.Sprites.Mobs
{
    class Ra : Mob
    {
        private Animation animation;

        private Bullet bullet;
        public float TimerBullet = 0;
        public float TimeToShot = 5f;
        public float BulletSpeed = 6f;

        private float timerMove = 0;
        private Random random;
        private float timeToSwichMove;
        private StateMove stateMove;
        Move xMove;
        Move yMove;
        enum StateMove { Free, Aggressively }

        public Ra(ContentManager content, Texture2D texture, Vector2 location, Level level)
             : base(content, texture, location, level)
        {
            animation = new Animation(Content, "Sprite/Ra/", 8, 20);
            bullet = new Bullet(Content, Content.Load<Texture2D>("Sprite/Ra/Bullet1"), Level, this) { Speed = BulletSpeed};
            random = new Random();
            HPBar = 20;
            SwichMove();
        }

        public override void Update(GameTime gameTime)
        {
            animation.Update(gameTime);
            if (animation.Next)
                Texture = animation.GetTexture;
            UpdateMove(gameTime);
            UpdateBullet(gameTime);
        }

        private void UpdateBullet(GameTime gameTime)
        {
            TimerBullet += (float)gameTime.ElapsedGameTime.TotalSeconds;
            if (TimerBullet > TimeToShot)
            {
                float rotation = 0;
                TimerBullet = 0;
                for(int i = 0; i < 12; i++)
                {
                    var bullet = this.bullet.Clone() as Bullet;
                    bullet.Location = new Vector2(Location.X + Texture.Width/2, Location.Y + Texture.Height/2);
                    bullet.Rotation = rotation;
                    bullet.Direction = new Vector2((float)Math.Cos(rotation), (float)Math.Sin(rotation));
                    Level.Shells.Add(bullet);
                    rotation += (float)Math.PI / 6;
                }
            }
        }

        private void UpdateMove(GameTime gameTime)
        {
            timerMove += (float)gameTime.ElapsedGameTime.TotalSeconds;
            if (timerMove > timeToSwichMove)
                SwichMove();

            if (stateMove == StateMove.Aggressively)
            {
                if ((Location.X < 50 || Location.X > Level.LevelSize.Width - 217) && !xMove.Swich)
                {
                    xMove.Direction = Direction.X * -1;
                    xMove.Swich = true;
                }
                if ((Location.Y < 50 || Location.Y > Level.LevelSize.Height - 175) && !yMove.Swich)
                {
                    yMove.Direction = Direction.Y * -1;
                    yMove.Swich = true;
                }

                if (xMove.Swich)
                {
                    xMove.TTK++;
                    if (Direction.X < xMove.Direction && xMove.TTK <= 200)
                    {
                        Direction.X += 0.005f;
                    }
                    else if (Direction.X > xMove.Direction && xMove.TTK <= 200)
                    {
                        Direction.X -= 0.005f;
                    }
                    else
                    {
                        Direction.X = xMove.Direction;
                        xMove.Swich = false;
                        xMove.TTK = 0;
                    }
                }
                if (yMove.Swich)
                {
                    yMove.TTK++;
                    if (Direction.Y < yMove.Direction && yMove.TTK <= 200)
                    {
                        Direction.Y += 0.005f;
                    }
                    else if (Direction.Y > yMove.Direction && yMove.TTK <= 200)
                    {
                        Direction.Y -= 0.005f;
                    }
                    else
                    {
                        Direction.Y = yMove.Direction;
                        yMove.Swich = false;
                        yMove.TTK = 0;
                    }
                }
            }
            if (stateMove == StateMove.Free)
            {
                Direction = Level.Player.Location - Location;
                if (Direction != Vector2.Zero)
                    Direction.Normalize();
            }
            Location += Direction * Speed;
        }

        private void SwichMove()
        {
            timeToSwichMove = random.Next(8, 18);
            if (stateMove == StateMove.Aggressively)
            {
                stateMove = StateMove.Free;
                Speed = 2.5f;
            }
            else
            {
                stateMove = StateMove.Aggressively;
                Speed = 6;
                float x, y;
                x = (Level.Player.Location.X > Location.X) ? 0.25f : -0.25f;
                y = (Level.Player.Location.Y > Location.Y) ? 0.25f : -0.25f;
                Direction = new Vector2(x, y);
                xMove = new Move(Direction.X);
                yMove = new Move(Direction.Y);
            }
            timerMove = 0;
        }
    }
    struct Move
    {
        public int TTK;
        public bool Swich;
        public float Direction;

        public Move(float direction)
        {
            TTK = 0;
            Swich = false;
            Direction = direction;
        }
    }
}
