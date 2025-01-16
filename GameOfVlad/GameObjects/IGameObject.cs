using GameOfVlad.GameRenderer;
using GameOfVlad.Utils;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace GameOfVlad.GameObjects;

public interface IGameObject : IRendererObject
{
    float LayerDepth { get; }
    
    Texture2D Texture { get; set; }

    Vector2 Position { get; set; }

    Vector2 Origin { get; }
    
    Size Size { get; }
    
    Vector2 CenterPosition { get; }
    
    Rectangle? SourceRectangle { get; }

    Color Color { get; set; }

    float Rotation { get; set; }

    Vector2 Scale { get; set; }

    SpriteEffects SpriteEffects { get; set; }
}