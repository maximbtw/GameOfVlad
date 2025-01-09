using System.Collections.Generic;
using GameOfVlad.GameObjects.Entities.Interfaces;
using GameOfVlad.GameRenderer;
using GameOfVlad.GameRenderer.GameObjectRendererModificators;
using GameOfVlad.Services.Camera;
using GameOfVlad.Services.Mouse;
using GameOfVlad.Services.Scene;
using GameOfVlad.Utils.Keyboards;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Input;

namespace GameOfVlad.Scenes.Map;

public partial class MapScene(ContentManager contentManager) : SceneBase(contentManager), IScene
{
    public SceneType Type => SceneType.Map;
    
    private ISceneService SceneService => this.ContentManager.ServiceProvider.GetRequiredService<ISceneService>();
    private ICameraService CameraService => this.ContentManager.ServiceProvider.GetRequiredService<ICameraService>();
    private IMouseService MouseService => this.ContentManager.ServiceProvider.GetRequiredService<IMouseService>();

    protected override void LoadCore()
    {
        this.KeyboardInputObserver.KeyUp += HandleKeyUp;
        
        AddDefaultRendererModificators();
    }

    protected override IEnumerable<IGameGameObject> InitInitGameGameObjectsCore()
    {
        yield break;
    }
    
    private void HandleKeyUp(KeyEventArgs e)
    {
        if (e.Key == Keys.Escape)
        {
            this.SceneService.PopScene();
        }
    }
    
    private void AddDefaultRendererModificators()
    {
        var modificators = new List<IGameObjectRendererModificator>
        {
            new MouseCursorRendererModificator(this.CameraService, this.MouseService)
        };
        
        AddRendererModificators(modificators);
    }
}