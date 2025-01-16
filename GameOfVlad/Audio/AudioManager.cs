namespace GameOfVlad.Audio;

public class AudioManager(IAudioLoader audioLoader)
{
    private readonly MusicManager _musicManager = new(audioLoader);
    private readonly SoundManager _soundManager = new(audioLoader);

    public void PlayMusic(Music music)
    {
        _musicManager.Play(music);
    }
    
    public void StopMusic()
    {
        _musicManager.Stop();
    }
    
    public void ResumeMusic()
    {
        _musicManager.Resume();
    }
    
    public void PlaySound(Sound sound)
    {
        _soundManager.Play(sound);
    }
}