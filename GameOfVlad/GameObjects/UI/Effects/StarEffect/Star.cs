using GameOfVlad.GameObjects.UI.Interfaces;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;

namespace GameOfVlad.GameObjects.UI.Effects.StarEffect;

/*public class Star(ContentManager contentManager, Rectangle levelBounds) : UiComponentBase(contentManager), IUiComponent
{
    public int DrawOrder => (int)DrawOrderType.Background;
    public int UpdateOrder => 1;
    public Vector2 Velocity { get; set; }
    
    private const int DrawOffset = 1000;

    public override void Update(GameTime gameTime)
    {
        // Обновляем позицию
        this.Position += this.Velocity * (float)gameTime.ElapsedGameTime.TotalSeconds;
        
        if (Position.Y > levelBounds.Height + DrawOffset || Position.X < levelBounds.X - DrawOffset || Position.X > levelBounds.Width + DrawOffset)
        {
            Destroyed = true; 
        }
        
        base.Update(gameTime);
    }
}*/