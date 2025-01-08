using System;
using GameOfVlad.Services.Level;
using GameOfVlad.Services.Scene;
using GameOfVlad.Utils.Keyboards;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Xna.Framework.Input;

namespace GameOfVlad.Scenes.Map;

public sealed class MapScene(IServiceProvider serviceProvider) : SceneBase<MapSceneCanvas>(serviceProvider), IScene
{
    public SceneType Type => SceneType.Map;
    
    private ISceneService SceneService => ServiceProvider.GetRequiredService<ISceneService>();
    private ILevelService LevelService => this.ServiceProvider.GetRequiredService<ILevelService>();

    protected override void BindKeyboardKeys(KeyboardStateObserver keyboard)
    {
        keyboard.KeyUnpressed += (_, e) =>
        {
            if (e.Key == Keys.Escape)
            {
                this.SceneService.SetScene(SceneType.MainMenu);
            }
        };
    }

    protected override MapSceneCanvas GetCanvas()
    {
        return new MapSceneCanvas(this.ServiceProvider)
        {
            SetLevel = level =>
            {
                this.LevelService.SetLevel(level);
                this.SceneService.SetScene(SceneType.Game);
            }
        };
    }
}