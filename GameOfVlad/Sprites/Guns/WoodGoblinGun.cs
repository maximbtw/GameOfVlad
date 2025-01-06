using GameOfVlad.Game.Levels;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using GameOfVlad.Sprites.Shells;
using GameOfVlad.Sprites.Mobs;

namespace GameOfVlad.Sprites.Guns
{
    public class WoodGoblinGun : Gun
    {
        public Lazer Lazer;

        public WoodGoblinGun(ContentManager content, Mob parent, Level level)
            : base(content, parent, level)
        {
            SkinGun = Content.Load<Texture2D>("Sprite/Rocket/WoodGoblinGun/Rocket");
            WeaponM = Content.Load<Texture2D>("Sprite/Rocket/WoodGoblinGun/M");
            WeaponS = Content.Load<Texture2D>("Sprite/Rocket/WoodGoblinGun/S");

            TimeShot = 2f;
            Lazer = new Lazer(Content,
                              Content.Load<Texture2D>("Sprite/Rocket/CutterGun/Lazer2"),
                              Level,
                              Parent){
                              Gun = this,
                              Damage = 70,
                              Param = new Lazer.Params(25, 1, 0.009f, 0, 0, 0.25f, 0)};
        }

        public override void Shot()
        {
            base.Shot();

            var lazer = Lazer.Clone() as Lazer;
            lazer.Create(Direction);
            Level.Shells.Add(lazer);
        }
    }
}
