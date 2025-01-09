using System;
using System.Collections.Generic;
using GameOfVlad.GameObjects.Entities.Interfaces;
using GameOfVlad.GameRenderer;
using GameOfVlad.GameRenderer.GameObjectRendererModificators;
using GameOfVlad.Services.Camera;
using GameOfVlad.Services.Level;
using GameOfVlad.Services.Mouse;
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
    private ICameraService CameraService => this.ServiceProvider.GetRequiredService<ICameraService>();
    private IMouseService MouseService => this.ServiceProvider.GetRequiredService<IMouseService>();

    protected override void LoadCore(ContentManager content)
    {
        this.KeyboardInputObserver.KeyUp += HandleKeyUp;
        
        AddDefaultRendererModificators();
    }

    protected override IEnumerable<IGameGameObject> InitInitGameGameObjectsCore(ContentManager content)
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