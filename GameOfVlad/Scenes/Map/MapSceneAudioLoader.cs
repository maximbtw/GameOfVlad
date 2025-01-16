using System.Collections.Generic;
using GameOfVlad.Audio;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Media;

namespace GameOfVlad.Scenes.Map;

public class MapSceneAudioLoader(ContentManager content) : IAudioLoader
{
    public IEnumerable<(Music, Song)> LoadMusic()
    {
        yield break;
    }

    public IEnumerable<(Sound, SoundEffect)> LoadSounds()
    {
        yield break;
    }
}