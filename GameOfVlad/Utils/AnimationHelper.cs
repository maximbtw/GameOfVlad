using System.Collections.Generic;
using System.Linq;
using GameOfVlad.GameObjects.Effects;
using GameOfVlad.Utils.GameObject;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace GameOfVlad.Utils;

public static class AnimationHelper
{
    /// <summary>
    /// Метод добаляет краткосрочный неподвижный объект, который исчезает по окончанию анимации.
    /// Текстры должны обязательно лежать по пути 2025/Animations/Splash/{textureNumber}/animation-splash-{textureNumber}
    /// </summary>
    public static void AddSplashEffect(
        ContentManager contentManager,
        IEffectDrawer effectDrawer,
        string textureNumber,
        int countTexture,
        Vector2 position,
        Color color,
        Vector2 scale,
        float layerDepth = 1f,
        float timePerFrame = 0.1f)
    {
        Texture2D[] textures = LoadTextures(contentManager, textureNumber, countTexture).ToArray();

        var effect = new AnimatedEffect(contentManager, textures, timePerFrame)
        {
            Position = position,
            Color = color,
            Scale = scale,
            LayerDepth = layerDepth
        };
        
        effectDrawer.AddEffect(effect);
    }

    private static IEnumerable<Texture2D> LoadTextures(
        ContentManager contentManager, 
        string textureNumber,
        int countTexture)
    {
        for (int i = 1; i < countTexture + 1; i++)
        {
            string number = i < 10 ? "0" + i : i.ToString();

            string path = $"2025/Animations/Splash/{textureNumber}/animation-splash-{textureNumber}-{number}";

            yield return contentManager.Load<Texture2D>(path);
        }
    }

    private class AnimatedEffect : GameObjects.GameObject, IEffect
    {
        public int UpdateOrder => 1;

        private readonly TextureAnimation<AnimatedEffect> _textureAnimation;

        public AnimatedEffect(ContentManager contentManager, Texture2D[] textures, float timePerFrame) : base(
            contentManager)
        {
            this.Texture = textures[0];
            _textureAnimation = new TextureAnimation<AnimatedEffect>(gameObject: this, textures, timePerFrame,false);
            _textureAnimation.OnAnimationEnd += Destroy;
        }

        public override void Update(GameTime gameTime)
        {
            _textureAnimation.Update(gameTime);
            
            base.Update(gameTime);
        }
    }
}