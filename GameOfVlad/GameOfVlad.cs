using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Media;
using Microsoft.Xna.Framework;
using GameOfVlad.Scenes;
using GameOfVlad.Services.Camera;
using GameOfVlad.Services.Game;
using GameOfVlad.Services.Mouse;
using GameOfVlad.Services.Scene;
using GameOfVlad.Services.Storage;
using GameOfVlad.Utils.Keyboards;
using GameOfVlad.Utils.Mouse;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Xna.Framework.Input;

namespace GameOfVlad;

public class GameOfVlad : Microsoft.Xna.Framework.Game
{
    private readonly ResolutionManager _resolutionManager;
    private SpriteBatch _spriteBatch;

    private readonly GameSceneRenderer _gameSceneRenderer;
    private readonly Camera _camera;
    private readonly MouseInput _mouseInput;
    private readonly KeyboardInputObserver _keyboardInputObserver;

    //Music
    public Song CurrentMusic;
    public Song NextMusic;
    public Song BackgraundMusic;
    private float timeMusic = 0;

    public GameOfVlad()
    {
        this.IsMouseVisible = true;

        var graphicsDeviceManager = new GraphicsDeviceManager(this);
        
        _resolutionManager = new ResolutionManager(graphicsDeviceManager);
        _camera = new Camera();
        _mouseInput = new MouseInput();
        _gameSceneRenderer = new GameSceneRenderer();
        _keyboardInputObserver = new KeyboardInputObserver();

        // TODO: в settings
        _keyboardInputObserver.KeyDown += args =>
        {
            if (args.Key == Keys.F10)
            {
                _resolutionManager.SetResolution(1920, 1080, isFullScreen: false);
            }

            if (args.Key == Keys.F11)
            {
                _resolutionManager.SetResolution(2560, 1440, isFullScreen: false);
            }

            if (args.Key == Keys.F12)
            {
                _resolutionManager.ChangeFullScreen();
            }
        };

        ConfigureServices();
    }


    private void ConfigureServices()
    {
        this.Services.AddService(typeof(IGameService), new GameService(this));
        this.Services.AddService(typeof(ISceneService), new SceneService(this.Services, _gameSceneRenderer));
        
        this.Services.AddService(typeof(ICameraService),
            new CameraService(_camera, this.Services.GetRequiredService<IGraphicsDeviceService>()));
        
        this.Services.AddService(typeof(IMouseService), new MouseService(_mouseInput));
        this.Services.AddService(typeof(IStorageService), new StorageService());
    }

    protected override void LoadContent()
    {
        _spriteBatch = new SpriteBatch(this.GraphicsDevice);

        base.LoadContent();
    }

    protected override void Initialize()
    {
        _resolutionManager.SetResolution(2560, 1440, false);

        var sceneService = this.Services.GetRequiredService<ISceneService>();
        sceneService.PushScene(SceneType.MainMenu);


        // BackgraundMusic = Content.Load<Song>("Pages/MainMenu/Music");
        // NextMusic = BackgraundMusic;

        base.Initialize();
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
        _keyboardInputObserver.Update();
        _gameSceneRenderer?.Update(gameTime);

        base.Update(gameTime);
    }

    protected override void Draw(GameTime gameTime)
    {
        this.GraphicsDevice.Clear(Color.White);

        _spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend, transformMatrix: _camera.GetTransformMatrix());

        _gameSceneRenderer?.Draw(gameTime, _spriteBatch);

        _spriteBatch.End();

        base.Draw(gameTime);
    }
    /*

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