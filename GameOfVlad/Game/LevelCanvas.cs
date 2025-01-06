using System.Collections.Generic;
using GameOfVlad.Services.Graphic;
using GameOfVlad.UI;
using Microsoft.Xna.Framework.Content;

namespace GameOfVlad.Game;

public abstract class LevelCanvas(IGraphicService graphicService) : CanvasBase(graphicService)
{
    private readonly List<UiComponent> _hidedComponents = new();

    public void HideCanvas()
    {
        _hidedComponents.ForEach(x=>x.IsActive = false);
    }
    
    public void ShowCanvas()
    {
        _hidedComponents.ForEach(x=>x.IsActive = true);
    }
    
    protected override void InitCore(ContentManager content)
    {
        base.InitCore(content);
        
        foreach (UiComponent uiComponent in GetHideUiComponents(content))
        {
            this.UiComponents.Add(uiComponent);
            _hidedComponents.Add(uiComponent);
        }
    }
    
    protected abstract IEnumerable<UiComponent> GetHideUiComponents(ContentManager content);
}