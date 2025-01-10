using System.Collections.Generic;
using GameOfVlad.GameObjects.UI.Components.Forms.GamePause;
using GameOfVlad.GameObjects.UI.Interfaces;

namespace GameOfVlad.Scenes.Game;

public partial class GameScene
{
    private GamePauseForm _gamePauseForm;
    
    protected override IEnumerable<IUiComponent> InitRenderObjectsCore()
    {
        _gamePauseForm = CreatePauseForm();

        yield return _gamePauseForm;
    }

    private void OnGameStateChanged(object sender, GameStateChangeEventArgs e)
    {
        VisiblyUiUpdate(e.GameState);
    }
    
    private void VisiblyUiUpdate(GameState gameState)
    {
        _gamePauseForm.IsActive = gameState == GameState.Pause;
        _gamePauseForm.Visible = gameState == GameState.Pause;
    }

    private GamePauseForm CreatePauseForm()
    {
        var form = new GamePauseForm(this.ContentManager);

        form.BtnContinueGame.OnBtnClick += () => _stateManager.SetState(GameState.Play);
        form.BtnRestartGame.OnBtnClick += () => { }; // TODO: how
        form.BtnToMapScene.OnBtnClick += () => this.SceneService.PushScene(SceneType.Map);
        form.BtnToSettingsScene.OnBtnClick += () => { };
        form.BtnToMainMenuScene.OnBtnClick += () => this.SceneService.PopScene();
        
        return form;
    }
}