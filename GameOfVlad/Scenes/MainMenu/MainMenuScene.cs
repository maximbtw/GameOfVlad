using System;
using System.Collections.Generic;
using GameOfVlad.GameObjects.Entities.Interfaces;
using GameOfVlad.Services.Game;
using GameOfVlad.Services.Scene;
using GameOfVlad.Utils.Keyboards;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Input;

namespace GameOfVlad.Scenes.MainMenu;

public partial class MainMenuScene(IServiceProvider serviceProvider) : SceneBase(serviceProvider), IScene
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

    protected override IEnumerable<IGameGameObject> InitInitGameGameObjectsCore(ContentManager content)
    {
        yield break;
    }
}