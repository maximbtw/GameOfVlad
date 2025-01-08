using Microsoft.Xna.Framework;

namespace GameOfVlad.Entities.Interfaces;

public interface ILevelBorderRestrictedGameObject : IColliderGameObject
{
    void OnLevelBorderCollision(Vector2 collisionNormal);
}