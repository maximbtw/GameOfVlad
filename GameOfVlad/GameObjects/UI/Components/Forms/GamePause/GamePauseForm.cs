using System;
using System.Collections.Generic;
using GameOfVlad.GameObjects.UI.Components.ButtonComponent;
using GameOfVlad.GameObjects.UI.Interfaces;
using GameOfVlad.Utils.Camera;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace GameOfVlad.GameObjects.UI.Components.Forms.GamePause;

public class GamePauseForm(IServiceProvider serviceProvider) : UiComponentBase(serviceProvider), ICameraBoundUiComponent
{
    public int DrawOrder => (int)DrawOrderType.FrontCanvas;
    public int UpdateOrder => 1;

    public override IEnumerable<IGameObject> Children
    {
        get
        {
            yield return this.BtnContinueGame;
            yield return this.BtnRestartGame;
            yield return this.BtnToMapScene;
            yield return this.BtnToSettingsScene;
            yield return this.BtnToMainMenuScene;
        }
        set => throw new NotSupportedException();
    }

    public Button BtnContinueGame { get; private set; }
    public Button BtnRestartGame { get; private set; }
    public Button BtnToMapScene { get; private set; }
    public Button BtnToSettingsScene { get; private set; }
    public Button BtnToMainMenuScene { get; private set; }
    
    public override void Init(ContentManager content)
    {
        this.Texture = content.Load<Texture2D>("Interfaces/Pause/Backgraund");
        //this.Position = new Vector2(Settings.ScreenWidth / 2 - 345, Settings.ScreenHeight / 2 - 400);

        InitButtons(content);
        
        base.Init(content);
    }

    public void UpdateByCamera(GameTime gameTime, Camera camera)
    {
      
    }

    private void InitButtons(ContentManager content)
    {
        this.BtnContinueGame = new Button(this.ServiceProvider)
        {
            Texture = content.Load<Texture2D>("Buttons/ContinueInMenu"),
            Position = new Vector2(this.Position.X + 125, this.Position.Y + 175),
            Parent = this
        };

        this.BtnRestartGame = new Button(this.ServiceProvider)
        {
            Texture = content.Load<Texture2D>("Buttons/Restart"),
            Position = new Vector2(this.Position.X + 173, this.Position.Y + 300),
            Parent = this
        };

        this.BtnToMapScene = new Button(this.ServiceProvider)
        {
            Texture = content.Load<Texture2D>("Buttons/Levels"),
            Position = new Vector2(this.Position.X + 183, this.Position.Y + 425),
            Parent = this
        };

        this.BtnToSettingsScene = new Button(this.ServiceProvider)
        {
            Texture = content.Load<Texture2D>("Buttons/SettingInMenu"),
            Position = new Vector2(this.Position.X + 175, this.Position.Y + 550),
            Parent = this
        };

        this.BtnToMainMenuScene = new Button(this.ServiceProvider)
        {
            Texture = content.Load<Texture2D>("Buttons/MainMenu"),
            Position = new Vector2(this.Position.X + 190, this.Position.Y + 675),
            Parent = this
        };
    }
}