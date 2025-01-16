using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework.Media;

namespace GameOfVlad.Audio;

public class MusicManager
{
    private readonly Dictionary<Music, Song> _musicCache;
    private Song _lastPlayedSong;
    
    public MusicManager(IAudioLoader audioLoader)
    {
        _musicCache = audioLoader.LoadMusic().ToDictionary(x => x.Item1, x => x.Item2);
    }
    
    public void Play(Music music)
    {
        if(!_musicCache.TryGetValue(music, out Song song))
        {
            throw new Exception($"Music {music} not found");
        }

        _lastPlayedSong = song;
        
        MediaPlayer.Play(song);
        MediaPlayer.IsRepeating = true;
    }

    public void Stop()
    {
        MediaPlayer.Stop();
    }
    
    public void Resume()
    {
        MediaPlayer.Play(_lastPlayedSong);
    }
}