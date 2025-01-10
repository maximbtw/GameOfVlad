using System.Collections.Generic;
using GameOfVlad.Game;
using GameOfVlad.GameObjects.UI.Components;
using GameOfVlad.GameObjects.UI.Components.ButtonComponent;
using GameOfVlad.GameObjects.UI.Interfaces;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace GameOfVlad.Scenes.Map;

public partial class MapScene
{
    protected override IEnumerable<IUiComponent> InitRenderObjectsCore()
    {
        var font = this.ContentManager.Load<SpriteFont>("Pages/MapLevels/Font");
        
        yield return new Image(this.ContentManager)
        {
            Texture = this.ContentManager.Load<Texture2D>("Pages/MapLevels/Backgraund2")
        };

        var btnLevel1 = new Button(this.ContentManager)
        {
            Texture = this.ContentManager.Load<Texture2D>("Pages/MapLevels/Buttons/LevelSelect"),
            Position = new Vector2(954, Settings.ScreenHeight - 165),
            Text = ButtonText.Create(font, "1", Color.AliceBlue)
        };

        btnLevel1.OnBtnClick += () =>
        {
            this.SceneService.PushScene(SceneType.Game);
        };

        yield return btnLevel1;
    }
    
    /*protected override IEnumerable<UiComponentBase> GetUiComponents(ContentManager content, ICameraService cameraService)
    {
        var btnLevelTexture = content.Load<Texture2D>("Pages/MapLevels/Buttons/LevelSelect");
        var btnSpecialLevelTexture = content.Load<Texture2D>("Pages/MapLevels/Buttons/SpecialLevel/LevelSelect");
        var font = content.Load<SpriteFont>("Pages/MapLevels/Font");

        var backgraundHeight = 1080;

        yield return new Image(cameraService)
        {
            Texture = content.Load<Texture2D>("Pages/MapLevels/Backgraund2"),
            Size = new Vector2(1920, 1080)
        };

        yield return new Button(cameraService)
        {
            Texture = btnLevelTexture,
            Position = new Vector2(954, backgraundHeight - 165),
            Text = new ButtonText(font)
            {
                Text = "1",
                Color = Color.AliceBlue,
            },
            OnClick = () => SetLevel?.Invoke(LevelType.Level1)
        };
        yield return new Button(cameraService)
        {
            Texture = btnLevelTexture,
            Position = new Vector2(754, backgraundHeight - 165),
            Text = new ButtonText(font)
            {
                Text = "2",
                Color = Color.AliceBlue,
            },
            OnClick = () => SetLevel?.Invoke(LevelType.Level2)
        };
        yield return new Button(cameraService)
        {
            Texture = btnLevelTexture,
            Position = new Vector2(648, backgraundHeight - 200),
            Text = new ButtonText(font)
            {
                Text = "3",
                Color = Color.AliceBlue,
            },
            OnClick = () => SetLevel?.Invoke(LevelType.Level3)
        };
        yield return new Button(cameraService)
        {
            Texture = btnLevelTexture,
            Position = new Vector2(540, backgraundHeight - 215),
            Text = new ButtonText(font)
            {
                Text = "4",
                Color = Color.AliceBlue,
            }
        };
        yield return new Button(cameraService)
        {
            Texture = btnLevelTexture,
            Position = new Vector2(428, backgraundHeight - 205),
            Text = new ButtonText(font)
            {
                Text = "5",
                Color = Color.AliceBlue,
            }
        };
        yield return new Button(cameraService)
        {
            Texture = btnLevelTexture,
            Position = new Vector2(320, backgraundHeight - 178),
            Text = new ButtonText(font)
            {
                Text = "6",
                Color = Color.AliceBlue,
            }
        };
        yield return new Button(cameraService)
        {
            Texture = btnLevelTexture,
            Position = new Vector2(214, backgraundHeight - 170),
            Text = new ButtonText(font)
            {
                Text = "7",
                Color = Color.AliceBlue,
            }
        };
        yield return new Button(cameraService)
        {
            Texture = btnLevelTexture,
            Position = new Vector2(120, backgraundHeight - 230),
            Text = new ButtonText(font)
            {
                Text = "8",
                Color = Color.AliceBlue,
            }
        };
        yield return new Button(cameraService)
        {
            Texture = btnSpecialLevelTexture,
            Position = new Vector2(200, backgraundHeight - 360),
            Text = new ButtonText(font)
            {
                Text = "??",
                Color = Color.DarkRed,
            }
        };

        yield return new Button(cameraService)
        {
            Texture = btnLevelTexture,
            Position = new Vector2(354, backgraundHeight - 435),
            Text = new ButtonText(font)
            {
                Text = "9",
                Color = Color.AliceBlue,
            }
        };

        yield return new Button(cameraService)
        {
            Texture = btnLevelTexture,
            Position = new Vector2(480, backgraundHeight - 465),
            Text = new ButtonText(font)
            {
                Text = "10",
                Color = Color.AliceBlue,
            }
        };
        yield return new Button(cameraService)
        {
            Texture = btnLevelTexture,
            Position = new Vector2(700, backgraundHeight - 540),
            Text = new ButtonText(font)
            {
                Text = "11",
                Color = Color.AliceBlue,
            }
        };
        yield return new Button(cameraService)
        {
            Texture = btnLevelTexture,
            Position = new Vector2(710, backgraundHeight - 645),
            Text = new ButtonText(font)
            {
                Text = "12",
                Color = Color.AliceBlue,
            }
        };
        yield return new Button(cameraService)
        {
            Texture = btnSpecialLevelTexture,
            Position = new Vector2(620, backgraundHeight - 707),
            Text = new ButtonText(font)
            {
                Text = "??",
                Color = Color.DarkRed,
            }
        };

        yield return new Button(cameraService)
        {
            Texture = btnLevelTexture,
            Position = new Vector2(530, backgraundHeight - 745),
            Text = new ButtonText(font)
            {
                Text = "13",
                Color = Color.AliceBlue,
            }
        };
        yield return new Button(cameraService)
        {
            Texture = btnLevelTexture,
            Position = new Vector2(428, backgraundHeight - 755),
            Text = new ButtonText(font)
            {
                Text = "14",
                Color = Color.AliceBlue,
            }
        };
        yield return new Button(cameraService)
        {
            Texture = btnLevelTexture,
            Position = new Vector2(328, backgraundHeight - 715),
            Text = new ButtonText(font)
            {
                Text = "15",
                Color = Color.AliceBlue,
            }
        };
        yield return new Button(cameraService)
        {
            Texture = btnLevelTexture,
            Position = new Vector2(238, backgraundHeight - 640),
            Text = new ButtonText(font)
            {
                Text = "16",
                Color = Color.AliceBlue,
            }
        };
        yield return new Button(cameraService)
        {
            Texture = btnLevelTexture,
            Position = new Vector2(138, backgraundHeight - 600),
            Text = new ButtonText(font)
            {
                Text = "17",
                Color = Color.AliceBlue,
            }
        };
        yield return new Button(cameraService)
        {
            Texture = btnLevelTexture,
            Position = new Vector2(38, backgraundHeight - 640),
            Text = new ButtonText(font)
            {
                Text = "18",
                Color = Color.AliceBlue,
            }
        };
        yield return new Button(cameraService)
        {
            Texture = btnLevelTexture,
            Position = new Vector2(10, backgraundHeight - 740),
            Text = new ButtonText(font)
            {
                Text = "19",
                Color = Color.AliceBlue,
            }
        };
        yield return new Button(cameraService)
        {
            Texture = btnLevelTexture,
            Position = new Vector2(28, backgraundHeight - 840),
            Text = new ButtonText(font)
            {
                Text = "20",
                Color = Color.AliceBlue,
            }
        };
        yield return new Button(cameraService)
        {
            Texture = btnSpecialLevelTexture,
            Position = new Vector2(130, backgraundHeight - 920),
            Text = new ButtonText(font)
            {
                Text = "??",
                Color = Color.DarkRed,
            }
        };

        yield return new Button(cameraService)
        {
            Texture = btnLevelTexture,
            Position = new Vector2(245, backgraundHeight - 1020),
            Text = new ButtonText(font)
            {
                Text = "21",
                Color = Color.AliceBlue,
            }
        };
        yield return new Button(cameraService)
        {
            Texture = btnLevelTexture,
            Position = new Vector2(355, backgraundHeight - 1045),
            Text = new ButtonText(font)
            {
                Text = "22",
                Color = Color.AliceBlue,
            }
        };

        yield return new Button(cameraService)
        {
            Texture = btnLevelTexture,
            Position = new Vector2(465, backgraundHeight - 1044),
            Text = new ButtonText(font)
            {
                Text = "23",
                Color = Color.AliceBlue,
            }
        };
        yield return new Button(cameraService)
        {
            Texture = btnLevelTexture,
            Position = new Vector2(572, backgraundHeight - 1007),
            Text = new ButtonText(font)
            {
                Text = "24",
                Color = Color.AliceBlue,
            }
        };
        yield return new Button(cameraService)
        {
            Texture = btnLevelTexture,
            Position = new Vector2(672, backgraundHeight - 956),
            Text = new ButtonText(font)
            {
                Text = "25",
                Color = Color.AliceBlue,
            }
        };

        yield return new Button(cameraService)
        {
            Texture = btnLevelTexture,
            Position = new Vector2(753, backgraundHeight - 875),
            Text = new ButtonText(font)
            {
                Text = "26",
                Color = Color.AliceBlue,
            }
        };

        yield return new Button(cameraService)
        {
            Texture = btnLevelTexture,
            Position = new Vector2(795, backgraundHeight - 773),
            Text = new ButtonText(font)
            {
                Text = "27",
                Color = Color.AliceBlue,
            }
        };
        yield return new Button(cameraService)
        {
            Texture = btnLevelTexture,
            Position = new Vector2(825, backgraundHeight - 670),
            Text = new ButtonText(font)
            {
                Text = "28",
                Color = Color.AliceBlue,
            }
        };

        yield return new Button(cameraService)
        {
            Texture = btnLevelTexture,
            Position = new Vector2(845, backgraundHeight - 562),
            Text = new ButtonText(font)
            {
                Text = "29",
                Color = Color.AliceBlue,
            }
        };
    }*/
}