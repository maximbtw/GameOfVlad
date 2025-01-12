using System;
using Microsoft.Xna.Framework;

namespace GameOfVlad.Services.Camera;

public class Camera
{
    private Matrix _transformMatrix;
    private Func<Vector2> _getPosition;
    
    public Matrix GetTransformMatrix() => _transformMatrix;


    public void Update()
    {
        Vector2 position = _getPosition();
        
        _transformMatrix = Matrix.CreateTranslation(new Vector3(-position.X, -position.Y, 0)) *
                Matrix.CreateRotationZ(0f) *
                Matrix.CreateScale(ScreenScale.GlobalScale);

    }

    public void SetTarget(Func<Vector2> getTargetPosition)
    {
        _getPosition = getTargetPosition;
    }
}