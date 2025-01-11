using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace GameOfVlad.GameRenderer;

public interface IRendererObjectHandler
{
    void Update(GameTime gameTime, IRendererObject obj, IEnumerable<IRendererObject> objects);
    
    void Draw(GameTime gameTime, SpriteBatch spriteBatch, IRendererObject obj);
}