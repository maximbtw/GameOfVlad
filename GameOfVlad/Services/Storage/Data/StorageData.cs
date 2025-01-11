using System.Collections.Generic;

namespace GameOfVlad.Services.Storage.Data;

public class StorageData
{
    public List<LevelInfo> LevelInfos { get; set; } = new();
    
    public GameplayInfo GameplayInfo { get; set; } = new();
}