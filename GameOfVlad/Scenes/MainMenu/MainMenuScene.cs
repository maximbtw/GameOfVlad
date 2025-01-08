using System;
using GameOfVlad.Services.Game;
using GameOfVlad.Services.Scene;
using GameOfVlad.Utils.Keyboards;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Xna.Framework.Input;

namespace GameOfVlad.Scenes.MainMenu;

public sealed class MainMenuScene(IServiceProvider serviceProvider)
    : SceneBase<MainMenuSceneCanvas>(serviceProvider), IScene
{
    public SceneType Type => SceneType.MainMenu;
    
    private ISceneService SceneService => ServiceProvider.GetRequiredService<ISceneService>();
    private IGameService GameService => this.ServiceProvider.GetRequiredService<IGameService>();

    protected override void BindKeyboardKeys(KeyboardStateObserver keyboard)
    {
        keyboard.KeyUnpressed += (_, e) =>
        {
            if (e.Key == Keys.Escape)
            {
                this.GameService.ExitGame();
            }
        };

        keyboard.KeyUnpressed += (_, e) =>
        {
            if (e.Key == Keys.Enter)
            {
                this.SceneService.SetScene(SceneType.Game);
            }
        };
    }

    protected override MainMenuSceneCanvas GetCanvas()
    {
        var canvas = new MainMenuSceneCanvas(this.GraphicService)
        {
            ExitGame = this.GameService.ExitGame,
            SetGameView = () => this.SceneService.SetScene(SceneType.Game),
            SetNewView = this.SceneService.SetScene,
        };

        return canvas;
    }
}