using GameOfVlad.Utils;

namespace GameOfVlad.Services.Game;

public class GameService(GameOfVlad game, FpsUpdater fpsUpdater) : IGameService
{
    public void ExitGame()
    {
        game.Exit();
    }

    public int GetCurrentFps()
    {
        return fpsUpdater.Fps;
    }
}