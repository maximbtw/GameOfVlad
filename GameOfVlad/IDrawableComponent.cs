using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace GameOfVlad;

public interface IDrawableComponent
{
    int DrawOrder { get; }
    
    void Draw(GameTime gameTime, SpriteBatch spriteBatch);
}