using GameOfVlad.Scenes;

namespace GameOfVlad.Services.Scene;

public interface ISceneService
{
    void PushScene(SceneType sceneType);

    void PopScene();
} 