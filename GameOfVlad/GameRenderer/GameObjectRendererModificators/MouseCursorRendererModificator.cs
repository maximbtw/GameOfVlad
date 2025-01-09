using System;
using System.Collections.Generic;
using GameOfVlad.GameObjects;
using GameOfVlad.Services.Camera;
using GameOfVlad.Services.Mouse;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace GameOfVlad.GameRenderer.GameObjectRendererModificators;

public class MouseCursorRendererModificator(ICameraService cameraService, IMouseService mouseService) : IGameObjectRendererModificator
{
    // TODO: разростаться может будет?
    private readonly Dictionary<Guid, bool> _gameObjectGuidToMouseHoverIndex = new();
    
    public void Update(IGameObject gameObject, GameTime gameTime)
    {
        if (gameObject is IMouseHoverable objHoverable)
        {
            _gameObjectGuidToMouseHoverIndex.TryGetValue(gameObject.Guid, out bool oldStateIsHover);

            Vector2 mousePosition = mouseService.GetMousePosition();
            Vector2 mousePositionInWorld = cameraService.PositionByCamera(mousePosition);

            bool newStateIsHover = objHoverable.IsCursorOver(mousePositionInWorld);
            switch (oldStateIsHover)
            {
                case false when newStateIsHover:
                    objHoverable.OnHoverEnter();
                    break;
                case true when !newStateIsHover:
                    objHoverable.OnHoverExit();
                    break;
            }

            if (objHoverable is IClickable objClickable)
            {
                if (newStateIsHover && mouseService.IsLeftClick())
                {
                    objClickable.OnClick();
                }   
            }

            _gameObjectGuidToMouseHoverIndex[gameObject.Guid] = newStateIsHover;
        }
    }

    public void Draw(IGameObject gameObject, GameTime gameTime, SpriteBatch spriteBatch)
    {
    }
}