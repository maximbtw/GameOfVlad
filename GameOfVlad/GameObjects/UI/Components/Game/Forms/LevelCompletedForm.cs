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

public class LevelCompletedForm(ContentManager contentManager) : UiComponent(contentManager), IUiComponent
{
    public int DrawOrder => (int)DrawOrderType.FrontCanvas;
    public int UpdateOrder => 1;

    public Button BtnNextLevel { get; } = new(contentManager);
    public Button BtnRestartLevel { get; } = new(contentManager);
    public Button BtnToMainMenuScene { get; } = new(contentManager);

    public override IEnumerable<IRendererObject> ChildrenAfter
    {
        get
        {
            yield return this.BtnNextLevel;
            yield return this.BtnRestartLevel;
            yield return this.BtnToMainMenuScene;
        }
        set => throw new NotSupportedException();
    }

    protected override void LoadCore()
    {
        this.Texture = this.ContentManager.Load<Texture2D>("Interfaces/Complite/Backgraund");

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
        this.BtnNextLevel.Texture = this.ContentManager.Load<Texture2D>("Buttons/ContinueInMenu");
        this.BtnNextLevel.Position = new Vector2(227, 225);
        this.BtnNextLevel.Parent = this;

        this.BtnRestartLevel.Texture = this.ContentManager.Load<Texture2D>( "Buttons/Restart");
        this.BtnRestartLevel.Position = new Vector2(268, 415);
        this.BtnRestartLevel.Parent = this;

        this.BtnToMainMenuScene.Texture = this.ContentManager.Load<Texture2D>( "Buttons/MainMenu");
        this.BtnToMainMenuScene.Position = new Vector2(285, 525);
        this.BtnToMainMenuScene.Parent = this;
    }
}