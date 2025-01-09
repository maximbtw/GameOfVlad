using System;
using System.Collections.Generic;
using GameOfVlad.GameObjects.Entities.Interfaces;
using GameOfVlad.GameRenderer;
using GameOfVlad.GameRenderer.GameObjectRendererModificators;
using GameOfVlad.Services.Camera;
using GameOfVlad.Services.Game;
using GameOfVlad.Services.Mouse;
using GameOfVlad.Services.Scene;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Input;

namespace GameOfVlad.Scenes.MainMenu;

public partial class MainMenuScene(IServiceProvider serviceProvider) : SceneBase(serviceProvider), IScene
{
    public SceneType Type => SceneType.MainMenu;
    
    private ISceneService SceneService => ServiceProvider.GetRequiredService<ISceneService>();
    private IGameService GameService => this.ServiceProvider.GetRequiredService<IGameService>();
    private ICameraService CameraService => this.ServiceProvider.GetRequiredService<ICameraService>();
    private IMouseService MouseService => this.ServiceProvider.GetRequiredService<IMouseService>();

    protected override void LoadCore(ContentManager content)
    {
        AddDefaultRendererModificators();
        
        this.KeyboardInputObserver.KeyUp += e =>
        {
            if (e.Key == Keys.Escape)
            {
                this.GameService.ExitGame();
            }
            
            if (e.Key == Keys.Enter)
            {
                this.SceneService.PushScene(SceneType.Game);
            }
        };
    }

    private void AddDefaultRendererModificators()
    {
        var modificators = new List<IGameObjectRendererModificator>
        {
            new MouseCursorRendererModificator(this.CameraService, this.MouseService)
        };
        
        AddRendererModificators(modificators);
    }

    protected override IEnumerable<IGameGameObject> InitInitGameGameObjectsCore(ContentManager content)
    {
        yield break;
    }
}