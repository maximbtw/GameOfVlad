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

namespace GameOfVlad.GameObjects.UI.Components.Game.Forms;

public class PlayerDeadForm(ContentManager contentManager) : UiComponent(contentManager), IUiComponent
{
    public int DrawOrder => (int)DrawOrderType.FrontCanvas;
    public int UpdateOrder => 1;
    
    public Button BtnRestartLevel { get; } = new(contentManager);
    public Button BtnToMapScene { get; } = new(contentManager);
    public Button BtnToSettingsScene { get; } = new(contentManager);
    public Button BtnToMainMenuScene { get; } = new(contentManager);
    
    public override IEnumerable<IRendererObject> ChildrenAfter 
    {
        get
        {
            yield return this.BtnRestartLevel;
            yield return this.BtnToMapScene;
            yield return this.BtnToSettingsScene;
            yield return this.BtnToMainMenuScene;   
        }
        set => throw new NotSupportedException();
    }

    protected override void LoadCore()
    {
        this.Texture = this.ContentManager.Load<Texture2D>("Interfaces/Dead/Backgraund");

        InitButtons();

        base.LoadCore();
    }

    protected override void UnloadCore()
    {
        this.Position = this.CameraService.CenterObjectOnScreen(this.Origin);
        
        base.UnloadCore();
    }

    private void InitButtons()
    {
        this.BtnRestartLevel.Texture = this.ContentManager.Load<Texture2D>("Buttons/Restart");
        this.BtnRestartLevel.Position = new Vector2(275, 225);
        this.BtnRestartLevel.Parent = this;

        this.BtnToMapScene.Texture = this.ContentManager.Load<Texture2D>("Buttons/Levels");
        this.BtnToMapScene.Position = new Vector2(280, 325);
        this.BtnToMapScene.Parent = this;

        this.BtnToSettingsScene.Texture = this.ContentManager.Load<Texture2D>("Buttons/SettingInMenu");
        this.BtnToSettingsScene.Position = new Vector2(280, 425);
        this.BtnToSettingsScene.Parent = this;

        this.BtnToMainMenuScene.Texture = this.ContentManager.Load<Texture2D>("Buttons/MainMenu");
        this.BtnToMainMenuScene.Position = new Vector2(280, 525);
        this.BtnToMainMenuScene.Parent = this;
    }
}