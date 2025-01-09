using System;
using System.Collections.Generic;
using GameOfVlad.Game.Levels;
using GameOfVlad.OldProject.Sprites.Abilities;
using GameOfVlad.OldProject.Sprites.Guns;
using GameOfVlad.OldProject.Sprites.Mobs;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace GameOfVlad.OldProject.Sprites
{
    public class Player : Mob
    {
        public enum State 
        { 
            Rocket,
            Run 
        }

        public enum WeaponState
        {
            Standart,
            DartGun,
            BlackCatGun,
            MiniSharkGun,
            CutterGun,
            SandstoneGun,
            SoundGun,
            TuskGun,
            DemagogueGun,
            WoodGoblinGun
        }
        public enum Turn
        {
            None = 0,
            Left = -1,
            Right = 1,
            Up,
            Dawn,
            Stay
        }
        public State StateGame = State.Rocket;

        private Animation animationLeft;
        private Animation animationRight;
        private Animation animationStay;

        public Turn turnState = Turn.None;
        public WeaponState _WeaponState = WeaponState.Standart;
        public Dictionary<WeaponState, Gun> Guns;
        private int currentScrollValue;
        private int previousScrollValue;

        private Physics physics;
        public float BordarY = 10;

        public Abilitie Boost;
        public Shield Shield;

        private static readonly float rotationVelocity = 1f;
        public float AccelerationMax = 4;
        private static readonly float deltaKA = 0.011f;

        public Player(ContentManager content, Texture2D texture, Vector2 location, Level level)
            : base(content, texture, location, level)
        {
            animationLeft = new Animation(Content, "PersonRun/Left/", 4, 10);
            animationRight = new Animation(Content, "PersonRun/Right/", 4, 10);
            animationStay = new Animation(Content, "PersonRun/Stay/", 4, 1);

            Origin = new Vector2(Texture.Width / 2, Texture.Height / 2);
            Speed = 0.001f;
            //Speed = 5f;
            HPBar = 40;
            physics = new Physics();

            Guns = new Dictionary<WeaponState, Gun>
            {
                [WeaponState.DartGun] = new DartGun(Content, this, Level),
                [WeaponState.BlackCatGun] = new BlackCatGun(Content, this, Level),
                [WeaponState.MiniSharkGun] = new MiniSharkGun(Content, this, Level),
                [WeaponState.CutterGun] = new CutterGun(Content, this, Level),
                [WeaponState.SandstoneGun] = new SandstoneGun(Content, this, Level),
                [WeaponState.SoundGun] = new SoundGun(Content, this, Level),
                [WeaponState.TuskGun] = new TuskGun(Content, this, Level),
                [WeaponState.DemagogueGun] = new DemagogueGun(Content, this, Level),
                [WeaponState.WoodGoblinGun] = new WoodGoblinGun(Content, this, Level),
            };

            Boost = new Boost(Content, Level, this, 240, 0.25f);
            Shield = new Shield(Content, Level, this, 240, 2f);
            UpdateWeaponMenu();
        }

        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            switch (StateGame)
            {
                case State.Rocket:
                    DrawRocket(gameTime, spriteBatch);
                    break;
                case State.Run:
                    DrawRun(gameTime, spriteBatch);
                    break;
            }
        }

        public override void Update(GameTime gameTime)
        {
            switch (StateGame)
            {
                case State.Rocket:
                    UpdateRocket(gameTime);
                    break;
                case State.Run:
                    UpdateRun(gameTime);
                    break;
            }
        }

        public void DrawRocket(GameTime gameTime, SpriteBatch spriteBatch)
        {
            base.Draw(gameTime, spriteBatch);
            Boost.Draw(gameTime, spriteBatch);
            Shield.Draw(gameTime, spriteBatch);
        }

        public void DrawRun(GameTime gameTime, SpriteBatch spriteBatch)
        {
            if (turnState == Turn.Right)
            {
                animationRight.Update(gameTime);
                if (animationRight.Next)
                {
                    Texture = animationRight.GetTexture;
                }
            }
            else if (turnState == Turn.Left)
            {
                animationLeft.Update(gameTime);
                if (animationLeft.Next)
                {
                    Texture = animationLeft.GetTexture;
                }
            }
            else
            {
                animationStay.Update(gameTime);
                if (animationStay.Next)
                {
                    Texture = animationStay.GetTexture;
                }
            }

            base.Draw(gameTime, spriteBatch);
        }

        public void UpdateRocket(GameTime gameTime)
        {
            if (Dead)
                Level.Death();
            UpdateRocketMove(gameTime);
            UpdateGuns(gameTime);
            UpdateAbility(gameTime);
            if (!End)
                UpdateEffect(gameTime);

            //if (Keyboard.GetState().IsKeyDown(Keys.A))
            //    Location += new Vector2(-5, 0);
            //if (Keyboard.GetState().IsKeyDown(Keys.D))
            //    Location += new Vector2(5, 0);
            //if (Keyboard.GetState().IsKeyDown(Keys.W))
            //    Location += new Vector2(0, -5);
            //if (Keyboard.GetState().IsKeyDown(Keys.S))
            //    Location += new Vector2(0, 5);
        }


        public bool hasJumped = false;
        public void UpdateRun(GameTime gameTime)
        {
            Location += Velocity;
            #region jopa
            if (Keyboard.GetState().IsKeyDown(Keys.A))
            {
                Velocity.X = -3.5f;
                turnState = Turn.Left;
            }
            else if (Keyboard.GetState().IsKeyDown(Keys.D))
            {
                Velocity.X = 3.5f;
                turnState = Turn.Right;
            }
            else
            {
                Velocity.X = 0f;
                turnState = Turn.Stay;
            }
            if (Keyboard.GetState().IsKeyDown(Keys.Space) && !hasJumped)
            {
                Location.Y -= 14f;
                Velocity.Y = -7f;
                hasJumped = true;
                //turnState = Turn.Up;
            }
            if (hasJumped)
            {
                float i = 1;
                Velocity.Y += 0.15f * i;
            }

            if (!hasJumped)
                Velocity.Y = 0;

            int count = 0;
            foreach (var wall in Level.Walls)
            {
                if ((this.Velocity.X > 0 && this.IsTouchingLeft(wall)) ||
                    (this.Velocity.X < 0 & this.IsTouchingRight(wall)))
                    this.Velocity.X = 0;
                if (this.Velocity.Y < 0 & this.IsTouchingBottom(wall))
                    this.Velocity.Y = 0;
                if (this.Velocity.Y > 0 && this.IsTouchingTop(wall))
                {
                    this.Velocity.Y = 0;
                    hasJumped = false;
                    count++;
                }
                else if (count == 0) hasJumped = true;
            }
            #endregion
        }

        private void UpdateRocketMove(GameTime gameTime)
        {
            if (Keyboard.GetState().IsKeyDown(Keys.A))
            {
                Rotation -= MathHelper.ToRadians(rotationVelocity);
                turnState = Turn.Left;
                if (Speed > deltaKA) Speed -= deltaKA;
                else Speed = 0;
            }
            else if (Keyboard.GetState().IsKeyDown(Keys.D))
            {
                Rotation += MathHelper.ToRadians(rotationVelocity);
                turnState = Turn.Right;
                if (Speed > deltaKA) Speed -= deltaKA;
                else Speed = 0;
            }
            if (turnState != Turn.None)
                Speed = (Speed < AccelerationMax - deltaKA) ? (Speed + deltaKA) : AccelerationMax;

            if (Location.Y < BordarY)
                Location = new Vector2(Location.X, Math.Max(BordarY, Location.Y));
            if (Location.X < 10)
                Location = new Vector2(Math.Max(10, Location.X), Location.Y);
            if (Location.Y > Level.LevelSize.Height - 15)
                Location = new Vector2(Location.X, Math.Min(Level.LevelSize.Height - 15, Location.Y));
            if (Location.X > Level.LevelSize.Width - 15)
                Location = new Vector2(Math.Min(Level.LevelSize.Width-15, Location.X), Location.Y);

            physics.MoveRocket(this);
            Direction = (turnState == Turn.None) ? Vector2.Zero : physics.VelosityRocket;

            if (!Boost.Activ)
            {
                Location += Speed * Direction;
            }
        }
 
        private void UpdateGuns(GameTime gameTime)
        {
            currentScrollValue = Mouse.GetState().ScrollWheelValue;

            if (currentScrollValue < previousScrollValue)
            {
                if (_WeaponState == (WeaponState)Guns.Count)
                {
                    _WeaponState = 0;
                    Texture = Content.Load<Texture2D>("Sprite/Rocket/Rocket");
                }
                else
                {
                    _WeaponState++;
                    Texture = Guns[_WeaponState].SkinGun;
                }
                UpdateWeaponMenu();
            }
            else if (currentScrollValue > previousScrollValue)
            {
                if (_WeaponState == 0)
                {
                    _WeaponState = (WeaponState)Guns.Count;
                    Texture = Guns[_WeaponState].SkinGun;
                }
                else if (_WeaponState == (WeaponState)1)
                {
                    _WeaponState--;
                    Texture = Content.Load<Texture2D>("Sprite/Rocket/Rocket");
                }
                else
                {
                    _WeaponState--;
                    Texture = Guns[_WeaponState].SkinGun;
                }
                UpdateWeaponMenu(); 
            }

            if (turnState != Turn.None && _WeaponState != WeaponState.Standart)
                Guns[_WeaponState].Update(gameTime);

            previousScrollValue = currentScrollValue;
        }

        void UpdateWeaponMenu()
        {
            Level.WeaponMenu.Clear();
            if (Guns.Count == 0)
            {
                Level.WeaponMenu.Add(new Sprite(Content.Load<Texture2D>("Sprite/Rocket/S"), new Vector2(1594, 75)));
                Level.WeaponMenu.Add(new Sprite(Content.Load<Texture2D>("Sprite/Rocket/M"), new Vector2(1684, 75)));
                Level.WeaponMenu.Add(new Sprite(Content.Load<Texture2D>("Sprite/Rocket/S"), new Vector2(1774, 75)));
            }
            else if(Guns.Count == 1)
            {
                if (_WeaponState == 0)
                {
                    Level.WeaponMenu.Add(new Sprite(Guns[(WeaponState)1].WeaponS, new Vector2(1594, 75)));
                    Level.WeaponMenu.Add(new Sprite(Content.Load<Texture2D>("Sprite/Rocket/M"), new Vector2(1684, 75)));
                    Level.WeaponMenu.Add(new Sprite(Guns[(WeaponState)1].WeaponS, new Vector2(1774, 75)));
                }
                else
                {
                    Level.WeaponMenu.Add(new Sprite(Content.Load<Texture2D>("Sprite/Rocket/S"), new Vector2(1594, 75)));
                    Level.WeaponMenu.Add(new Sprite(Guns[_WeaponState].WeaponM, new Vector2(1684, 75)));
                    Level.WeaponMenu.Add(new Sprite(Content.Load<Texture2D>("Sprite/Rocket/S"), new Vector2(1774, 75)));
                }
            }
            else
            {
                Texture2D texture0;
                Texture2D texture1;
                Texture2D texture2;
                if (_WeaponState == 0)
                {
                    texture0 = Guns[(WeaponState)(Guns.Count - 1)].WeaponS;
                    texture1 = Content.Load<Texture2D>("Sprite/Rocket/M");
                    texture2 = Guns[_WeaponState + 1].WeaponS;
                }
                else if (_WeaponState == (WeaponState)1)
                {
                    texture0 = Content.Load<Texture2D>("Sprite/Rocket/S");
                    texture1 = Guns[_WeaponState].WeaponM;
                    texture2 = Guns[_WeaponState + 1].WeaponS;
                }
                else if (_WeaponState == (WeaponState)Guns.Count)
                {
                    texture0 = Guns[_WeaponState - 1].WeaponS;
                    texture1 = Guns[_WeaponState].WeaponM;
                    texture2 = Content.Load<Texture2D>("Sprite/Rocket/S");
                }
                else
                {
                    texture0 = Guns[_WeaponState - 1].WeaponS;
                    texture1 = Guns[_WeaponState].WeaponM;
                    texture2 = Guns[_WeaponState + 1].WeaponS;
                }

                Level.WeaponMenu.Add(new Sprite(texture0, new Vector2(1594, 75)));
                Level.WeaponMenu.Add(new Sprite(texture1, new Vector2(1684, 75)));
                Level.WeaponMenu.Add(new Sprite(texture2, new Vector2(1774, 75)));
            }
        }

        private void UpdateAbility(GameTime gameTime)
        {
            Boost.Update(gameTime);
            Shield.Update(gameTime);
        }

        public override void WasShot(int damage)
        {
            base.WasShot(damage);
            StartEffect();
        }

        private int timeCounter = 0;
        public bool End = true;

        public void UpdateEffect(GameTime gameTime)
        {
            if (timeCounter >= 512)
            {
                Color = Color.White;
                End = true;
            }
            else
            {
                Color = new Color(255, timeCounter % 256, timeCounter % 256, 255);
                timeCounter += 30;
            }
        }

        public void StartEffect()
        {
            Color = Color.White;
            End = false;
            timeCounter = 0;
        }
    }
}
