using GameOfVlad.Scenes;

namespace GameOfVlad.Game;

public interface ILevelCanvas : ICanvas
{
    void HideCanvas();

    void ShowCanvas();
}