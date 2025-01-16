using GameOfVlad.GameObjects.UI.Interfaces;
using GameOfVlad.GameRenderer;
using GameOfVlad.Services.Camera;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;

namespace GameOfVlad.GameObjects.UI;

public abstract class UiComponent(ContentManager contentManager) : GameObject(contentManager)
{
    public virtual Vector2 PositionByCamera
    {
        get
        {
            if (this.Parent != null)
            {
                Vector2 parentPosition = ((IUiComponent)this.Parent).PositionByCamera;

                return new Vector2(parentPosition.X + this.Position.X, parentPosition.Y + this.Position.Y);
            }

            return CameraService.PositionByCamera(this.Position);
        }
    }

    public override Vector2 CenterPosition => this.PositionByCamera + this.Origin;
}