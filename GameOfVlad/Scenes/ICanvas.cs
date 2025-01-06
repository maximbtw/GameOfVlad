using System.Collections.Generic;
using GameOfVlad.UI;
using Microsoft.Xna.Framework.Content;

namespace GameOfVlad.Scenes;

public interface ICanvas
{
    void Init(ContentManager content);

    void Terminate();
    
    IEnumerable<UiComponent> GetComponents();
}