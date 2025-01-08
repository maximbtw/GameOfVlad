using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace GameOfVlad.Services.Camera;

public class CameraService(Utils.Camera.Camera camera) : ICameraService
{
    public void SetCameraPosition(Vector2 position, GraphicsDevice graphicsDevice)
    {
        position.X -= Settings.ScreenWidth / 2;
        position.Y -= Settings.ScreenHeight / 2;
        camera.Position = position;
    }

    public void ResetCamera()
    {
        camera.Position = Vector2.Zero;
    }
}