using System.Collections.Generic;
using GameOfVlad.Game.Levels;
using GameOfVlad.OldProject.GameEffects;
using GameOfVlad.OldProject.Sprites.Mobs;
using GameOfVlad.OldProject.Sprites.Shells;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace GameOfVlad.OldProject.Sprites.Guns
{
    public class DemagogueGun : Gun
    {
        public Bomb Bomb;

        public DemagogueGun(ContentManager content, Mob parent, Level level)
            : base(content, parent, level)
        {
            SkinGun = Content.Load<Texture2D>("Sprite/Rocket/DemagogueGun/Rocket");
            WeaponM = Content.Load<Texture2D>("Sprite/Rocket/DemagogueGun/M");
            WeaponS = Content.Load<Texture2D>("Sprite/Rocket/DemagogueGun/S");

            Bomb = new Bomb(Content,
                            Content.Load<Texture2D>("Sprite/Rocket/DemagogueGun/Bomb"),
                            Level,
                            Parent) {
                            Gun = this,
                            Distance = 150, 
                            Damage=5, 
                            ttk = 10};

            List<Texture2D> texturesFire = new List<Texture2D>
            {
                Content.Load<Texture2D>("Sprite/Rocket/DemagogueGun/Effect1"),
                Content.Load<Texture2D>("Sprite/Rocket/DemagogueGun/Effect2"),
                Content.Load<Texture2D>("Sprite/Rocket/DemagogueGun/Effect3"),
            };
            Bomb.Effect = new ParticleEngine(texturesFire, 15, 5) 
                                            { G=0.1f,B=0 };

            TimeShot = 3f;
        }

        public override void Update(GameTime gameTime)
        {
            Timer += (float)gameTime.ElapsedGameTime.TotalSeconds;
            mouse = Mouse.GetState();

            if (mouse.RightButton == ButtonState.Pressed && Timer > TimeShot)
            {
                SetBomb();
            }

            if (mouse.LeftButton == ButtonState.Pressed)
            {
                DetonateBombs();
            }
        }

        private void SetBomb()
        {
            Timer = 0;

            var bomb = this.Bomb.Clone() as Bomb;
            bomb.Explosion = false;
            bomb.Gun = this;
            bomb.Location = Parent.Location;
            Level.Shells.Add(bomb);
        }

        private void DetonateBombs()
        {
            foreach(var shell in Level.Shells)
            {
                if(shell is Bomb)
                {
                    var bomb = (Bomb)shell;
                    if(bomb.Gun==this)
                        bomb.Explosion = true;
                }
            }
        }
    }
}
