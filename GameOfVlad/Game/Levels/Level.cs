using System;
using System.Collections.Generic;
using GameOfVlad.GameEffects;
using GameOfVlad.Interfaces;
using GameOfVlad.Sprites;
using GameOfVlad.Sprites.Bonuses;
using GameOfVlad.Sprites.Mobs;
using GameOfVlad.Sprites.Shells;
using GameOfVlad.Tools;
using GameOfVlad.Tools.Keyboards;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

namespace GameOfVlad.Game.Levels
{
    public class Level 
    {
        public enum State
        {
            Play,
            Pause,
            LevelComplite,
            Death
        }
        public enum StateGame
        {
            Space, Ship,
        }
        public HitBulletEffect HitBulletEffect;
        public List<HitBulletEffect> EffectsBulletHit;
        public List<Star> Stars;
        //Keyboard
        public KeyboardStateObserver KeyboardState;
        //Music
        private float timeMusic = 0;
        public Song Music;
        //Game
        public GameOfVlad Game;
        public GraphicsDevice GraphicsDevice;
        public ContentManager Content;
        //LevelSetting
        public Texture2D Backgraund { get; set; }
        public Size LevelSize { get; set; }
        public StateGame _StateGame { get; set; }
        public State StateProcess { get; set; }
        public Gravity Gravity { get; set; }
        //Menu
        public PauseMenu PauseMenu { get; set; }
        public CompliteMenu CompliteMenu { get; set; }
        public DeathMenu DeathMenu { get; set; }
        //DataManager
        private Texture2D textureCursor;
        private SpriteFont font;
        public string Name;
        public int IndexLevel;
        private float playTime = 0;
        public string DeathCount;
        private Stack<Sprite> LifeIcons;
        public bool SpecialLevel = false;
        public bool StartMusic = true;
        int MusicSwich = 0;
        //Mobs
        public List<Mob> HostileMobs { get; set; }
        public List<Shell> Shells { get; set; }
        public List<Bonus> Bonuses { get; set; }
        public List<Wall> Walls { get; set; }
        public List<Sprite> Icons { get; set; }
        public List<Sprite> WeaponMenu { get; set; }
        public Sprite Earth { get; set; }
        public Sprite BlackHole { get; set; }
        public Player Player { get; set; }

        public Vector2 MousePosition { get { return new Vector2(Mouse.GetState().X, Mouse.GetState().Y); } }

        public Level(GameOfVlad game, GraphicsDevice graphicsDevice, ContentManager content)
        {
            Game = game;
            GraphicsDevice = graphicsDevice;
            Content = content;

            KeyboardState = new KeyboardStateObserver();
            font = content.Load<SpriteFont>("Pages/GamePlay/Font");

            Shells = new List<Shell>();
            WeaponMenu = new List<Sprite>(3);
            LifeIcons = new Stack<Sprite>();
            Icons = new List<Sprite>()
            {
                new Sprite(Content.Load<Texture2D>("Pages/GamePlay/DeadIcon"), new Vector2(1840, 8)),
                new Sprite(Content.Load<Texture2D>("Pages/GamePlay/WeaponMenu"), new Vector2(1608, 75)),
            };

            textureCursor = Content.Load<Texture2D>("Pages/GamePlay/Cursor");
            
            List<Texture2D> textures = new List<Texture2D>();
            textures.Add(Content.Load<Texture2D>("Sprite/Rocket/BulletShot/Particle"));
            textures.Add(Content.Load<Texture2D>("Sprite/Rocket/BulletShot/Particle1"));
            HitBulletEffect = new HitBulletEffect(textures, Vector2.Zero);
            EffectsBulletHit = new List<HitBulletEffect>();
        }

        public virtual void InitializeSprites()
        {

        }

        public void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(Backgraund, new Rectangle(0, 0, 1920, 1080), Color.White);

            switch (StateProcess)
            {
                case State.Play:
                    DrawPlay(gameTime, spriteBatch);
                    break;
                case State.Pause:
                    PauseMenu.Draw(gameTime, spriteBatch);
                    break;
                case State.LevelComplite:
                    CompliteMenu.Draw(gameTime, spriteBatch);
                    break;
                case State.Death:
                    DeathMenu.Draw(gameTime, spriteBatch);
                    break;
            }
        }

