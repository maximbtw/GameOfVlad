using System;
using System.Collections.Generic;
using GameOfVlad.GameRenderer;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace GameOfVlad.GameObjects.Effects;

public sealed class EffectDrawer : IEffectDrawer
{
    public int DrawOrder => (int)DrawOrderType.Effect;
    public int UpdateOrder => 1;
    
    public bool Loaded { get; private set; }
    public Guid Guid { get; } = Guid.NewGuid();
    public bool Destroyed { get; set; }
    public bool IsActive { get; set; } = true;
    public bool Visible { get; set; } = true;
    public IRendererObject Parent
    {
        get => null;
        set => throw new NotSupportedException();
    }

    public IEnumerable<IRendererObject> ChildrenBefore
    {
        get => [];
        set => throw new NotSupportedException();
    }

    public IEnumerable<IRendererObject> ChildrenAfter
    {
        get => _effects;
        set => throw new NotSupportedException();
    }

    private readonly List<IEffect> _effects = new();

    public void Load()
    {
        if (this.Loaded)
        {
            throw new Exception("Object is already loaded");
        }
        
        this.Loaded = true;
    }

    public void Unload()
    {
        this.Loaded = false;
    }

    public void Draw(GameTime gameTime, SpriteBatch spriteBatch)
    {
    }

    public void Update(GameTime gameTime)
    {
        _effects.RemoveAll(x => x.Destroyed);
    }

    public void Destroy()
    {
        this.Destroyed = true;
    }

    public void AddEffect(IEffect effect)
    {
       _effects.Add(effect);
    }

    public void ClearEffects()
    {
        _effects.ForEach(x => x.Destroy());
        _effects.Clear();
    }
}