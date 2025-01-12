using System;
using GameOfVlad.Utils;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace GameOfVlad.Services.Camera;

public class CameraService(Camera camera, IGraphicsDeviceService graphicsDeviceService) : ICameraService
{
    public void SetTargetPosition(Func<Vector2> getTargetPosition)
    {
        Vector2 GetCameraPosition()
        {
            Vector2 targetPosition = getTargetPosition();
            Viewport viewport = GetViewport();
            
            // TODO: доработать с учтом scale
            return new Vector2(
                targetPosition.X  - (viewport.Width / 2f) ,
                targetPosition.Y - (viewport.Height / 2f) );
        }

        camera.SetTarget(GetCameraPosition);
    }
    
    public void ResetCamera()
    {
        Viewport viewport = GetViewport();

        var cameraPosition = new Vector2(viewport.Width / 2f, viewport.Height / 2f);

        camera.SetTarget(() => cameraPosition);
    }
    
    public Vector2 PositionByCamera(Vector2 position)
    {
        Matrix inverseView = Matrix.Invert(camera.GetTransformMatrix());
        
        return Vector2.Transform(position, inverseView);
    }

    public Vector2 CenterObjectOnScreen(Vector2 origin)
    {
        Viewport viewport = GetViewport();
        
        return new Vector2(
            viewport.Width / 2f - origin.X * ScreenScale.GlobalScale,
            viewport.Height / 2f - origin.Y * ScreenScale.GlobalScale
        );
    }
    
    public Vector2 TopLeftCorner(Size size, int offsetX, int offsetY)
    {
        return new Vector2(
            offsetX * ScreenScale.GlobalScale, 
            offsetY * ScreenScale.GlobalScale);
    }

    public Vector2 TopRightCorner( Size size, int offsetX, int offsetY)
    {
        Viewport viewport = GetViewport();
    
        return new Vector2(
            viewport.Width - (size.Width * ScreenScale.GlobalScale) + offsetX * ScreenScale.GlobalScale, 
            offsetY * ScreenScale.GlobalScale);
    }

    public Vector2 BottomLeftCorner( Size size, int offsetX, int offsetY)
    {
        Viewport viewport = GetViewport();
    
        return new Vector2(
            offsetX * ScreenScale.GlobalScale, 
            viewport.Height - ( size.Height * ScreenScale.GlobalScale) + offsetY * ScreenScale.GlobalScale);
    }

    public Vector2 BottomRightCorner( Size size, int offsetX, int offsetY)
    {
        Viewport viewport = GetViewport();

        return new Vector2(
            viewport.Width - (size.Width * ScreenScale.GlobalScale) + offsetX * ScreenScale.GlobalScale,
            viewport.Height - ( size.Height * ScreenScale.GlobalScale) + offsetY * ScreenScale.GlobalScale);
    }
    
    private Viewport GetViewport() => graphicsDeviceService.GraphicsDevice.Viewport;
}