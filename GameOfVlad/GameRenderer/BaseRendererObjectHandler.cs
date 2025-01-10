using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace GameOfVlad.GameRenderer;

public abstract class BaseRendererObjectHandler<TObject> where TObject : IRendererObject
{
    public void Update(IRendererObject obj, GameTime gameTime)
    {
        if (obj is TObject castedObj)
        {
            UpdateCore(castedObj, gameTime);
        }
    }

    public void Draw(IRendererObject obj, GameTime gameTime, SpriteBatch spriteBatch)
    {
        if (obj is TObject castedObj)
        {
            DrawCore(castedObj, gameTime, spriteBatch);
        }
    }
    
    protected abstract void UpdateCore(TObject obj, GameTime gameTime);
    
    protected abstract void DrawCore(TObject obj, GameTime gameTime, SpriteBatch spriteBatch);
}