using GameOfVlad.Game.Levels;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace GameOfVlad.Sprites.Mobs
{
    public class Mob : ColldesSprite
    {
        public Mob(ContentManager content, Texture2D texture, Vector2 location, Level level)
                : base(content, texture, location, level)
        {
        }
    }
}
