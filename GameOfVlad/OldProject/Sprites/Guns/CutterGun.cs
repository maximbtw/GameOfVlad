using GameOfVlad.Game.Levels;
using GameOfVlad.OldProject.Sprites.Mobs;
using GameOfVlad.OldProject.Sprites.Shells;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace GameOfVlad.OldProject.Sprites.Guns
{
    public class CutterGun : Gun
    {
        public Lazer Lazer;
        public CutterGun(ContentManager content, Mob parent, Level level)
            : base(content, parent, level)
        {
            SkinGun = Content.Load<Texture2D>("Sprite/Rocket/CutterGun/Rocket");
            WeaponM = Content.Load<Texture2D>("Sprite/Rocket/CutterGun/M");
            WeaponS = Content.Load<Texture2D>("Sprite/Rocket/CutterGun/S");

            TimeShot = 2f;
            Lazer = new Lazer(Content,
                                      Content.Load<Texture2D>("Sprite/Rocket/WoodGoblinGun/Bullet"),
                                      Level,
                                      Parent){
                                      Gun = this,
                                      GhostLazer = false,
                                      Damage = 13,
                                      Param = new Lazer.Params(13, 0.85f, 0, 0.55f, 0.005f, 0, 0)};
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
