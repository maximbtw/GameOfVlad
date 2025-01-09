using System;
using System.Collections.Generic;
using GameOfVlad.GameObjects.Entities.Interfaces;
using GameOfVlad.GameRenderer;
using GameOfVlad.GameRenderer.GameObjectRendererModificators;
using GameOfVlad.Services.Camera;
using GameOfVlad.Services.Graphic;
using GameOfVlad.Services.Level;
using GameOfVlad.Services.Mouse;
using GameOfVlad.Services.Scene;
using GameOfVlad.Utils.Keyboards;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Input;

namespace GameOfVlad.Scenes.Game;

public partial class GameScene : SceneBase, IScene
{
    public SceneType Type => SceneType.Game;
    
    private ILevelService LevelService => this.ServiceProvider.GetRequiredService<ILevelService>();
    private ISceneService SceneService => this.ServiceProvider.GetRequiredService<ISceneService>();
    private IGraphicService GraphicService => this.ServiceProvider.GetRequiredService<IGraphicService>();
    private ICameraService CameraService => this.ServiceProvider.GetRequiredService<ICameraService>();
    private IMouseService MouseService => this.ServiceProvider.GetRequiredService<IMouseService>();
    
    private readonly GameSceneStateManager _stateManager;
    public GameScene(IServiceProvider serviceProvider) : base(serviceProvider)
    {
        _stateManager = new GameSceneStateManager(this.GraphicService);
    }

    protected override void LoadCore(ContentManager content)
    {
        _stateManager.OnGameStateChanged += OnGameStateChanged;
        _stateManager.OnLevelChanged += OnLevelChanged;
        
        _stateManager.SetLevel(this.LevelService.GetCurrentLevel());
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

    protected override IEnumerable<IGameGameObject> InitInitGameGameObjectsCore(ContentManager content)
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