using System.Collections.Generic;
using GameOfVlad.Audio;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Media;

namespace GameOfVlad.Scenes.Game;

public class GameSceneAudioLoader(ContentManager content) : IAudioLoader
{
    public IEnumerable<(Music, Song)> LoadMusic()
    {
        yield return (Music.Game_001, content.Load<Song>("2025/Audio/Musics/music-game-001"));
    }

    public IEnumerable<(Sound, SoundEffect)> LoadSounds()
    {
        yield return (Sound.Weapon_PlasmaBlaster_Shoot, LoadSound("sound-weapon-PlasmaBlaster-shoot"));
        yield return (Sound.Enemy_Forkfighter_Hit, LoadSound("sound-enemy-forkfighter-hit"));
    }

    private SoundEffect LoadSound(string name) => content.Load<SoundEffect>($"2025/Audio/Sounds/{name}");
}