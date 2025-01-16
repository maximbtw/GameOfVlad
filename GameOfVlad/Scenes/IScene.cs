using GameOfVlad.Audio;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace GameOfVlad.Scenes;

public interface IScene
{
    SceneType Type { get; }
    
    IAudioLoader GetAudioLoader();
    
    void Load();

    void Unload();
    
    void Update(GameTime gameTime);
    
    void Draw(GameTime gameTime, SpriteBatch spriteBatch);
}