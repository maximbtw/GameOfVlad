using System;
using GameOfVlad.GameObjects.UI.Interfaces;

namespace GameOfVlad.GameObjects.UI.Components;

public class Image(IServiceProvider serviceProvider) : UiComponentBase(serviceProvider), IUiComponent
{
    public int DrawOrder => (int)DrawOrderType.Background;
    
    public int UpdateOrder => 1;
}