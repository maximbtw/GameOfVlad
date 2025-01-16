using System.Collections.Generic;
using GameOfVlad.Audio;
using GameOfVlad.Scenes;

namespace GameOfVlad.Services.Audio;

public class AudioService : IAudioService
{
    private readonly Stack<AudioManager> _audioManagers = new();

    public void Load(IScene scene)
    {
        IAudioLoader audioLoader = scene.GetAudioLoader();
        
        var audioManager = new AudioManager(audioLoader);
        
        _audioManagers.Push(audioManager);
    }
    
    public void Unload()
    {
        AudioManager audioManager = _audioManagers.Pop();
        audioManager.StopMusic();
        
        GetAudioManager().ResumeMusic();
    }

    public void PlayMusic(Music music)
    {
        AudioManager audioManager = GetAudioManager();
        
        audioManager.PlayMusic(music);
    }
    
    public void PlaySound(Sound sound)
    {
        AudioManager audioManager = GetAudioManager();
        
        audioManager.PlaySound(sound);
    }

    private AudioManager GetAudioManager() => _audioManagers.Peek();
}