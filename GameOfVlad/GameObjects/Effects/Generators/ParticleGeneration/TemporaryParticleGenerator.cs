using System;
using System.Collections.Generic;
using GameOfVlad.GameRenderer;
using GameOfVlad.Utils;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace GameOfVlad.GameObjects.Effects.Generators.ParticleGeneration;

public class TemporaryParticleGenerator(
    IEffectDrawer effectDrawer,
    ParticleGeneratorConfiguration configuration,
    float generationTime)
    : GameObject, IEffect
{
    public int DrawOrder => (int)DrawOrderType.Effect;
    public int UpdateOrder => 1;

    public override IEnumerable<IRendererObject> ChildrenAfter
    {
        get
        {
            yield return _particleGenerator;
        }
        set => throw new NotSupportedException();
    }

    private readonly Timer _timer = new();
    private readonly ParticleGenerator _particleGenerator = new(effectDrawer, configuration);

    public override void Update(GameTime gameTime)
    {
        _timer.Update(gameTime);
        if (_timer.Time >= generationTime)
        {
            this.Destroy();
        }

        base.Update(gameTime);
    }

    public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
    {
    }
}