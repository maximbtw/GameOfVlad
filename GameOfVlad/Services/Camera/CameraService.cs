using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace GameOfVlad.Services.Camera;

public class CameraService(Utils.Camera.Camera camera) : ICameraService
{
    public void SetCameraPosition(Vector2 position)
    {
        position.X -= Settings.ScreenWidth / 2;
        position.Y -= Settings.ScreenHeight / 2;
        camera.Position = position;
    }

    public Vector2 PositionByCamera(Vector2 position)
    {
        Matrix inverseView = Matrix.Invert(camera.View);
        
        return Vector2.Transform(position, inverseView);
    }
}