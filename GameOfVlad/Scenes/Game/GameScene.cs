using System;
using GameOfVlad.Game;
using GameOfVlad.GameRenderer;
using GameOfVlad.Services.Level;
using GameOfVlad.Services.Scene;
using GameOfVlad.UI.Forms.GamePause;
using GameOfVlad.Utils.Keyboards;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Input;

namespace GameOfVlad.Scenes.Game;

public sealed class GameScene : SceneBase<GameSceneCanvas>, IScene
{
    public SceneType Type => SceneType.Game;
    private ILevelService LevelService => this.ServiceProvider.GetRequiredService<ILevelService>();
    private ISceneService SceneService => ServiceProvider.GetRequiredService<ISceneService>();
    
    private readonly GameSceneStateManager _gameSceneStateManager;

    public GameScene(IServiceProvider serviceProvider) : base(serviceProvider)
    {
        _gameSceneStateManager = new GameSceneStateManager(this.GraphicService);
    }

    protected override void InitBeginCore(ContentManager content)
    {
        _gameSceneStateManager.SetState(GameState.Play);
        _gameSceneStateManager.SetLevel(this.LevelService.GetCurrentLevel());
        
        _gameSceneStateManager.OnGameStateChanged += OnGameStateChanged;
        _gameSceneStateManager.OnLevelChanged += OnLevelChanged;
    }

    protected override void InitEndCore(ContentManager content)
    {
        ILevel level = this.LevelService.GetCurrentLevel();
        
        ApplyLevelToRenderer(level);
        
        base.InitEndCore(content);
    }

    protected override void BindKeyboardKeys(KeyboardStateObserver keyboard)
    {
        keyboard.KeyPressed += (sender, args) =>
        {
            if (args.Key == Keys.Escape)
            {
                GameState state = _gameSceneStateManager.GetState();
                if (state == GameState.Play)
                {
                    _gameSceneStateManager.SetState(GameState.Pause);
                }
            }

            if (args.Key == Keys.Q)
            {
                Settings.ShowCollider = !Settings.ShowCollider;
            }
        };
    }
    
    private void OnGameStateChanged(object sender, GameStateChangeEventArgs e)
    {
        ILevel level = _gameSceneStateManager.GetLevel();
        if (e.GameState == GameState.Play)
        {
            level.Play();
        }
        else
        {
            level.Stop();
        }
    }
    
    private void OnLevelChanged(object sender, GameLevelChangeEventArgs e)
    {
        ILevel level = e.NewLevel;

        ApplyLevelToRenderer(level);
    }

    private void ApplyLevelToRenderer(ILevel level)
    {
        this.Renderer.ClearModificators();

        foreach (IRendererModificator modificator in level.GetLevelModificators())
        {
            this.Renderer.AddModificator(modificator);   
        }
        
        foreach (IGameObject gameObject in level.GetGameObjects())
        {
            this.Renderer.AddGameObject(gameObject);
        }
    }

    protected override GameSceneCanvas GetCanvas()
    {
        var gamePauseFormEventConfiguration = new GamePauseFormEventConfiguration
        {
            OnBackToGameBtnClick = () => _gameSceneStateManager.SetState(GameState.Play),
            OnRestartBtnClick = () => { }, //TODO: how
            OnToMapBtnClick = () => this.SceneService.SetScene(SceneType.Map),
            OnToSettingsBtnClick = () => this.SceneService.SetScene(SceneType.Settings),
            OnToMainMenuBtnClick = () => this.SceneService.SetScene(SceneType.MainMenu),
        };

        return new GameSceneCanvas(this.GraphicService, _gameSceneStateManager, gamePauseFormEventConfiguration);
    }
}