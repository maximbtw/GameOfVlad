using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace GameOfVlad.GameObjects.UI.Interfaces;

public interface IUiComponent : IGameObject
{
    Vector2 Position { get; set; }
    
    Texture2D Texture { get; set; }
    
    Color Color { get; set; } 
    
    Rectangle BoundingBox { get; }
}