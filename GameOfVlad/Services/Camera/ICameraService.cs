using System;
using GameOfVlad.Utils;
using Microsoft.Xna.Framework;

namespace GameOfVlad.Services.Camera;

public interface ICameraService
{
    void SetTargetPosition(Func<Vector2> getTargetPosition);
    
    Vector2 PositionByCamera(Vector2 position);
    
    void ResetCamera();

    Vector2 CenterObjectOnScreen(Vector2 origin);
    
    Vector2 TopLeftCorner(Size size, int offsetX, int offsetY);

    Vector2 TopRightCorner(Size size, int offsetX, int offsetY);

    Vector2 BottomLeftCorner(Size size, int offsetX, int offsetY);

    Vector2 BottomRightCorner( Size size, int offsetX, int offsetY);
}