using System;
using System.IO;
using System.Linq;
using System.Text.Json;
using GameOfVlad.Game;
using GameOfVlad.Services.Storage.Data;

namespace GameOfVlad.Services.Storage;

public class StorageService : IStorageService
{
    private const string FileName = "Saves.json";

    private readonly StorageData _storageData;

    public StorageService()
    {
        _storageData = LoadStorageFromFileOrCreate();
    }

    public LevelInfo GetLevelInfo(LevelType level)
    {
        return _storageData.LevelInfos.First(x => x.LevelType == level);
    }
    
    public void CompleteLevel(LevelType level, TimeSpan time)
    {
        LevelInfo levelInfo = _storageData.LevelInfos.First(x => x.LevelType == level);

        levelInfo.Completed = true;
        if (levelInfo.BestTime == null || levelInfo.BestTime < time)
        {
            levelInfo.BestTime = time;
        }

        SaveChanges();
    }

    public void IncreaseRetriesInLevel(LevelType level)
    {
        LevelInfo levelInfo = _storageData.LevelInfos.First(x => x.LevelType == level);
        
        levelInfo.RetriesNumber++;
        
        SaveChanges();
    }

    private StorageData LoadStorageFromFileOrCreate()
    {
        if (File.Exists(FileName))
        {
            string json = File.ReadAllText(FileName);

            return JsonSerializer.Deserialize<StorageData>(json);
        }

        return CreateNewStorage();
    }

    private StorageData CreateNewStorage()
    {
        var storage = new StorageData
        {
            LevelInfos = Enum.GetValues(typeof(LevelType)).Cast<LevelType>().Select(x => new LevelInfo
            {
                LevelType = x,
            }).ToList()
        };
        
        File.WriteAllText(FileName, JsonSerializer.Serialize(storage));

        return storage;
    }
    
    private void SaveChanges()
    {
        // Кто-то удалил файл во время игры.
        if (File.Exists(FileName))
        {
            File.WriteAllText(FileName, JsonSerializer.Serialize(_storageData));
            
            return;
        }
        
        CreateNewStorage();

        File.WriteAllText(FileName, JsonSerializer.Serialize(_storageData));
    }
}