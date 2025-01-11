using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace GameOfVlad.GameRenderer;

public abstract class BaseRendererObjectHandler<TObject> where TObject : IRendererObject
{
    public void Update(GameTime gameTime, IRendererObject obj, IEnumerable<IRendererObject> objects)
    {
        if (obj is TObject castedObj)
        {
            UpdateCore(gameTime, castedObj);
            UpdateCore(gameTime, castedObj, objects.Where(x=>x is TObject).Cast<TObject>());
        }
    }

    public void Draw(GameTime gameTime, SpriteBatch spriteBatch, IRendererObject obj)
    {
        if (obj is TObject castedObj)
        {
            DrawCore(gameTime, spriteBatch, castedObj);
        }
    }

    protected virtual void UpdateCore(GameTime gameTime, TObject obj, IEnumerable<TObject> objects)
    {

    }

    protected virtual void UpdateCore(GameTime gameTime, TObject obj)
    {

    }

    protected virtual void DrawCore(GameTime gameTime, SpriteBatch spriteBatch, TObject obj)
    {

    }
}