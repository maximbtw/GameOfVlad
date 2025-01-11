using System;
using GameOfVlad.Game;
using GameOfVlad.Services.Storage.Data;

namespace GameOfVlad.Services.Storage;

public interface IStorageService
{
     LevelInfo GetLevelInfo(LevelType level);
     
     void CompleteLevel(LevelType level, TimeSpan time);
     
     void IncreaseRetriesInLevel(LevelType level);
}