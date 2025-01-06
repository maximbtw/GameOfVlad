using System;
using System.Collections.Generic;
using GameOfVlad.Game.Levels;
using GameOfVlad.GameEffects;
using GameOfVlad.Sprites.Bonuses;
using GameOfVlad.Sprites.Shells;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace GameOfVlad.Sprites.Mobs.Boss
{
    class SunFish : Mob
    {
        Random random;
        private float time;
        private float VerticalVelosity = 0.5f;

        public FireBeam FireBeamShell;
        public FireBall FireBallShell;

        public float TimeFireBall;
        public float spawnTimeFBall = 11f;

        public float TimeFireBaem;
        private bool spawnFireBeam = false;

        private ScreenDimmingEffect screenDimmingEffect;
        private HandsFireBeamEffect handsFireBeam1;
        private HandsFireBeamEffect handsFireBeam2;

        public List<bool> Stages;

        public SunFish(ContentManager content,Texture2D texture, Vector2 location, Level level)
                : base(content, texture, location, level)
        {
            HPBar = 1000;
            Speed = 0.75f;

            FireBeamShell = new FireBeam(Content,
                                         Content.Load<Texture2D>("Pages/GamePlay/SpecialLevel2/FireBeam2"),
                                         Level,
                                         this);
            FireBallShell = new FireBall(Content,
                                         Content.Load<Texture2D>("Pages/GamePlay/SpecialLevel2/FireBall"), 
                                         Level,
                                         this);

            Stages = new List<bool>();
            for (int i = 0; i < 9; i++)
                Stages.Add(true);

            random = new Random();
            screenDimmingEffect = new ScreenDimmingEffect(Level.Backgraund);

            List<Texture2D> texturesHands = new List<Texture2D>();
            texturesHands.Add(Content.Load<Texture2D>("Pages/GamePlay/SpecialLevel2/Particles/Hand"));
            handsFireBeam1 = new HandsFireBeamEffect(texturesHands, Vector2.Zero);
            handsFireBeam2 = new HandsFireBeamEffect(texturesHands, Vector2.Zero);
        }

        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(Texture, Location, Color.White);
            if (!screenDimmingEffect.End)
            {
                screenDimmingEffect.Draw(gameTime, spriteBatch);
            }
            if (spawnFireBeam)
            {
                handsFireBeam1.Draw(spriteBatch);
                handsFireBeam2.Draw(spriteBatch);
            }
        }

        public override void Update(GameTime gameTime)
        {
            if (HPBar <= 900)
            {
                if (HPBar > 800 && Stages[0])
                    UpdateStage1();
                else if (HPBar > 700 && HPBar <= 800 && Stages[1])
                    UpdateStage2();
                else if (HPBar > 600 && HPBar <= 700 && Stages[2])
                    UpdateStage3();
                else if (HPBar > 500 && HPBar <= 600 && Stages[3])
                    UpdateStage4();
                else if (HPBar > 400 && HPBar <= 500 && Stages[4])
                    UpdateStage5();
                else if (HPBar > 300 && HPBar <= 400 && Stages[5])
                    UpdateStage6();
                else if (HPBar > 200 && HPBar <= 300 && Stages[6])
                    UpdateStage7();
                else if (HPBar > 100 && HPBar <= 200 && Stages[7])
                    UpdateStage8();
                else if (HPBar <= 100 && Stages[8])
                    UpdateStage9();
            }

            if (!screenDimmingEffect.End)
                screenDimmingEffect.Update(gameTime);
            UpdateMove(gameTime);
            UpdateAbility(gameTime);
        }

        private void UpdateMove(GameTime gameTime)
        {
            time += (float)gameTime.ElapsedGameTime.TotalSeconds;
            var point = Level.Player.Location;

            if (point.X < Location.X + Size.Width / 2)
            {
                Location.X -= Speed;
            }
            if (point.X > Location.X + Size.Width / 2)
            {
                Location.X += Speed;
            }
            if (time > 0 && time < 7.5f)
            {
                Location.Y -= VerticalVelosity;
            }
            else if (time > 5f && time < 15f)
            {
                Location.Y += VerticalVelosity;
            }
            else
                time = 0;
        }

        private void UpdateAbility(GameTime gameTime)
        {
            TimeFireBall += (float)gameTime.ElapsedGameTime.TotalSeconds;
            if (TimeFireBall > spawnTimeFBall && !spawnFireBeam)
            {
                ShotFireBall();
            }

            if (spawnFireBeam)
            {
                TimeFireBaem += (float)gameTime.ElapsedGameTime.TotalSeconds;
                if (TimeFireBaem < 3.5)
                {
                    handsFireBeam1.EmitterLocation = new Vector2(Location.X + 40, Location.Y + Size.Height - 50);
                    handsFireBeam1.Update(gameTime);
                    handsFireBeam2.EmitterLocation = new Vector2(Location.X + Size.Width - 40, Location.Y + Size.Height - 50);
                    handsFireBeam2.Update(gameTime);
                }
                else
                {
                    TimeFireBaem = 0;
                    spawnFireBeam = false;
                    TimeFireBall = spawnTimeFBall - 1.5f;
                }
            }

        }

        private void UpdateStage1()
        {
            Stages[0] = false;
            spawnTimeFBall -= 0.5f;
            screenDimmingEffect.Start();
            SpawnSpaceTrash();
            SpawnSpaceTrash();
        }

        private void UpdateStage2()
        {
            Stages[1] = false;
            spawnTimeFBall -= 0.5f;
            screenDimmingEffect.Start();
            SpawnMeteorit();
            SpawnMeteorit();
            SpawnSpaceKnight();
        }

        private void UpdateStage3()
        {
            Stages[2] = false;
            spawnTimeFBall -= 0.5f;
            screenDimmingEffect.Start();
            SpawnMeteorit(); 
            SpawnObserver();
            SpawnSpaceTrash();
        }

        private void UpdateStage4()
        {
            Stages[3] = false;
            spawnTimeFBall -= 0.5f;
            screenDimmingEffect.Start();
            SpawnObserver();
            SpawnObserver();
        }

        private void UpdateStage5()
        {
            Stages[4] = false;
            spawnTimeFBall -= 0.5f;
            screenDimmingEffect.Start();
            Level.Bonuses.Add(new HealthBox(Content, Content.Load<Texture2D>("Sprite/Bonuses/HealthBox1"), Vector2.Zero, Level));
            SpawnObserver(true);
            SpawnObserver();
        }

        private void UpdateStage6()
        {
            Stages[5] = false;
            spawnTimeFBall -= 0.5f;
            screenDimmingEffect.Start();
            SpawnSpaceKnight();
            SpawnSpaceKnight(true);
        }

        private void UpdateStage7()
        {
            Stages[6] = false;
            spawnTimeFBall -= 0.5f;
            screenDimmingEffect.Start(256);
            SpawnMeteorit(); 
            SpawnFireBeam(true);
            SpawnFireBeam(false);
            SpawnSpaceTrash();
        }
        private void UpdateStage8()
        {
            Stages[7] = false;
            spawnTimeFBall -= 0.5f;
            screenDimmingEffect.Start();
            SpawnObserver(true);
            SpawnSpaceKnight();
        }
        private void UpdateStage9()
        {
            Stages[8] = false;
            spawnTimeFBall -= 0.5f;
            screenDimmingEffect.Start(256);
            SpawnSpaceKnight(true);
            SpawnObserver(true);
            SpawnFireBeam(true);
            SpawnFireBeam(false);
            SpawnMeteorit();
        }


        void SpawnSpaceTrash()
        {
            var texture = random.Next(1, 5);
            var hp = (texture == 1) ? 50 : (texture == 2) ? 60 : (texture == 3) ? 55 : 40;
            Vector2 location;
            while (true)
            {
                location = new Vector2(random.Next(0, (int)Level.LevelSize.Width-90), 
                                       random.Next((int)Level.LevelSize.Height / 2, (int)Level.LevelSize.Height-90));
                if ((Level.Player.Location - location).Length() > 150)
                    break;
            }
            Level.HostileMobs.Add(new SpaceTrash(Content, 
                                                 Content.Load<Texture2D>("Sprite/SpaceTrash/trash" + texture),
                                                 location, 
                                                 Level){ 
                                                 HPBar = hp,
                                                 Speed = (float)random.NextDouble(),
                                                 TurnTime = random.Next(1,10),
                                                 State = SpaceTrash.StateDirection.Horizontally});;
        }

        void SpawnObserver(bool miniBoss = false)
        {
            var centr = new Vector2(Location.X + Texture.Width / 2, Location.Y + Texture.Height / 2);
            Vector2 location;
            while (true)
            {
                location = new Vector2(random.Next(0, (int)Level.LevelSize.Width), random.Next(-10, 125));
                if ((centr - location).Length() > 200)
                    break;
            }

            Level.HostileMobs.Add(new Observer(Content,
                                               Content.Load<Texture2D>("Sprite/Observer/Left/1"),
                                               location,
                                               Level,
                                               miniBoss){ 
                                               Timer = random.Next(0,3)});
 
        } 

        void SpawnMeteorit()
        {
            Level.HostileMobs.Add(new Meteorit(Content,
                                               Content.Load<Texture2D>("Sprite/Meteorit/Meteorit"),
                                               Vector2.Zero,
                                               Level));
        }

        void SpawnSpaceKnight(bool miniBoss = false)
        {
            Level.HostileMobs.Add(new SpaceKnight(Content,
                                                  Content.Load<Texture2D>("Sprite/SpaceKnight/Left/1"),
                                                  new Vector2(random.Next(0, (int)Level.LevelSize.Width), 50),
                                                  Level,
                                                  miniBoss));
        }

        private void ShotFireBall()
        {
            TimeFireBall = 0;
            var location = new Vector2(Location.X + Size.Width / 2, Location.Y + Size.Height / 2);
            var direction = Level.Player.Location - location;
            if (direction != Vector2.Zero)
                direction.Normalize();
            var fireBall = FireBallShell.Clone() as FireBall;
            fireBall.Direction = direction;
            fireBall.Location = location;
            fireBall.Rotation = (float)Math.Atan2(direction.Y, direction.X);
            fireBall.Parent = this;
            Level.Shells.Add(fireBall);
        }

        private void SpawnFireBeam(bool leftBeat)
        {
            var fireBeam = FireBeamShell.Clone() as FireBeam;
            fireBeam.LeftBeat = leftBeat;
            fireBeam.Rotation = (float)(-Math.PI / 7);
            fireBeam.Parent = this;
            Level.Shells.Add(fireBeam);
            spawnFireBeam = true;
        }

        public class HandsFireBeamEffect : ParticleEngine
        {
            public HandsFireBeamEffect(List<Texture2D> textures, Vector2 location)
                : base(textures, location)
            {
                EmitterLocation = location;
                Textures = textures;
                Particles = new List<Particle>();
                random = new Random();
            }

            public override void Update(GameTime gameTime)
            {
                int total = 1;

                for (int i = 0; i < total; i++)
                {
                    Particles.Add(GenerateNewParticle());
                }

                for (int particle = 0; particle < Particles.Count; particle++)
                {
                    Particles[particle].Update();
                    if (Particles[particle].TTL <= 0)
                    {
                        Particles.RemoveAt(particle);
                        particle--;
                    }
                }
            }

            public override Particle GenerateNewParticle()
            {
                Texture2D texture = Textures[random.Next(Textures.Count)];
                Vector2 position = EmitterLocation;
                Vector2 velocity = new Vector2(
                                        1f * (float)(random.NextDouble() * 2 - 1),
                                        1f * (float)(random.NextDouble() * 2 - 1));
                float angle = 0;
                float angularVelocity = 0.1f * (float)(random.NextDouble() * 2 - 1);
                Color color = new Color(
                            0.55f,
                            0.15f,
                            0f,
                            (float)random.NextDouble());

                float size = (float)random.NextDouble();
                int ttl = 1 + random.Next(20);

                return new Particle(texture, position, velocity, angle, angularVelocity, color, size, ttl);
            }
        }

        public class FireBall : GhostBullet
        {
            private ParticleEngine effect;

            public FireBall(ContentManager content, Texture2D texture, Level level, Mob parent)
                : base(content, texture, level, parent)
            {
                Damage = 29;
                Speed = 5f;
                Origin = new Vector2(texture.Width / 2, texture.Height / 2);

                List<Texture2D> texturesFire = new List<Texture2D>
                {
                    Content.Load<Texture2D>("Pages/GamePlay/SpecialLevel2/Particles/Fire1"),
                    Content.Load<Texture2D>("Pages/GamePlay/SpecialLevel2/Particles/Fire2"),
                    Content.Load<Texture2D>("Pages/GamePlay/SpecialLevel2/Particles/Fire3"),
                };
                effect = new ParticleEngine(texturesFire, 3, 25) 
                {
                    R = 0.25f,
                    G = 0.15f,
                    B = 0,
                    Alpha = 0.15f,
                    AngularVelocity = (float)Math.PI / 300,
                };
            }
            public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
            {
                base.Draw(gameTime, spriteBatch);
                effect.Draw(spriteBatch);
            }

            public override void Update(GameTime gameTime)
            {
                base.Update(gameTime);
                effect.EmitterLocation = new Vector2(Location.X - 25, Location.Y - 55);
                effect.Update(gameTime);
            }
        }

        public class FireBeam : GhostBullet
        {
            public bool LeftBeat = true;
            private EffectFireBeam FireParticleEngine;

            public FireBeam(ContentManager content, Texture2D texture, Level level, Mob parent)
                : base(content, texture, level, parent)
            {
                Damage = 20;
                Speed = 0;
                Origin = new Vector2(Texture.Width / 2, 25);
                Location = new Vector2(-500, -1000);

                List<Texture2D> texturesFire = new List<Texture2D>();
                texturesFire.Add(Content.Load<Texture2D>("Pages/GamePlay/SpecialLevel2/Particles/FireBeamEffect2"));

                FireParticleEngine = new EffectFireBeam(texturesFire, Vector2.Zero);
            }

            public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
            {
                base.Draw(gameTime, spriteBatch);
                FireParticleEngine.Draw(spriteBatch);
            }

            public override void Update(GameTime gameTime)
            {
                if (Rotation > Math.PI / 7)
                {
                    Dead = true;
                }
                Rotation += (float)Math.PI / 720;

                Location = LeftBeat ?
                    new Vector2(Parent.Location.X + 40, Parent.Location.Y + Parent.Size.Height - 50)
                  : new Vector2(Parent.Location.X + Parent.Size.Width - 40, Parent.Location.Y + Parent.Size.Height - 50);

                FireParticleEngine.EmitterLocation = new Vector2(Location.X, Location.Y);
                FireParticleEngine.Rotation = this.Rotation - (float)Math.PI / 180;
                FireParticleEngine.Update(gameTime);
            }

            public class EffectFireBeam : ParticleEngine
            {
                public EffectFireBeam(List<Texture2D> textures, Vector2 location)
                         : base(textures, location)
                {
                    EmitterLocation = location;
                    Textures = textures;
                    Particles = new List<Particle>();
                    random = new Random();
                }

                public override void Update(GameTime gameTime)
                {
                    int total = 2;

                    for (int i = 0; i < total; i++)
                    {
                        Particles.Add(GenerateNewParticle());
                    }

                    for (int particle = 0; particle < Particles.Count; particle++)
                    {
                        Particles[particle].Update();
                        if (Particles[particle].TTL <= 0)
                        {
                            Particles.RemoveAt(particle);
                            particle--;
                        }
                    }
                }

                public override Particle GenerateNewParticle()
                {
                    Texture2D texture = Textures[random.Next(Textures.Count)];
                    Vector2 position = EmitterLocation;
                    Vector2 velocity = new Vector2(
                                            1f * (float)(random.NextDouble() * 2 - 1),
                                            1f * (float)(random.NextDouble() * 2 - 1));
                    float angle = Rotation;
                    float angularVelocity = (float)Math.PI / 720;
                    Color color = new Color(
                                0.55f,
                                0.15f,
                                0,
                                (float)random.NextDouble());

                    float size = (float)random.NextDouble();
                    int ttl = 5;

                    return new Particle(texture, position, velocity, angle, angularVelocity, color, size, ttl)
                    { Origin = new Vector2(texture.Width / 2, 25), CurrentOrigin = false };
                }
            }
        }
    }
}
