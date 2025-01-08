using Microsoft.Xna.Framework;

namespace GameOfVlad.GameObjects.Entities.Interfaces;

public interface ILevelBorderRestrictedGameObject : IColliderGameObject
{
    void OnLevelBorderCollision(Vector2 collisionNormal);
}