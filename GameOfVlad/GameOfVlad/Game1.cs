using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Media;
using Microsoft.Xna.Framework;
using GameOfVlad.Pages;
using GameOfVlad.Tools;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Input.Touch;
using System;

namespace GameOfVlad
{
    public class Game1 : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        //Music
        public Song CurrentMusic;
        public Song NextMusic;
        public Song BackgraundMusic;
        private float timeMusic = 0;
        //
        public DataManager DataManager;
        public DataCenter DataCenter;

        private State currentState;
        private State nextState;
        public void ChangeState(State state)
        {
            nextState = state;
        }

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            graphics.PreferredBackBufferWidth = 1920;
            graphics.PreferredBackBufferHeight = 1080;
            graphics.IsFullScreen = true;
            this.Window.ClientSizeChanged += new EventHandler<EventArgs>(Window_ClientSizeChanged);
            graphics.ApplyChanges();
            Content.RootDirectory = "Content";
        }

        protected override void Initialize()
        {

            ShowCursor(true);
            currentState = new MainMenu(this, graphics.GraphicsDevice, Content);
            BackgraundMusic = Content.Load<Song>("Pages/MainMenu/Music");
            //MediaPlayer.IsMuted = true;
            NextMusic = BackgraundMusic;

            base.Initialize();
        }

        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);
            DataManager = new DataManager();
            DataManager.CreateFile();
            DataCenter = new DataCenter();
            DataCenter.LoadData(this);
        }


        protected override void UnloadContent()
        {

        }

        protected override void Update(GameTime gameTime)
        {
            if (nextState != null)
            {
                currentState = nextState;
                nextState = null;
            }
            currentState.Update(gameTime);
            UpdateSounds(gameTime);
            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);
            currentState.Draw(gameTime, spriteBatch);
            base.Draw(gameTime);
        }
        void Window_ClientSizeChanged(object sender, EventArgs e)
        {
            if (graphics.IsFullScreen)
            {
                graphics.PreferredBackBufferWidth = GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Width;
                graphics.PreferredBackBufferHeight = GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Height;
            }
            else
            {
                graphics.PreferredBackBufferWidth = 1920;
                graphics.PreferredBackBufferHeight = 1080;
            }

            graphics.ApplyChanges();
        }

        public void ShowCursor(bool cursor)
        {
            this.IsMouseVisible = cursor;
        }

        private void UpdateSounds(GameTime gameTime)
        {
            if (NextMusic != null)
            {
                CurrentMusic = NextMusic;
                NextMusic = null;
                MediaPlayer.Stop();
                MediaPlayer.Play(CurrentMusic);
            }
            if (CurrentMusic == BackgraundMusic)
            {
                timeMusic += (float)gameTime.ElapsedGameTime.TotalSeconds;
                if (timeMusic > 18f)
                {
                    MediaPlayer.Play(BackgraundMusic);
                    timeMusic = 0;
                }

            }
        }
    }
}
