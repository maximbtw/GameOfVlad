using Microsoft.Xna.Framework;

namespace GameOfVlad.Services.Mouse;

public interface IMouseService
{
    public bool IsLeftClick();

    public bool IsRightClick();

    public Vector2 GetMousePosition();
}