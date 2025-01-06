using System;
using System.Collections.Generic;
using GameOfVlad.Services.Graphic;
using GameOfVlad.UI;
using GameOfVlad.UI.Background;
using GameOfVlad.UI.Button;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace GameOfVlad.Scenes.MainMenu;

public sealed class MainMenuSceneCanvas(IGraphicService graphicService) : CanvasBase(graphicService), ICanvas
{
    public Action SetGameView { get; init; }
    public Action<SceneType> SetNewView { get; init; }
    public Action ExitGame { get; init; }
    
    protected override IEnumerable<UiComponent> GetUiComponents(ContentManager content)
    {
        yield return new Background
        {
            Texture = content.Load<Texture2D>("Pages/MainMenu/Backgraund"),
            Size = new Vector2(1920, 1080)
        };

        yield return new Button
        {
            Texture = content.Load<Texture2D>("Buttons/Start"),
            Position = new Vector2(1420, 150),
            OnPressed = () => SetGameView?.Invoke()
        };

        yield return new Button
        {
            Texture = content.Load<Texture2D>("Buttons/Levels"),
            Position = new Vector2(1410, 300),
            OnPressed = () => SetNewView?.Invoke(SceneType.Map)
        };

        yield return new Button
        {
            Texture = content.Load<Texture2D>("Buttons/MiniGames"),
            Position = new Vector2(1320, 450)
        };

        yield return new Button
        {
            Texture = content.Load<Texture2D>("Buttons/Gallery"),
            Position = new Vector2(1410, 600)
        };

        yield return new Button
        {
            Texture = content.Load<Texture2D>("Buttons/Setting"),
            Position = new Vector2(1320, 750)
        };

        yield return new Button
        {
            Texture = content.Load<Texture2D>("Buttons/Exit"),
            Position = new Vector2(1420, 900),
            OnPressed = () => ExitGame?.Invoke()
        };
    }
}