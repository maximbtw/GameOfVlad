namespace GameOfVlad.Services.Game;

public class GameService(GameOfVlad game) : IGameService
{
    public void ExitGame()
    {
        game.Exit();
    }
}