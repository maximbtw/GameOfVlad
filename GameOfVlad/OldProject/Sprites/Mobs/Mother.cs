using System;
using GameOfVlad.Game.Levels;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace GameOfVlad.OldProject.Sprites.Mobs
{
    class Mother : Mob
    {
        enum StateMove { Left,Bot,Right,Top,None}
        StateMove state = StateMove.None;
        float Height;
        float Width;
        int swich = 0;

        Animation animationRight;
        public Children Children;
        private float timer = 0;
        public float SpawnTime = 2.5f;

        public Mother(ContentManager content, Texture2D texture, Vector2 location, Level level)
            : base(content, texture, location, level)
        {
            animationRight = new Animation(Content, "Sprite/Mother/Right/", 0.2f, 32);
            Children = new Children(Content, Content.Load<Texture2D>("Sprite/Mother/Children/1"), Location, Level);
            Origin = new Vector2(Texture.Width / 2, Texture.Height / 2);
            Width = Level.LevelSize.Width - 135;
            Height = Level.LevelSize.Height - 135;
            HPBar = 125;
            Speed = 1;
        }


        public override void Update(GameTime gameTime)
        {
            timer += (float)gameTime.ElapsedGameTime.TotalSeconds;
            if (timer > SpawnTime)
                SpawnChildren();
            UpdateMove(gameTime);

            animationRight.Update(gameTime);
            if (animationRight.Next)
            {
                Texture = animationRight.GetTexture;
            }

        }

        void UpdateMove(GameTime gameTime)
        {
            var nextLoc = Location + Direction * Speed;
            switch (state) 
            {
                case StateMove.None:
                    DefineLoc();
                    break;
                case StateMove.Left:
                    if (nextLoc.X <= 135)
                        UpdateSlew( 0, 1, StateMove.Bot);
                    break;
                case StateMove.Top:
                    if (nextLoc.Y <= 135)
                        UpdateSlew( -1, 0, StateMove.Left);
                    break;
                case StateMove.Right:
                    if (nextLoc.X >= Width)
                        UpdateSlew( 0, -1, StateMove.Top);
                    break;
                case StateMove.Bot:
                    if (nextLoc.Y >= Height)
                        UpdateSlew( 1, 0, StateMove.Right);
                    break;
            }
            Rotation = (float)Math.Atan2(Direction.Y, Direction.X);
            Location += Direction * Speed;
        }

        void UpdateSlew(int X, int Y, StateMove nextState)
        {
            swich++;
            Direction.X += (Direction.X > X) ? -0.01f : 0.01f;
            Direction.Y += (Direction.Y > Y) ? -0.01f : 0.01f;
            if (swich>=100)
            {
                Direction = new Vector2(X, Y);
                state = nextState;
                swich = 0;
            }
        }


        void DefineLoc() 
        {
            var loc = new Vector2(Location.X + Texture.Width / 2, Location.Y + Texture.Height / 2);
            var maxH = (loc.Y > Height / 2) ? Height - loc.Y : loc.Y;
            var maxW = (loc.X > Width / 2) ? Width - loc.X : loc.X;
            if (maxH < maxW)
                state = (loc.Y > Height / 2) ? StateMove.Bot : StateMove.Top;
            else
                state = (loc.X > Width / 2) ? StateMove.Right : StateMove.Left;

            Direction = (state == StateMove.Left)  ? new Vector2(-1, 0) :
                        (state == StateMove.Top)   ? new Vector2(0, -1) :
                        (state == StateMove.Right) ? new Vector2(1, 0 ) :
                                                     new Vector2(0, 1 );
        }

        private void SpawnChildren()
        {
            timer = 0;
            var children = Children.Clone() as Children;
            children.Location = new Vector2(Location.X,Location.Y);
            children.Parent = this;
            Level.HostileMobs.Add(children);
        }
    }
}
