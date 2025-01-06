using Microsoft.Xna.Framework;

namespace GameOfVlad;

public interface IUpdateableComponent
{
    int UpdateOrder { get; }
    
    void Update(GameTime gameTime);
}