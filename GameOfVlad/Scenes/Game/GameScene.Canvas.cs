using System.Collections.Generic;
using GameOfVlad.GameObjects.UI.Components.Game.Forms;
using GameOfVlad.GameObjects.UI.Interfaces;

namespace GameOfVlad.Scenes.Game;

public partial class GameScene
{
    private GamePauseForm _gamePauseForm;
    private LevelCompletedForm _levelCompletedForm;
    private PlayerDeadForm _playerDeadForm;
    
    protected override IEnumerable<IUiComponent> InitRenderObjectsCore()
    {
        _gamePauseForm = CreatePauseForm();
        _levelCompletedForm = CreateLevelCompletedForm();
        _playerDeadForm = CreatePlayerDeadForm();

        yield return _gamePauseForm;
        yield return _levelCompletedForm;
        yield return _playerDeadForm;
    }

    private void OnGameStateChanged(object sender, GameStateChangeEventArgs e)
    {
        VisiblyUiUpdate(e.GameState);
    }
    
    private void VisiblyUiUpdate(GameState gameState)
    {
        _gamePauseForm.IsActive = gameState == GameState.Pause;
        _gamePauseForm.Visible = gameState == GameState.Pause;
        
        _levelCompletedForm.IsActive = gameState == GameState.LevelCompleted;
        _levelCompletedForm.Visible = gameState == GameState.LevelCompleted;
        
        _playerDeadForm.IsActive = gameState == GameState.PlayerDead;
        _playerDeadForm.Visible = gameState == GameState.PlayerDead;
    }

    private GamePauseForm CreatePauseForm()
    {
        var form = new GamePauseForm(this.ContentManager);

        form.BtnContinueGame.OnBtnClick += () => _stateManager.SetState(GameState.Play);
        form.BtnRestartLevel.OnBtnClick += () =>
        {
            this.StorageService.IncreaseRetriesInLevel(_stateManager.GetCurrentLevelType());
            
            _stateManager.SetLevel(_stateManager.GetCurrentLevelType());
        };
        form.BtnToMapScene.OnBtnClick += () => this.SceneService.PushScene(SceneType.Map);
        form.BtnToSettingsScene.OnBtnClick += () => { };
        form.BtnToMainMenuScene.OnBtnClick += () => this.SceneService.PopScene();
        
        return form;
    }
    
    private LevelCompletedForm CreateLevelCompletedForm()
    {
        var form = new LevelCompletedForm(this.ContentManager);

        form.BtnNextLevel.OnBtnClick += () => _stateManager.SetLevel(_stateManager.GetNextLevelType());
        form.BtnRestartLevel.OnBtnClick += () => _stateManager.SetLevel(_stateManager.GetCurrentLevelType());
        form.BtnToMainMenuScene.OnBtnClick += () => this.SceneService.PopScene();
        
        return form;
    }
    
    private PlayerDeadForm CreatePlayerDeadForm()
    {
        var form = new PlayerDeadForm(this.ContentManager);
        
        form.BtnRestartLevel.OnBtnClick += () =>
        {
            this.StorageService.IncreaseRetriesInLevel(_stateManager.GetCurrentLevelType());
            
            _stateManager.SetLevel(_stateManager.GetCurrentLevelType());
        };
        form.BtnToMapScene.OnBtnClick += () => this.SceneService.PushScene(SceneType.Map);
        form.BtnToSettingsScene.OnBtnClick += () => { };
        form.BtnToMainMenuScene.OnBtnClick += () => this.SceneService.PopScene();
        
        return form;
    }
}