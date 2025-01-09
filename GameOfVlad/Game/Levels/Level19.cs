using System.Collections.Generic;
using GameOfVlad.OldProject;
using GameOfVlad.OldProject.GameEffects;
using GameOfVlad.OldProject.Interfaces;
using GameOfVlad.OldProject.Sprites;
using GameOfVlad.OldProject.Sprites.Mobs;
using GameOfVlad.Utils;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace GameOfVlad.Game.Levels
{
    class Level19 : Level
    {
        public Level19(GameOfVlad game, GraphicsDevice graphicsDevice, ContentManager content)
                     : base(game, graphicsDevice, content)
        {
            Backgraund = content.Load<Texture2D>("Pages/GamePlay/Backgroand2");
            LevelSize = new Size(Backgraund.Width, Backgraund.Height);
            StateProcess = State.Play;

            PauseMenu = new PauseMenu(game, content, graphicsDevice, this);
            CompliteMenu = new CompliteMenu(game, content, graphicsDevice, this);
            DeathMenu = new DeathMenu(game, content, graphicsDevice, this);

            Name = "Level21";
            DeathCount = Game.DataManager.GetAllDeath(Name);
            IndexLevel = 21;
            InitializeSprites();
        }

        public override void InitializeSprites()
        {
            Stars = new List<Star>();
            for (int i = 0; i < 6; i++)
                Stars.Add(new Star(Content, this) { B = true });

            Player = new Player(Content, 
                                Content.Load<Texture2D>("Sprite/Rocket/Rocket"),
                                new Vector2(LevelSize.Width - 150, LevelSize.Height / 2 + 75), 
                                this);

            Earth = new Sprite(Content, 
                               Content.Load<Texture2D>("Sprite/Earth/Earth"),
                               new Vector2(LevelSize.Width / 2 - 155, LevelSize.Height / 2 - 30), 
                               this);

            Gravity = new Gravity((size, vector) =>
            {
                var anomaly = new Vector2(Earth.Location.X+Earth.Texture.Width/2, Earth.Location.Y + Earth.Texture.Height/ 2) - vector;
                var d = anomaly.Length();
                if (d > 0)
                    anomaly *= 1 / d;
                return anomaly * -140 * d / (d * d + 1);
            });


            HostileMobs = new List<Mob>
            {
                new Meteorit(Content,Content.Load<Texture2D>("Sprite/Meteorit/Meteorit"),Vector2.Zero,this),
                new Meteorit(Content,Content.Load<Texture2D>("Sprite/Meteorit/Meteorit"),Vector2.Zero,this),
                new Meteorit(Content,Content.Load<Texture2D>("Sprite/Meteorit/Meteorit"),Vector2.Zero,this),
                new Meteorit(Content,Content.Load<Texture2D>("Sprite/Meteorit/Meteorit"),Vector2.Zero,this),
                new SpaceTrash(Content,
                               Content.Load<Texture2D>("Sprite/SpaceTrash/trash2"),
                               new Vector2(LevelSize.Width / 2, LevelSize.Height / 2 - 100),
                               this){
                               Speed=0.1f,
                               HPBar = 60,
                               TurnTime = 25f,
                               State = SpaceTrash.StateDirection.Upright},
                new SpaceTrash(Content,
                               Content.Load<Texture2D>("Sprite/SpaceTrash/trash1"),
                               new Vector2(LevelSize.Width / 2 - 155, LevelSize.Height / 2),
                               this){
                               Speed = 0.15f,
                               Timer = 2.5f,
                               TurnTime = 5,
                               State = SpaceTrash.StateDirection.Horizontally},
                new SpaceTrash(Content,
                               Content.Load<Texture2D>("Sprite/SpaceTrash/trash2"),
                               new Vector2(LevelSize.Width / 2 - 310, LevelSize.Height / 2 + 50),
                               this){
                               Speed=0.1f,
                               HPBar = 60,
                               TurnTime = 25f,
                               Timer = 25f,
                               State = SpaceTrash.StateDirection.Upright},
                new SpaceTrash(Content,
                               Content.Load<Texture2D>("Sprite/SpaceTrash/trash2"),
                               new Vector2(150, 75),
                               this){
                               Speed=3f,
                               HPBar = 60,
                               TurnTime = 4.75f,
                               State = SpaceTrash.StateDirection.Upright},
                new SpaceTrash(Content,
                               Content.Load<Texture2D>("Sprite/SpaceTrash/trash1"),
                               new Vector2(LevelSize.Width / 2 + 250, LevelSize.Height -150),
                               this){
                               Timer = 2f,
                               State = SpaceTrash.StateDirection.Horizontally},
                new Observer(Content,
                             Content.Load<Texture2D>("Sprite/Observer/Left/1"),
                             new Vector2(100,100),
                             this),
                new Mother(Content,Content.Load<Texture2D>("Sprite/Mother/Right/1"),new Vector2(375,370),this),
            };
        }
    }
}
