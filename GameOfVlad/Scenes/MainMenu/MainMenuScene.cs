﻿using GameOfVlad.Audio;
using GameOfVlad.Services.Game;
using GameOfVlad.Services.Scene;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Input;

namespace GameOfVlad.Scenes.MainMenu;

public partial class MainMenuScene(ContentManager contentManager) : SceneBase(contentManager), IScene
{
    public SceneType Type => SceneType.MainMenu;
    
    private ISceneService SceneService => this.ContentManager.ServiceProvider.GetRequiredService<ISceneService>();
    private IGameService GameService => this.ContentManager.ServiceProvider.GetRequiredService<IGameService>();
    
    public IAudioLoader GetAudioLoader() => new MainMenuSceneAudioLoader(this.ContentManager);

    protected override void LoadCore()
    {
        this.AudioService.PlayMusic(Music.MainMenu_01);
        
        this.KeyboardInput.KeyUp += e =>
        {
            if (e.Key == Keys.Escape)
            {
                this.GameService.ExitGame();
            }
            
            if (e.Key == Keys.Enter)
            {
                this.SceneService.PushScene(SceneType.Game);
            }
        };
    }
}