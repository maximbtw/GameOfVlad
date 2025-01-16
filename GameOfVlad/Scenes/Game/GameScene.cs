﻿using GameOfVlad.Audio;
using GameOfVlad.Game;
using GameOfVlad.Services.Scene;
using GameOfVlad.Services.Storage;
using GameOfVlad.Utils.Keyboards;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Input;

namespace GameOfVlad.Scenes.Game;

public partial class GameScene(ContentManager contentManager) : SceneBase(contentManager), IScene
{
    public SceneType Type => SceneType.Game;
    
    private ISceneService SceneService => this.ContentManager.ServiceProvider.GetRequiredService<ISceneService>();
    private IStorageService StorageService => this.ContentManager.ServiceProvider.GetRequiredService<IStorageService>();
    
    private readonly GameSceneStateManager _stateManager = new(contentManager);
    
    public IAudioLoader GetAudioLoader() => new GameSceneAudioLoader(this.ContentManager);

    protected override void LoadCore()
    {
        _stateManager.OnGameStateChanged += OnGameStateChanged;
        _stateManager.OnLevelChanged += OnLevelChanged;
        
        _stateManager.SetLevel(LevelType.Level1);

        this.KeyboardInput.KeyDown += HandleKeyDawn;
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
    }
    
    private void OnLevelChanged(object sender, GameLevelChangeEventArgs e)
    {
        AddRenderObject(e.NewLevel);
    }
}