using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace GameOfVlad.Utils.Camera;

public class Camera(GraphicsDevice graphicsDevice)
{
    public Matrix View { get; private set; }

    public Matrix Projection { get; private set; } = Matrix.CreateOrthographicOffCenter(0,
        graphicsDevice.Viewport.Width, graphicsDevice.Viewport.Height, 0, 0, 1);

    public Vector2 Position { get; set; } = Vector2.Zero;
    public float Zoom { get; set; } = 1f; // Масштаб
    public float Rotation { get; set; } = 0f; // Поворот камеры
    
    public void Update()
    {
        // Применяем трансформацию: позиция камеры + масштаб + поворот
        View = Matrix.CreateTranslation(new Vector3(-Position.X, -Position.Y, 0)) *
               Matrix.CreateRotationZ(Rotation) *
               Matrix.CreateScale(Zoom);
    }
}