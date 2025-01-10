using GameOfVlad.GameRenderer;
using GameOfVlad.Utils;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace GameOfVlad.GameObjects;

public interface IGameObject : IRendererObject
{
    Texture2D Texture { get; set; }
    
    public Vector2 Position { get; set; }
    
    public Vector2 Origin { get; }
    
    public Size Size { get; set; }
    
    public Rectangle? SourceRectangle { get; }
    
    public Color Color { get; set; }
    
    public float Rotation { get; set; }
    
    public Vector2 Scale { get; set; }
    
    public SpriteEffects SpriteEffects { get; set; }
    
    public float LayerDepth { get; set; }
    
    public Vector2 DrawPosition { get; }
}