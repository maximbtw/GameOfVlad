using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using System.Collections.Generic;
using GameOfVlad.Sprites.Shells;
using Microsoft.Xna.Framework;
using System;
using GameOfVlad.Game.Levels;
using GameOfVlad.GameEffects;
using GameOfVlad.Utils;

namespace GameOfVlad.Sprites.Mobs
{
    class Kalabok : Mob
    {
        public enum State
        {
            One,
            Too,
            Three,
            Foa,
            Five
        }

        public float Distance = 100;
        public float TimeToShot = 4f;
        public float Time = 0;

        private Animation animationLeft;
        private Animation animationRight;
        private Bomb bomb;
        private List<Vector2> bombLocations;

        Move xMove;
        Move yMove;

        public Kalabok(ContentManager content, Texture2D texture, Vector2 location, Level level)
            : base(content, texture, location, level)
        {
            Origin = new Vector2(Texture.Width / 2, Texture.Height / 2);
            animationLeft = new Animation(Content, "Sprite/Kalabok/Left/", 8, 4);
            animationRight = new Animation(Content, "Sprite/Kalabok/Right/", 8, 4);
            bombLocations = new List<Vector2>();
            bomb = new Bomb(Content,
                            Content.Load<Texture2D>("Sprite/Kalabok/Bomb"),
                            Level, 
                            this){
                            Distance = this.Distance,
                            Damage = 5,
                            ttk = 10,
                            Parent = this};
            List<Texture2D> texturesFire = new List<Texture2D>
            {
                Content.Load<Texture2D>("Sprite/Rocket/DemagogueGun/Effect1"),
                Content.Load<Texture2D>("Sprite/Rocket/DemagogueGun/Effect2"),
                Content.Load<Texture2D>("Sprite/Rocket/DemagogueGun/Effect3"),
            };
            bomb.Effect = new ParticleEngine(texturesFire, 15, 5)
            { G = 0.1f, B = 0.5f };

            Direction = new Vector2(-0.25f, 0.25f);
            xMove = new Move(Direction.X);
            yMove = new Move(Direction.Y);
            Rotation = (float)Math.Atan2(Direction.Y, Direction.X);
            Speed = 6;
        }

        public override void Update(GameTime gameTime)
        {
            Time += (float)gameTime.ElapsedGameTime.TotalSeconds;
            if (Time > TimeToShot)
            {
                Time = 0;
                SetBomb();
            }

            foreach (var location in bombLocations)
            {
                if ((Level.Player.Location - location).Length() < Distance)
                {
                    DetonateBombs();
                    break;
                }
            }

            UpdateMove();

            if (Math.Abs(Rotation) > 1.5)
            {
                animationRight.Update(gameTime);
                if (animationRight.Next)
                    Texture = animationRight.GetTexture;
            }
            else
            {
                animationLeft.Update(gameTime);
                if (animationLeft.Next)
                    Texture = animationLeft.GetTexture;
            }
        }

        private void UpdateMove()
        {
            if ((Location.X < 150 || Location.X > Level.LevelSize.Width - 100) && !xMove.Swich)
            {
                xMove.Direction = Direction.X * -1;
                xMove.Swich = true;
            }
            if ((Location.Y < 75 || Location.Y > Level.LevelSize.Height - 125) && !yMove.Swich)
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

            Rotation = (float)Math.Atan2(-Direction.Y, -Direction.X);

            Location += Direction * Speed;
        }

        private void SetBomb()
        {
            var bomb = this.bomb.Clone() as Bomb;
            bomb.Location = Location;
            Level.Shells.Add(bomb);
            bombLocations.Add(Location);
        }

        private void DetonateBombs()
        {
            foreach (var shell in Level.Shells)
            {
                if (shell is Bomb)
                {
                    var bomb = (Bomb)shell;
                    if (bomb.Parent==this)
                        bomb.Explosion = true;
                }
            }
            bombLocations.Clear();
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
}
