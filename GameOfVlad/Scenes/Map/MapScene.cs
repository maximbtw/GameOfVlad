using System;
using System.Collections.Generic;
using GameOfVlad.GameObjects.Entities.Interfaces;
using GameOfVlad.Services.Level;
using GameOfVlad.Services.Scene;
using GameOfVlad.Utils.Keyboards;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Input;

namespace GameOfVlad.Scenes.Map;

public partial class MapScene(IServiceProvider serviceProvider) : SceneBase(serviceProvider), IScene
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

    protected override IEnumerable<IGameGameObject> InitInitGameGameObjectsCore(ContentManager content)
    {
        yield break;
    }
}