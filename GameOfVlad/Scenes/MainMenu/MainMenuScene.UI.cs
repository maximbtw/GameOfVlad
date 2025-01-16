using System.Collections.Generic;
using GameOfVlad.Audio;
using GameOfVlad.GameObjects;
using GameOfVlad.GameObjects.UI.Components;
using GameOfVlad.GameObjects.UI.Components.ButtonComponent;
using GameOfVlad.GameObjects.UI.Interfaces;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace GameOfVlad.Scenes.MainMenu;

public partial class MainMenuScene
{
    protected override IEnumerable<IUiComponent> InitRenderObjectsCore()
    {
        var background = new Image(this.ContentManager)
        {
            Texture = this.ContentManager.Load<Texture2D>("Pages/MainMenu/Backgraund"),
            LayerDepth = (float)DrawOrderType.Background / 100f
        };

        var btnStartGame = new Button(this.ContentManager)
        {
            Texture = this.ContentManager.Load<Texture2D>("Buttons/Start"),
            Position = new Vector2(1420, 150),
            LayerDepth = (float)DrawOrderType.UI / 100f
        };

        var btnMap= new Button(this.ContentManager)
        {
            Texture = this.ContentManager.Load<Texture2D>("Buttons/Levels"),
            Position = new Vector2(1410, 300),
            LayerDepth = (float)DrawOrderType.UI / 100f
        };

        var btnMiniGames = new Button(this.ContentManager)
        {
            Texture = this.ContentManager.Load<Texture2D>("Buttons/MiniGames"),
            Position = new Vector2(1320, 450),
            LayerDepth = (float)DrawOrderType.UI / 100f
        };

        var btnGallery = new Button(this.ContentManager)
        {
            Texture = this.ContentManager.Load<Texture2D>("Buttons/Gallery"),
            Position = new Vector2(1410, 600),
            LayerDepth = (float)DrawOrderType.UI / 100f
        };

        var btnSettings = new Button(this.ContentManager)
        {
            Texture = this.ContentManager.Load<Texture2D>("Buttons/Setting"),
            Position = new Vector2(1320, 750),
            LayerDepth = (float)DrawOrderType.UI / 100f
        };

        var btnExit = new Button(this.ContentManager)
        {
            Texture = this.ContentManager.Load<Texture2D>("Buttons/Exit"),
            Position = new Vector2(1420, 900),
            LayerDepth = (float)DrawOrderType.UI / 100f
        };

        btnStartGame.OnBtnClick += () => this.SceneService.PushScene(SceneType.Game);
        btnMap.OnBtnClick += () => this.SceneService.PushScene(SceneType.Map);
        btnExit.OnBtnClick += () => this.GameService.ExitGame();

        yield return background;
        yield return btnStartGame;
        yield return btnMap;
        yield return btnMiniGames;
        yield return btnGallery;
        yield return btnSettings;
        yield return btnExit;
    }
}