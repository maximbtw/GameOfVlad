using System.Collections.Generic;
using GameOfVlad.OldProject;
using GameOfVlad.OldProject.GameEffects;
using GameOfVlad.OldProject.Interfaces;
using GameOfVlad.OldProject.Sprites;
using GameOfVlad.OldProject.Sprites.Bonuses;
using GameOfVlad.OldProject.Sprites.Mobs;
using GameOfVlad.OldProject.Sprites.Mobs.Boss;
using GameOfVlad.Utils;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace GameOfVlad.Game.Levels
{
    class SpecialLevel3 : Level
    {
        private SpriteFont font;
        public SpecialLevel3(GameOfVlad game, GraphicsDevice graphicsDevice, ContentManager content) 
            : base(game, graphicsDevice, content)
        {
            SpecialLevel = true;
            Backgraund = content.Load<Texture2D>("Pages/GamePlay/Speciallevel2/Backgraund");
            LevelSize = new Size(Backgraund.Width, Backgraund.Height);
            font = content.Load<SpriteFont>("Interfaces/Complite/Font");
            StateProcess = State.Play;

            PauseMenu = new PauseMenu(game, content, graphicsDevice, this);
            CompliteMenu = new CompliteMenu(game, content, graphicsDevice, this);
            DeathMenu = new DeathMenu(game, content, graphicsDevice, this);

            Name = "Level25";
            DeathCount = Game.DataManager.GetAllDeath(Name);
            IndexLevel = 25;

            InitializeSprites();
        }

        public override void InitializeSprites()
        {
            Stars = new List<Star>();
            for (int i = 0; i < 6; i++)
                Stars.Add(new Star(Content, this) { B = true });

            Bonuses = new List<Bonus>();

            Gravity = new Gravity((size, vector) => Vector2.Zero);

            Player = new Player(Content,
                                Content.Load<Texture2D>("Sprite/Rocket/Rocket"),
                                new Vector2(LevelSize.Width / 2, LevelSize.Height - 200),
                                this) {
                                BordarY = LevelSize.Height / 2 };

            HostileMobs = new List<Mob>()
            {
                new SunFish(Content,
                            Content.Load<Texture2D>("Pages/GamePlay/SpecialLevel2/SunFish"),
                            new Vector2(550,230),
                            this),
            };
        }

        public override void DrawPlay(GameTime gameTime, SpriteBatch spriteBatch)
        {
            base.DrawPlay(gameTime, spriteBatch);

            foreach(var mob in HostileMobs)
            {
                if(mob is SunFish)
                {
                    var sunFish = (SunFish)mob;

                    spriteBatch.DrawString(font, sunFish.HPBar.ToString()+ " hp", new Vector2(LevelSize.Width / 2 - 75, 25), Color.DarkRed);
                }
            }
        }
        public override void UpdatePlay(GameTime gameTime)
        {
            base.UpdatePlay(gameTime);

            var availability = false;
            foreach (var mob in HostileMobs)
            {
                if (mob is SunFish)
                {
                    availability = true;
                }
            }
            if (!availability)
                Save();
        }
    }
}