        public void Update(GameTime gameTime)
        {
          //  KeyboardState.UpdateStart();

            switch (StateProcess)
            {
                case State.Play:
                    UpdatePlay(gameTime);
                    break;
                case State.Pause:
                    PauseMenu.Update(gameTime);
                    break;
                case State.LevelComplite:
                    CompliteMenu.Update(gameTime);
                    break;
                case State.Death:
                    DeathMenu.Update(gameTime);
                    break;
            }

          //  KeyboardState.UpdateEnd();
        }

        public virtual void DrawPlay(GameTime gameTime, SpriteBatch spriteBatch)
        {
            foreach(var icon in Icons)
                icon.Draw(gameTime, spriteBatch);
            if(WeaponMenu!=null)
                foreach (var weapon in WeaponMenu)
                    weapon.Draw(gameTime, spriteBatch);
            foreach (var life in LifeIcons) 
                life.Draw(gameTime, spriteBatch);

            spriteBatch.DrawString(font, "время: " + playTime.ToString("N2"), new Vector2(25, 25), Color.BlanchedAlmond);
            spriteBatch.DrawString(font, DeathCount, new Vector2(LevelSize.Width-55, 10), Color.Gray);
            spriteBatch.DrawString(font, Player.HPBar.ToString()+"/100", new Vector2(LevelSize.Width - 360, 10), Color.Red);
            //spriteBatch.DrawString(font,Rocket.Velocity.ToString(), new Vector2(200, 200), Color.Blue);
            ////spriteBatch.DrawString(font, Rocket.Speed.ToString(), new Vector2(400, 200), Color.Blue);
            //spriteBatch.DrawString(font, Rocket.Location.ToString(), new Vector2(500, 200), Color.Blue);
            //spriteBatch.DrawString(font, HostileMobs.Count.ToString(), new Vector2(500, 200), Color.Blue);
            switch (_StateGame)
            {
                case StateGame.Space:
                    if (!SpecialLevel)
                        Earth.Draw(gameTime, spriteBatch);
                    break;
                case StateGame.Ship:
                    if (Walls != null)
                        foreach (var wall in Walls)
                            wall.Draw(gameTime, spriteBatch);
                    break;
            }
            if (BlackHole != null)
            {
                BlackHole.Draw(gameTime, spriteBatch);
            }
            if(Stars!=null)
                foreach (var star in Stars)
                    star.Draw(gameTime, spriteBatch);
            foreach (var shell in Shells)
            {
                shell.Draw(gameTime, spriteBatch);
            }
            foreach (var mob in HostileMobs)
            {
                mob.Draw(gameTime, spriteBatch);
                //spriteBatch.DrawString(font, mob.Direction.ToString(), new Vector2(150, 150), Color.Blue);
                //spriteBatch.DrawString(font, mob.Location.ToString(), new Vector2(500, 150), Color.Red);
                //spriteBatch.DrawString(font, mob.HPBar.ToString(), new Vector2(150, 200), Color.Blue);
            }

            if (Bonuses != null)
                foreach (var bonus in Bonuses)
                    bonus.Draw(gameTime, spriteBatch);

            Player.Draw(gameTime, spriteBatch);

            foreach (var effect in EffectsBulletHit)
                effect.Draw(spriteBatch);
        }

        public virtual void UpdatePlay(GameTime gameTime)
        {
            UpdateMusic(gameTime);
            UpdateLife();

            switch (_StateGame)
            {
                case StateGame.Space:
                    if (!SpecialLevel && Collides.CollideEarth(Player, Earth))
                        Save();
                    break;
                case StateGame.Ship:
                    if (Walls != null)
                    {
                        foreach (var wall in Walls)
                        {
                            wall.Update(gameTime);
                        }
                    }
                    break;
            }

            if (BlackHole != null && Collides.CollideBlackHole(Player, BlackHole))
                Death();
            if (Stars!=null)
                foreach (var star in Stars)
                    star.Update(gameTime);

            Player.Update(gameTime);
            if (Player.turnState != Player.Turn.None)
            {
                UpdateShells(gameTime);
                UpdateMobs(gameTime);
            }

            if (Bonuses != null)
                UpdateBonuses(gameTime);

            UpdateEffectsBulletHit(gameTime);

            // if (KeyboardState.CommandUp(Keys.Escape))
            //     StateProcess = State.Pause;
        }

