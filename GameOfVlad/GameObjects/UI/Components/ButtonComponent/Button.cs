using System;
using GameOfVlad.GameObjects.Interfaces;
using GameOfVlad.GameObjects.UI.Interfaces;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace GameOfVlad.GameObjects.UI.Components.ButtonComponent;

public class Button(ContentManager contentManager)
    : CollierUiComponent(contentManager), IUiComponent, IClickable, IColliderGameObject
{
    public int UpdateOrder => 1;
    
    public event Action OnBtnClick;
    public ButtonText Text { get; set; }
    public int HoverOffset { get; set; } = 3;

    public Rectangle BoundingBox => this.Texture == null
        ? Rectangle.Empty
        : new Rectangle((int)PositionByCamera.X, (int)PositionByCamera.Y, Texture.Width, Texture.Height);


    private ButtonInteractionState _state;

    public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
    {
        // TODO: В будущем UI текст компонент отдельный и тут Update
        if (this.Text != null)
        {
            Vector2 textSize = this.Text.Font.MeasureString(this.Text.Text);

            var textPosition = new Vector2(
                this.Position.X + (this.Texture.Width - textSize.X) / 2,
                this.Position.Y + (this.Texture.Height - textSize.Y) / 2
            );

            if (_state == ButtonInteractionState.Hover)
            {
                textPosition = new Vector2(textPosition.X + HoverOffset, textPosition.Y + HoverOffset);
            }

            spriteBatch.DrawString(this.Text.Font, this.Text.Text, textPosition, this.Text.Color);
        }

        base.Draw(gameTime, spriteBatch);
    }

    public void OnHoverEnter()
    {
        this.Position = new Vector2(this.Position.X + HoverOffset, this.Position.Y + HoverOffset);
        _state = ButtonInteractionState.Hover;
    }

    public void OnHoverExit()
    {
        this.Position = new Vector2(this.Position.X - HoverOffset, this.Position.Y - HoverOffset);
        _state = ButtonInteractionState.Idle;
    }

    public bool IsCursorOver(Vector2 cursorPosition)
    {
        return this.BoundingBox.Contains(cursorPosition);
    }

    public void OnClick()
    {
        OnBtnClick?.Invoke();
    }
}