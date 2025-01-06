using GameOfVlad.Game.Levels;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework;
using GameOfVlad.Tools;


namespace GameOfVlad.Sprites
{
    public class Sprite : OldComponent
    {
        public ContentManager Content;
        public Texture2D Texture;
        public Vector2 Location;
        public Level Level;

        public Vector2 Origin;
        public Size Size;
        public Color Color = Color.White;

        public Sprite()
        {

        }

        public Sprite(Texture2D texture, Vector2 location)
        {
            Texture = texture;
            Location = location;
        }

        public Sprite(ContentManager content, Texture2D texture, Vector2 location, Level level)
        {
            Content = content;
            Texture = texture;
            Location = location;
            Level = level;
            Size = new Size(Texture.Width, Texture.Height);
        }

        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(Texture, Location, Color);
        }

        public override void Update(GameTime gameTime)
        {

        }
    }
}
