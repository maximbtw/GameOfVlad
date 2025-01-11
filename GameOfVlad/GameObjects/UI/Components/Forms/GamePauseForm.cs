using System;
using System.Collections.Generic;
using GameOfVlad.GameObjects.UI.Components.ButtonComponent;
using GameOfVlad.GameObjects.UI.Interfaces;
using GameOfVlad.GameRenderer;
using GameOfVlad.Utils;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace GameOfVlad.GameObjects.UI.Components.Forms;

public class GamePauseForm(ContentManager contentManager) : UiComponent(contentManager), IUiComponent
{
    public int DrawOrder => (int)DrawOrderType.FrontCanvas;
    public int UpdateOrder => 1;

    public Button BtnContinueGame { get; } = new(contentManager);
    public Button BtnRestartLevel { get; } = new(contentManager);
    public Button BtnToMapScene { get; } = new(contentManager);
    public Button BtnToSettingsScene { get; } = new(contentManager);
    public Button BtnToMainMenuScene { get; } = new(contentManager);
    
    public override IEnumerable<IRendererObject> ChildrenAfter 
    {
        get
        {
            yield return this.BtnContinueGame;
            yield return this.BtnRestartLevel;
            yield return this.BtnToMapScene;
            yield return this.BtnToSettingsScene;
            yield return this.BtnToMainMenuScene;   
        }
        set => throw new NotSupportedException();
    }

    protected override void LoadCore()
    {
        this.Texture = this.ContentManager.Load<Texture2D>("Interfaces/Pause/Backgraund");

        var graphicsDeviceService = this.ContentManager.ServiceProvider.GetRequiredService<IGraphicsDeviceService>();
        
        this.Position = GameHelper.CenterObjectOnScreen(this.Texture, graphicsDeviceService.GraphicsDevice.Viewport);

        InitButtons();

        base.LoadCore();
    }
    
    private void InitButtons()
    {
        this.BtnContinueGame.Texture = this.ContentManager.Load<Texture2D>("Buttons/ContinueInMenu");
        this.BtnContinueGame.Position = new Vector2(125, 175);
        this.BtnContinueGame.Parent = this;

        this.BtnRestartLevel.Texture = this.ContentManager.Load<Texture2D>("Buttons/Restart");
        this.BtnRestartLevel.Position = new Vector2(173, 300);
        this.BtnRestartLevel.Parent = this;

        this.BtnToMapScene.Texture = this.ContentManager.Load<Texture2D>("Buttons/Levels");
        this.BtnToMapScene.Position = new Vector2(183, 425);
        this.BtnToMapScene.Parent = this;

        this.BtnToSettingsScene.Texture = this.ContentManager.Load<Texture2D>("Buttons/SettingInMenu");
        this.BtnToSettingsScene.Position = new Vector2(175, 550);
        this.BtnToSettingsScene.Parent = this;

        this.BtnToMainMenuScene.Texture = this.ContentManager.Load<Texture2D>("Buttons/MainMenu");
        this.BtnToMainMenuScene.Position = new Vector2(190, 675);
        this.BtnToMainMenuScene.Parent = this;
    }
}