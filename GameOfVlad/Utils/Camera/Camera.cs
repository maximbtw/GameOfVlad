using Microsoft.Xna.Framework;

namespace GameOfVlad.Utils.Camera;

public class Camera
{
    public Matrix View { get; private set; }
    
    public Vector2 Position { get; set; } = Vector2.Zero;
    public float Zoom { get; private set; } = 1f; // Масштаб
    public float Rotation { get; private set; } = 0f; // Поворот камеры
    
    public void Update()
    {
        View = Matrix.CreateTranslation(new Vector3(-Position.X, -Position.Y, 0)) *
               Matrix.CreateRotationZ(Rotation) *
               Matrix.CreateScale(Zoom);
        
    }
    
    public void ResetCamera() => this.Position = Vector2.Zero;
}