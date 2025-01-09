using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Media;
using Microsoft.Xna.Framework;
using GameOfVlad.GameRenderer;
using GameOfVlad.OldProject;
using GameOfVlad.Scenes;
using GameOfVlad.Services.Camera;
using GameOfVlad.Services.Game;
using GameOfVlad.Services.Mouse;
using GameOfVlad.Services.Scene;
using GameOfVlad.Utils.Camera;
using GameOfVlad.Utils.Mouse;
using Microsoft.Extensions.DependencyInjection;

namespace GameOfVlad;

public class GameOfVlad : Microsoft.Xna.Framework.Game
{
    private readonly GraphicsDeviceManager _graphics;
    private readonly SpriteBatch _spriteBatch;

    private readonly GameSceneRenderer _gameSceneRenderer;
    private readonly Camera _camera;
    private readonly MouseInput _mouseInput;

    //Music
    public Song CurrentMusic;
    public Song NextMusic;
    public Song BackgraundMusic;

    private float timeMusic = 0;

    //
    public DataManager DataManager;
    public DataCenter DataCenter;

    // private PageStateMachine currentState;
    // private PageStateMachine nextState;
    // public void ChangeState(PageStateMachine state)
    // {
    //     nextState = state;
    // }

    public GameOfVlad()
    {
        //this.Content.RootDirectory = "Content";
        this.IsMouseVisible = true;

        _graphics = new GraphicsDeviceManager(this);
        _graphics.PreferredBackBufferWidth = 1920;
        _graphics.PreferredBackBufferHeight = 1080;

        // graphics.IsFullScreen = true;
        //this.Window.ClientSizeChanged += OnClientSizeChanged;

        _graphics.ApplyChanges();
        
        _spriteBatch = new SpriteBatch(this.GraphicsDevice);
        _camera = new Camera(this.GraphicsDevice);
        _mouseInput = new MouseInput();
        _gameSceneRenderer = new GameSceneRenderer();

        ConfigureServices();
    }
    

    private void ConfigureServices()
    {
        this.Services.AddService(typeof(IGameService), new GameService(this));
        this.Services.AddService(typeof(ISceneService), new SceneService(this.Services, _gameSceneRenderer));
        this.Services.AddService(typeof(ICameraService), new CameraService(_camera));
        this.Services.AddService(typeof(IMouseService),new MouseService(_mouseInput));
    }

    protected override void Initialize()
    {
        var sceneService = this.Services.GetRequiredService<ISceneService>();
        sceneService.PushScene(SceneType.MainMenu);

        // BackgraundMusic = Content.Load<Song>("Pages/MainMenu/Music");
        // NextMusic = BackgraundMusic;

        base.Initialize();
    }

    protected override void LoadContent()
    {
        DataManager = new DataManager();
        DataManager.CreateFile();
        DataCenter = new DataCenter();
        DataCenter.LoadData(this);
    }

    protected override void Update(GameTime gameTime)
    {
        // if (nextState != null)
        // {
        //     currentState = nextState;
        //     nextState = null;
        // }
        // currentState.Update(gameTime);
        // UpdateSounds(gameTime);

        _camera.Update();
        _mouseInput.Update();
        _gameSceneRenderer?.Update(gameTime);

        base.Update(gameTime);
    }

    protected override void Draw(GameTime gameTime)
    {
        this.GraphicsDevice.Clear(Color.White);
        
        _spriteBatch.Begin(transformMatrix: _camera.View);

        _gameSceneRenderer?.Draw(gameTime, _spriteBatch);

        _spriteBatch.End();

        base.Draw(gameTime);
    }

    /*private void OnClientSizeChanged(object sender, EventArgs e)
    {
        if (_graphics.IsFullScreen)
        {
            _graphics.PreferredBackBufferWidth = GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Width;
            _graphics.PreferredBackBufferHeight = GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Height;
        }
        else
        {
            _graphics.PreferredBackBufferWidth = Settings.ScreenWidth;
            _graphics.PreferredBackBufferHeight = Settings.ScreenHeight;
        }

        _graphics.ApplyChanges();
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

        }*/
}