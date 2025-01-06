using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace GameOfVlad.UI.Forms.GamePause;

public class GamePauseForm(GamePauseFormEventConfiguration eventConfiguration) : FormBase
{
    protected override void InitCore(ContentManager content)
    {
        this.Texture = content.Load<Texture2D>("Interfaces/Pause/Backgraund");
        this.Position = new Vector2(1920 / 2 - 345, 1080 / 2 - 400);
        
        base.InitCore(content);
    }

    protected override IEnumerable<UiComponent> GetUiComponents(ContentManager content)
    {
        yield return new Button.Button
        {
            Texture = content.Load<Texture2D>("Buttons/ContinueInMenu"),
            Position = new Vector2(this.Position.X + 125, this.Position.Y + 175),
            OnPressed = () => eventConfiguration.OnBackToGameBtnClick?.Invoke()
        };

        yield return new Button.Button
        {
            Texture = content.Load<Texture2D>("Buttons/Restart"),
            Position = new Vector2(this.Position.X + 173, this.Position.Y + 300),
            OnPressed = () => eventConfiguration.OnRestartBtnClick?.Invoke()
        };

        yield return new Button.Button
        {
            Texture = content.Load<Texture2D>("Buttons/Levels"),
            Position = new Vector2(this.Position.X + 183, this.Position.Y + 425),
            OnPressed = () => eventConfiguration.OnToMapBtnClick?.Invoke()
        };

        yield return new Button.Button
        {
            Texture = content.Load<Texture2D>("Buttons/SettingInMenu"),
            Position = new Vector2(this.Position.X + 175, this.Position.Y + 550),
            OnPressed = () => eventConfiguration.OnToSettingsBtnClick?.Invoke()
        };

        yield return new Button.Button
        {
            Texture = content.Load<Texture2D>("Buttons/MainMenu"),
            Position = new Vector2(this.Position.X + 190, this.Position.Y + 675),
            OnPressed = () => eventConfiguration.OnToMainMenuBtnClick?.Invoke()
        };
    }
}