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
    class Level16 : Level
    {
        public Level16(GameOfVlad game, GraphicsDevice graphicsDevice, ContentManager content)
                     : base(game, graphicsDevice, content)
        {
            Backgraund = content.Load<Texture2D>("Pages/GamePlay/Backgroand2");
            LevelSize = new Size(Backgraund.Width, Backgraund.Height);
            StateProcess = State.Play;

            PauseMenu = new PauseMenu(game, content, graphicsDevice, this);
            CompliteMenu = new CompliteMenu(game, content, graphicsDevice, this);
            DeathMenu = new DeathMenu(game, content, graphicsDevice, this);

            Name = "Level18";
            DeathCount = Game.DataManager.GetAllDeath(Name);
            IndexLevel = 18;
            InitializeSprites();
        }

        public override void InitializeSprites()
        {
            Stars = new List<Star>();
            for (int i = 0; i < 6; i++)
                Stars.Add(new Star(Content, this) { B = true });

            Gravity = new Gravity((size, vector) => new Vector2(0,-0.75f));

            Player = new Player(Content, 
                                Content.Load<Texture2D>("Sprite/Rocket/Rocket"),
                                new Vector2(80, 80), 
                                this);

            Earth = new Sprite(Content, 
                               Content.Load<Texture2D>("Sprite/Earth/Earth"),
                               new Vector2(LevelSize.Width - 230, LevelSize.Height - 250), 
                               this);


            HostileMobs = new List<Mob>
            {
                new Meteorit(Content,Content.Load<Texture2D>("Sprite/Meteorit/Meteorit"),Vector2.Zero,this),
                new Meteorit(Content,Content.Load<Texture2D>("Sprite/Meteorit/Meteorit"),Vector2.Zero,this),
                new Fire(Content,Content.Load<Texture2D>("Pages/GamePlay/Speciallevel2//FireBeam2"),Vector2.Zero,this),
                new Fire(Content,Content.Load<Texture2D>("Pages/GamePlay/Speciallevel2//FireBeam2"),Vector2.Zero,this),
                new SpaceTrash(Content,
                               Content.Load<Texture2D>("Sprite/SpaceTrash/trash1"),
                               new Vector2(LevelSize.Width - 200, LevelSize.Height - 330),
                               this){
                               Speed=0.05f,
                               Timer=7f,
                               State = SpaceTrash.StateDirection.Horizontally},
                new SpaceTrash(Content,
                               Content.Load<Texture2D>("Sprite/SpaceTrash/trash1"),
                               new Vector2(LevelSize.Width - 410, LevelSize.Height - 185),
                               this){
                               Speed=0.05f,
                               TurnTime=9f,
                               State = SpaceTrash.StateDirection.Horizontally},
                new SpaceTrash(Content,
                               Content.Load<Texture2D>("Sprite/SpaceTrash/trash2"),
                               new Vector2(40, LevelSize.Height/2-80),
                               this){
                               Speed=8f,
                               TurnTime=1.75f,
                               HPBar=60,
                               State = SpaceTrash.StateDirection.Horizontally},
                new SpaceTrash(Content,
                               Content.Load<Texture2D>("Sprite/SpaceTrash/trash2"),
                               new Vector2(LevelSize.Width - 210, LevelSize.Height/2-80),
                               this){
                               Speed=8f,
                               TurnTime=1.75f,
                               Timer=2f,
                               HPBar=60,
                               State = SpaceTrash.StateDirection.Horizontally},
                new Observer(Content,
                             Content.Load<Texture2D>("Sprite/Observer/Left/1"),
                             new Vector2(1600,40),
                             this){
                             Speed = 0.75f },
                new Observer(Content,
                             Content.Load<Texture2D>("Sprite/Observer/Left/1"),
                             new Vector2(75,800),
                             this){
                             TimeToShot = 2.35f },
                new Observer(Content,
                             Content.Load<Texture2D>("Sprite/Observer/Left/1"),
                             new Vector2(LevelSize.Width - 280, LevelSize.Height - 310),
                             this){
                             TimeToShot = 2.35f },
            };
        }
    }
}
