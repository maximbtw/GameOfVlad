using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework.Audio;

namespace GameOfVlad.Audio;

public class SoundManager
{
    private readonly Dictionary<Sound, SoundEffect> _soundCache;

    public SoundManager(IAudioLoader audioLoader)
    {
        _soundCache = audioLoader.LoadSounds().ToDictionary(x => x.Item1, x => x.Item2);
    }
    
    public void Play(Sound sound)
    {
        if(!_soundCache.TryGetValue(sound, out SoundEffect soundEffect))
        {
            throw new Exception($"Sound effect {sound} not found");
        }

        soundEffect.Play();
    }
}