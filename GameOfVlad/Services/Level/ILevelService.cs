using GameOfVlad.Game;

namespace GameOfVlad.Services.Level;

public interface ILevelService
{
    void LoadLevelsToCache();
    
    void SetLevel(LevelType levelType);
    
    ILevel GetCurrentLevel();
}