        private void UpdateMusic(GameTime gameTime)
        {
            if (StartMusic)
            {
                if (IndexLevel == 16 && MusicSwich == 0)
                {
                    MusicSwich++;
                    StartMusic = false;
                    Game.NextMusic = Content.Load<Song>("Pages/GamePlay/SpecialLevel3/Op");
                }
                else
                {
                    Music = (IndexLevel == 9) ? Content.Load<Song>("Pages/GamePlay/SpecialLevel1/Music") :
                            (IndexLevel == 16) ? Content.Load<Song>("Pages/GamePlay/SpecialLevel3/MainMusic") :
                            (IndexLevel == 25) ? Content.Load<Song>("Pages/GamePlay/SpecialLevel2/Music2") :
                                                 Content.Load<Song>("Pages/GamePlay/Music");
                    Game.NextMusic = Music;
                    StartMusic = false;
                }
            }
            else
            {
                playTime += (float)gameTime.ElapsedGameTime.TotalSeconds;
                timeMusic += (float)gameTime.ElapsedGameTime.TotalSeconds;
            }

            if (timeMusic > 27f || MediaPlayer.State == MediaState.Paused)
            {
                MediaPlayer.Play(Game.CurrentMusic);
                timeMusic = 0;
            }

            if (timeMusic > 6 && IndexLevel == 16 && MusicSwich==1)
            {
                StartMusic = true;
                MusicSwich++;
            }
        }

        private void UpdateBonuses(GameTime gameTime)
        {
            for (int i =0;i<Bonuses.Count;i++)
            {
                Bonuses[i].Update(gameTime);
                if (Collides.Collide(Player, Bonuses[i]))
                {
                    Bonuses[i].ActivateBonus();
                    Bonuses.RemoveAt(i);
                    i--;
                }
            }
        }

        private void UpdateMobs(GameTime gameTime)
        {
            for (int k = 0; k < HostileMobs.Count; k++)
            {
                HostileMobs[k].Update(gameTime);
                if (Collides.Collide(Player, HostileMobs[k]))
                {
                    var hpPlayer = Player.HPBar;
                    HostileMobs[k].WasHit(Player);
                    HostileMobs[k].WasShot(hpPlayer);
                }

                foreach (var shell in Shells)
                     shell.WasHit(HostileMobs[k]);

                if (HostileMobs[k].Dead)
                {
                    HostileMobs.RemoveAt(k);
                    k--;
                }
            }
        }

        private void UpdateEffectsBulletHit(GameTime gameTime)
        {
            for (int i = 0; i < EffectsBulletHit.Count; i++)
            {
                EffectsBulletHit[i].Update(gameTime);
                if (EffectsBulletHit[i].End)
                {
                    EffectsBulletHit.RemoveAt(i);
                    i--;
                }
            }
        }

        private void UpdateShells(GameTime gameTime)
        {
            for (int i = 0; i < Shells.Count; i++)
            {
                Shells[i].Update(gameTime);

                if (Shells[i].Dead)
                {
                    Shells.RemoveAt(i);
                    i--;
                }
                else
                    Shells[i].WasHit(Player);
            }
        }

        private void UpdateLife()
        {
            var hp = Player.HPBar;
            while (hp % 10 != 0)
            {
                hp++;
            }
            while (hp / 10 > LifeIcons.Count)
            {
                var life = new Vector2(1560, 40) + new Vector2(35, 0) * LifeIcons.Count;
                LifeIcons.Push(new Sprite(Content.Load<Texture2D>("Pages/GamePlay/LifeIcon"),life));
            }
            while(hp / 10 < LifeIcons.Count && Player.HPBar>0)
            {
                LifeIcons.Pop();
            }
            while (10 != LifeIcons.Count)
            {
                var life = new Vector2(1560, 40) + new Vector2(35, 0) * LifeIcons.Count;
                LifeIcons.Push(new Sprite(Content.Load<Texture2D>("Pages/GamePlay/LifeIconNone"), life));
            }
        }

        protected void Save()
        {
            var time = (float)Math.Round(playTime,2);
            Game.DataManager.AddScore(Name, time);
            CompliteMenu.СurrentTime = time.ToString();
            CompliteMenu.BestTime = Game.DataManager.GetBestScore(Name);
            StateProcess = State.LevelComplite;
        }

        public void Death()
        {
            StateProcess = State.Death;
            Game.DataManager.AddDeath(Name);
            DeathCount = Game.DataManager.GetAllDeath(Name);
        }
    }
}
