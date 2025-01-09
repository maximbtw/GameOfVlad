using System.Collections.Generic;
using GameOfVlad.Game;
using GameOfVlad.Game.Levels;
using GameOfVlad.GameObjects.Entities.Interfaces;
using GameOfVlad.GameRenderer;
using GameOfVlad.GameRenderer.GameObjectRendererModificators;
using GameOfVlad.Services.Camera;
using GameOfVlad.Services.Mouse;
using GameOfVlad.Services.Scene;
using GameOfVlad.Utils.Keyboards;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Input;

namespace GameOfVlad.Scenes.Game;

public partial class GameScene(ContentManager contentManager) : SceneBase(contentManager), IScene
{
    public SceneType Type => SceneType.Game;
    
    private ISceneService SceneService => this.ContentManager.ServiceProvider.GetRequiredService<ISceneService>();
    private ICameraService CameraService => this.ContentManager.ServiceProvider.GetRequiredService<ICameraService>();
    private IMouseService MouseService => this.ContentManager.ServiceProvider.GetRequiredService<IMouseService>();
    
    private readonly GameSceneStateManager _stateManager = new(contentManager);

    protected override void LoadCore()
    {
        _stateManager.OnGameStateChanged += OnGameStateChanged;
        _stateManager.OnLevelChanged += OnLevelChanged;
        
        _stateManager.SetLevel(LevelType.Level1);
        _stateManager.SetState(GameState.Play);

        this.KeyboardInputObserver.KeyDown += HandleKeyDawn;
    }

    private void HandleKeyDawn(KeyEventArgs e)
    {
        if (e.Key == Keys.Escape)
        {
            GameState state = _stateManager.GetState();
            if (state == GameState.Play)
            {
                _stateManager.SetState(GameState.Pause);
            }
            else if (state == GameState.Pause)
            {
                _stateManager.SetState(GameState.Play);
            }
        }

        if (e.Key == Keys.Q)
        {
            Settings.ShowCollider = !Settings.ShowCollider;
        }
    }

    protected override IEnumerable<IGameGameObject> InitInitGameGameObjectsCore()
    {
        yield break;
    }

    private void AddDefaultRendererModificators()
    {
        var modificators = new List<IGameObjectRendererModificator>
        {
            new MouseCursorRendererModificator(this.CameraService, this.MouseService)
        };
        
        AddRendererModificators(modificators);
    }
    
    private void OnLevelChanged(object sender, GameLevelChangeEventArgs e)
    {
        ResetModificators();

        AddDefaultRendererModificators();
        AddGameObjects(e.NewLevel.GetGameObjects());
        AddRendererModificators(e.NewLevel.GetLevelModificators());
    }
}