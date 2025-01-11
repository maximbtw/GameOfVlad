using GameOfVlad.Utils.Mouse;
using Microsoft.Xna.Framework;

namespace GameOfVlad.Services.Mouse;

public class MouseService(MouseInput mouseInput) : IMouseService
{
    public bool IsLeftClick() => mouseInput.IsLeftClick();
    
    public bool IsRightClick() => mouseInput.IsRightClick();
    
    public Vector2 GetMousePosition() => mouseInput.GetMousePosition();
}