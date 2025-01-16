using System.Collections.Generic;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Media;

namespace GameOfVlad.Audio;

public interface IAudioLoader
{
    IEnumerable<(Music, Song)> LoadMusic();
    
    IEnumerable<(Sound, SoundEffect)> LoadSounds();
}