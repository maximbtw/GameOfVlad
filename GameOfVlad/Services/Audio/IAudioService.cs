using GameOfVlad.Audio;
using GameOfVlad.Scenes;

namespace GameOfVlad.Services.Audio;

public interface IAudioService
{
    void Load(IScene scene);

    void Unload();

    void PlayMusic(Music music);

    void PlaySound(Sound sound);
}