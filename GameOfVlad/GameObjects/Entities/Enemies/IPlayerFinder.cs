using GameOfVlad.GameObjects.Entities.Player;
using GameOfVlad.GameRenderer;

namespace GameOfVlad.GameObjects.Entities.Enemies;

public interface IPlayerFinder : IRendererObject
{
    void UpdateByPlayer(PlayerV2 player);
}