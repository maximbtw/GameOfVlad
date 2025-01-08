using System;
using System.Collections.Generic;
using GameOfVlad.GameObjects.Entities.Interfaces;
using GameOfVlad.Services.Level;
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
    
    private readonly GameSceneStateManager _stateManager;
    public GameScene(IServiceProvider serviceProvider) : base(serviceProvider)
    {
        _stateManager = new GameSceneStateManager(this.GraphicService);
    }

    protected override void InitEndCore(ContentManager content)
    {
        _stateManager.OnGameStateChanged += OnGameStateChanged;
        _stateManager.OnLevelChanged += OnLevelChanged;
        
        _stateManager.SetLevel(this.LevelService.GetCurrentLevel());
        _stateManager.SetState(GameState.Play);
    }

    protected override void BindKeyboardKeys(KeyboardStateObserver keyboard)
    {
        keyboard.KeyPressed += (sender, args) =>
        {
            if (args.Key == Keys.Escape)
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

            if (args.Key == Keys.Q)
            {
                Settings.ShowCollider = !Settings.ShowCollider;
            }
        };
    }

    protected override IEnumerable<IGameGameObject> InitInitGameGameObjectsCore(ContentManager content)
    {
        yield break;
    }
    
    private void OnLevelChanged(object sender, GameLevelChangeEventArgs e)
    {
        ClearModificators();
        
        AddGameObjects(e.NewLevel.GetGameObjects());
        AddRendererModificators(e.NewLevel.GetLevelModificators());
    }
}