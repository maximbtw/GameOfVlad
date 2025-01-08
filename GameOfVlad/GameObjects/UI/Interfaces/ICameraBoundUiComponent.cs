using GameOfVlad.Utils.Camera;
using Microsoft.Xna.Framework;

namespace GameOfVlad.GameObjects.UI.Interfaces;

public interface ICameraBoundUiComponent : IUiComponent
{
    void UpdateByCamera(GameTime gameTime, Camera camera);
}