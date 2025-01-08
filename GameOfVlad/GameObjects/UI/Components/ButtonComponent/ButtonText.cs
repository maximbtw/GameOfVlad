using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace GameOfVlad.GameObjects.UI.Components.ButtonComponent;

public class ButtonText(SpriteFont font)
{
    public SpriteFont Font { get; set; } = font;

    public string Text { get; set; }
    
    public Color Color { get; set; } = Color.Black;

    public static ButtonText Create(SpriteFont font, string text, Color color) => new(font)
    {
        Text = text,
        Color = color
    };
}