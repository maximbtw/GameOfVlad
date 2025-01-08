using System;
using System.Collections.Generic;
using GameOfVlad.Game;
using GameOfVlad.UI;
using GameOfVlad.UI.Forms.GamePause;
using Microsoft.Xna.Framework.Content;

namespace GameOfVlad.Scenes.Game;

public sealed class GameSceneCanvas(
    IServiceProvider serviceProvider,
    GameSceneStateManager gameStateManager,
    GamePauseFormEventConfiguration gamePauseFormEventConfiguration) : CanvasBase(serviceProvider), ICanvas
{
    private readonly GamePauseForm _gamePauseForm = new(gamePauseFormEventConfiguration);

    protected override void InitCore(ContentManager content)
    {
        _gamePauseForm.Init(content);

        gameStateManager.OnGameStateChanged += OnGameStateChanged;

        VisiblyUiUpdate(gameStateManager.GetState());

        base.InitCore(content);
    }

    protected override IEnumerable<UiComponent> GetUiComponents(ContentManager content)
    {
        ILevel level = gameStateManager.GetLevel();

        foreach (UiComponent levelUiComponent in level.GetCanvas().GetComponents())
        {
            yield return levelUiComponent;
        }

        yield return _gamePauseForm;
    }

    private void VisiblyUiUpdate(GameState gameState)
    {
        _gamePauseForm.IsActive = gameState == GameState.Pause;
        ILevel level = gameStateManager.GetLevel();
        if (level != null)
        {
            ILevelCanvas canvas = level.GetCanvas();
            if (gameState == GameState.Play)
            {
                canvas.ShowCanvas();
            }
            else
            {
                canvas.HideCanvas();
            }
        }
    }

    private void OnGameStateChanged(object sender, GameStateChangeEventArgs e)
    {
        VisiblyUiUpdate(e.GameState);
    }
}