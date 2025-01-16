using System.Collections.Generic;
using GameOfVlad.GameObjects.Effects;
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
            UpdateCore(gameTime, castedObj, GetAllCastedObjects(objects));
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

    private IEnumerable<TObject> GetAllCastedObjects(IEnumerable<IRendererObject> objects)
    {
        foreach (IRendererObject obj in objects)
        {
            if (obj is null or IEffectDrawer)
            {
                continue;
            }
            
            if (obj is TObject castedObj)
            {
                yield return castedObj;
            }

            foreach (TObject child in GetAllCastedObjects(obj.Children))
            {
                yield return child;
            }
        }
    }
}