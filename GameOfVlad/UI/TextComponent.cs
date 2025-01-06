using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace GameOfVlad.UI;

public class TextComponent
{
    public SpriteFont Font { get; set; }

    public string Text { get; set; } = "Text..";
    
    public Color Color { get; set; } = Color.Black;

    public TextComponent(SpriteFont font)
    {
        this.Font = font;
    }
}