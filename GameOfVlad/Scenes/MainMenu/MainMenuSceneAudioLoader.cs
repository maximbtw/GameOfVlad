using System.Collections.Generic;
using GameOfVlad.Audio;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Media;

namespace GameOfVlad.Scenes.MainMenu;

public class MainMenuSceneAudioLoader(ContentManager content) : IAudioLoader
{
    public IEnumerable<(Music, Song)> LoadMusic()
    {
        yield return (Music.MainMenu_01, content.Load<Song>("2025/Audio/Musics/music-main-menu-001"));
    }

    public IEnumerable<(Sound, SoundEffect)> LoadSounds()
    {
        yield break;
    }
}