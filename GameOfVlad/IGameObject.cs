using System.Collections.Generic;
using Microsoft.Xna.Framework.Content;

namespace GameOfVlad;

public interface IGameObject : IUpdateableComponent, IDrawableComponent
{
    IGameObject Parent { get; set; }
    
    IEnumerable<IGameObject> Children { get; set; }
    
    bool IsActive { get; set; }
    
    void Init(ContentManager content);

    void Terminate();
}