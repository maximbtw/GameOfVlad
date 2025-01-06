using GameOfVlad.Scenes.Game;
using GameOfVlad.Scenes.MainMenu;
using GameOfVlad.Scenes.Map;
using GameOfVlad.Tools.Keyboards;
using Microsoft.Extensions.DependencyInjection;

namespace GameOfVlad.Scenes;

public static class DependencyInjectionExtension
{
    public static void RegisterScenes(this IServiceCollection services)
    {
        services.AddTransient(_ => new KeyboardStateObserver());
        
        services.AddScoped(serviceProvider => new MainMenuScene(serviceProvider));
        services.AddScoped(serviceProvider => new MapScene(serviceProvider));
        services.AddScoped(serviceProvider => new GameScene(serviceProvider));
    }
}