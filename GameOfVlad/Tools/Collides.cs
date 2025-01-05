using GameOfVlad.Sprites;
using Microsoft.Xna.Framework;

namespace GameOfVlad.Tools
{
    public class Collides
    {
        public static bool CollideEarth(Sprite sprite1, Sprite sprite2)
        {
            Rectangle sprite1Rectangle = new Rectangle((int)sprite1.Location.X,
                (int)sprite1.Location.Y, (int)sprite1.Size.Width, (int)sprite1.Size.Height);
            Rectangle sprite2Rectangle = new Rectangle(
                (int)sprite2.Location.X + 125,
                (int)sprite2.Location.Y + 100,
                (int)sprite2.Size.Width - 155,
                (int)sprite2.Size.Height - 130);

            return sprite1Rectangle.Intersects(sprite2Rectangle);
        }


        public static bool CollideBlackHole(Sprite sprite1, Sprite sprite2)
        {
            Rectangle sprite1Rectangle = new Rectangle((int)sprite1.Location.X,
                (int)sprite1.Location.Y, (int)sprite1.Size.Width, (int)sprite1.Size.Height);
            Rectangle sprite2Rectangle = new Rectangle(
                (int)sprite2.Location.X + 165,
                (int)sprite2.Location.Y + 150,
                (int)sprite2.Size.Width - 215,
                (int)sprite2.Size.Height - 230);

            return sprite1Rectangle.Intersects(sprite2Rectangle);
        }

        public static bool Collide(ColldesSprite sprite1, ColldesSprite sprite2)
        {
            if (sprite1.CollisionArea.Intersects(sprite2.CollisionArea))
                return sprite1.Intersects(sprite2);
            return false;
        }
    }
}
