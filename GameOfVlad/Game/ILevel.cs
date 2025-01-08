using System.Collections.Generic;
using GameOfVlad.GameRenderer;
using GameOfVlad.Utils;
using Microsoft.Xna.Framework.Content;

namespace GameOfVlad.Game;

public interface ILevel
{
    Size LevelSize { get; }
    
    LevelType LevelType { get; }

    void Init(ContentManager content);

    void Terminate();
    
    ILevelCanvas GetCanvas();
    
    IEnumerable<IGameObject> GetGameObjects();
    
    IEnumerable<IRendererModificator> GetLevelModificators();

    void Play();

    void Stop();
}