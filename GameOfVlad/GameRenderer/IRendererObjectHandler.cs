using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace GameOfVlad.GameRenderer;

public interface IRendererObjectHandler
{
    void Update(IRendererObject obj, GameTime gameTime);
    
    void Draw(IRendererObject obj, GameTime gameTime, SpriteBatch spriteBatch);
}