using System.Collections.Generic;
using GameOfVlad.GameObjects.UI.Components.Forms.GamePause;
using GameOfVlad.GameObjects.UI.Interfaces;
using Microsoft.Xna.Framework.Content;

namespace GameOfVlad.Scenes.Game;

public partial class GameScene
{
    private GamePauseForm _gamePauseForm;
    
    protected override IEnumerable<IUiComponent> InitUiComponentsCore(ContentManager content)
    {
        _gamePauseForm = CreatePauseForm(content);

        yield return _gamePauseForm;
    }

    private void OnGameStateChanged(object sender, GameStateChangeEventArgs e)
    {
        VisiblyUiUpdate(e.GameState);
    }
    
    private void VisiblyUiUpdate(GameState gameState)
    {
        _gamePauseForm.IsActive = gameState == GameState.Pause;
    }

    private GamePauseForm CreatePauseForm(ContentManager content)
    {
        var form = new GamePauseForm(this.ServiceProvider);
        form.Init(content);

        form.BtnContinueGame.OnBtnClick += () => _stateManager.SetState(GameState.Play);
        form.BtnRestartGame.OnBtnClick += () => { }; // TODO: how
        form.BtnToMapScene.OnBtnClick += () => this.SceneService.SetScene(SceneType.Map);
        form.BtnToSettingsScene.OnBtnClick += () => { };
        form.BtnToMainMenuScene.OnBtnClick += () => this.SceneService.SetScene(SceneType.MainMenu);
        
        return form;
    }
}