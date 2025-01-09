using GameOfVlad.Scenes.Game;
using GameOfVlad.Scenes.MainMenu;
using GameOfVlad.Scenes.Map;
using Microsoft.Extensions.DependencyInjection;

namespace GameOfVlad.Scenes;

public static class DependencyInjectionExtension
{
    public static void RegisterScenes(this IServiceCollection services)
    {
        services.AddScoped<MainMenuScene>();
        services.AddScoped<MapScene>();
        services.AddScoped<GameScene>();
    }
}