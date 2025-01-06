using Microsoft.Xna.Framework;

namespace GameOfVlad.UI.Background;

public class Background : UiComponent
{
    public Vector2 Size { get; set; }

    public override int DrawOrder => (int)DrawOrderType.Background;

    protected override Rectangle BoundingBox => new(0, 0, (int)Size.X, (int)Size.Y);
}