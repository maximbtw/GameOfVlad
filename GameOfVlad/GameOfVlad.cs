using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Media;
using Microsoft.Xna.Framework;
using System;
using GameOfVlad.Scenes;
using GameOfVlad.Services.Camera;
using GameOfVlad.Services.Game;
using GameOfVlad.Services.Graphic;
using GameOfVlad.Services.Level;
using GameOfVlad.Services.Scene;
using GameOfVlad.Utils;
using GameOfVlad.Utils.Camera;
using Microsoft.Extensions.DependencyInjection;

namespace GameOfVlad;

public class GameOfVlad : Microsoft.Xna.Framework.Game
{
    private readonly GraphicsDeviceManager _graphics;
    private readonly SpriteBatch _spriteBatch;
    
    private GameStateManager _gameStateManager;
    private Camera _camera;
        
    // DI-контейнер
    private readonly IServiceProvider _serviceProvider;
        
        
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
        this.Content.RootDirectory = "Content";
        this.Window.ClientSizeChanged += OnClientSizeChanged;
        this.IsMouseVisible = true;
            
        _graphics = new GraphicsDeviceManager(this);
        _graphics.PreferredBackBufferWidth = 1920;
        _graphics.PreferredBackBufferHeight = 1080;
        
        // graphics.IsFullScreen = true;
                
        _graphics.ApplyChanges();
            
        _spriteBatch = new SpriteBatch(this.GraphicsDevice);
        _camera = new Camera(this.GraphicsDevice);
            
        // DI
        var serviceCollection = new ServiceCollection();
        ConfigureServices(serviceCollection);
        _serviceProvider = serviceCollection.BuildServiceProvider();
    }

    private void ConfigureServices(IServiceCollection services)
    {
        services.AddSingleton<IGameService>(new GameService(this));
        services.AddSingleton<IGraphicService>(new GraphicService(this.Content, _graphics));
        services.AddSingleton<ISceneService, SceneService>();
        services.AddSingleton<ILevelService, LevelService>();
        services.AddSingleton<ICameraService>(new CameraService(_camera));
        
        services.RegisterScenes();
    }

    protected override void Initialize()
    {
        var sceneService = _serviceProvider.GetRequiredService<ISceneService>();
        var graphicService = _serviceProvider.GetRequiredService<IGraphicService>();
        var levelService = _serviceProvider.GetRequiredService<ILevelService>();
        levelService.LoadLevelsToCache();
        
        sceneService.SetScene(SceneType.MainMenu);
        
        _gameStateManager = new GameStateManager(sceneService, graphicService);
            
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

    protected override void UnloadContent()
    {
        base.UnloadContent();
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
            
        _gameStateManager?.Update(gameTime);
        _camera.Update();
            
        base.Update(gameTime);
    }

    protected override void Draw(GameTime gameTime)
    {
        this.GraphicsDevice.Clear(new Color(0, 0, 100, 255));
        
        _spriteBatch.Begin(transformMatrix: _camera.View);
            
        _gameStateManager?.Draw(gameTime, _spriteBatch);
            
        _spriteBatch.End();
        
        base.Draw(gameTime);
    }
        
    private void OnClientSizeChanged(object sender, EventArgs e)
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

        }
    }
}