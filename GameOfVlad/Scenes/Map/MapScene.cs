using GameOfVlad.Audio;
using GameOfVlad.Services.Scene;
using GameOfVlad.Services.Storage;
using GameOfVlad.Utils.Keyboards;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Input;

namespace GameOfVlad.Scenes.Map;

public partial class MapScene(ContentManager contentManager) : SceneBase(contentManager), IScene
{
    public SceneType Type => SceneType.Map;
    
    private ISceneService SceneService => this.ContentManager.ServiceProvider.GetRequiredService<ISceneService>();
    private IStorageService StorageService => this.ContentManager.ServiceProvider.GetRequiredService<IStorageService>();
    
    public IAudioLoader GetAudioLoader() => new MapSceneAudioLoader(this.ContentManager);

    protected override void LoadCore()
    {
        this.KeyboardInput.KeyUp += HandleKeyUp;
    }
    
    private void HandleKeyUp(KeyEventArgs e)
    {
        if (e.Key == Keys.Escape)
        {
            this.SceneService.PopScene();
        }
    }
}