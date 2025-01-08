using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace GameOfVlad.GameObjects.Entities.Interfaces;

public interface IGameGameObject : IGameObject
{
    Texture2D Texture { get; set; }
    
    Vector2 Position { get; set; }
    
    Color Color { get; set; }
}