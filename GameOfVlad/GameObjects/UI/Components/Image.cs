using GameOfVlad.GameObjects.UI.Interfaces;
using Microsoft.Xna.Framework.Content;

namespace GameOfVlad.GameObjects.UI.Components;

public class Image(ContentManager contentManager) : UiComponent(contentManager), IUiComponent
{
    public int DrawOrder => (int)DrawOrderType.Background;
    
    public int UpdateOrder => 1;
}