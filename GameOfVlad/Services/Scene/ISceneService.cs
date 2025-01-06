using GameOfVlad.Scenes;

namespace GameOfVlad.Services.Scene;

public interface ISceneService
{
    void SetScene(SceneType sceneType);
    
    IScene GetCurrentScene();
} 