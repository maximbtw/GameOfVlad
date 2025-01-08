using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace GameOfVlad.Services.Camera;

public interface ICameraService
{
    void SetCameraPosition(Vector2 position, GraphicsDevice graphicsDevice);

    void ResetCamera();
}