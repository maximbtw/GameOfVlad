using System;
using System.Windows.Forms.VisualStyles;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace GameOfVlad.UI.Button;

public class Button : UiComponent
{
    public Action OnPressed { get; set; } = null!;
    public Action OnHovered { get; set; } = null!;
    public TextComponent Text { get; set; } = null!;
    
    private ButtonInteractionState _state;

    protected override void DrawCore(GameTime gameTime, SpriteBatch spriteBatch)
    {
        if (_state == ButtonInteractionState.Hover)
        {
            var mergedLocation = new Vector2(this.Position.X + 3, this.Position.Y + 3);

            spriteBatch.Draw(this.Texture, mergedLocation, Color.White);
        }
        else
        {
            spriteBatch.Draw(this.Texture, this.Position, Color.White);
        }

        if (this.Text != null)
        {
            Vector2 textSize = this.Text.Font.MeasureString(this.Text.Text);

            var textPosition = new Vector2(
                this.Position.X + (this.Texture.Width - textSize.X) / 2,
                this.Position.Y + (this.Texture.Height - textSize.Y) / 2
            );

            if (_state == ButtonInteractionState.Hover)
            {
                textPosition = new Vector2(textPosition.X + 3, textPosition.Y + 3);
            }

            spriteBatch.DrawString(this.Text.Font, this.Text.Text, textPosition, this.Text.Color);
        }
    }

    protected override void UpdateCore(GameTime gameTime)
    {
        MouseState mouseState = Mouse.GetState();
        if (BoundingBox.Contains(mouseState.X, mouseState.Y))
        {
            if (mouseState.LeftButton == ButtonState.Pressed)
            {
                if (_state != ButtonInteractionState.Pressed)
                {
                    OnPressed?.Invoke();
                }

                _state = ButtonInteractionState.Pressed;
                
                return;
            }

            if (_state != ButtonInteractionState.Hover)
            {
                OnHovered?.Invoke();
            }

            _state = ButtonInteractionState.Hover;
        }
        else
        {
            _state = ButtonInteractionState.Idle;
        }
    }
}