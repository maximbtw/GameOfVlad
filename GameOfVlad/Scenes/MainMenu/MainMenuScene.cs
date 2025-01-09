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

public partial class MainMenuScene(ContentManager contentManager) : SceneBase(contentManager), IScene
{
    public SceneType Type => SceneType.MainMenu;
    
    private ISceneService SceneService => this.ContentManager.ServiceProvider.GetRequiredService<ISceneService>();
    private IGameService GameService => this.ContentManager.ServiceProvider.GetRequiredService<IGameService>();
    private ICameraService CameraService => this.ContentManager.ServiceProvider.GetRequiredService<ICameraService>();
    private IMouseService MouseService => this.ContentManager.ServiceProvider.GetRequiredService<IMouseService>();

    protected override void LoadCore()
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

    protected override IEnumerable<IGameGameObject> InitInitGameGameObjectsCore()
    {
        yield break;
    }
}