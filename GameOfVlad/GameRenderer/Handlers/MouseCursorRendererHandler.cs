using System;
using System.Collections.Generic;
using GameOfVlad.GameObjects.Interfaces;
using GameOfVlad.Services.Camera;
using GameOfVlad.Services.Mouse;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace GameOfVlad.GameRenderer.Handlers;

public class MouseCursorRendererHandler(ICameraService cameraService, IMouseService mouseService)
    : BaseRendererObjectHandler<IMouseHoverable>, IRendererObjectHandler
{
    // TODO: разростаться может будет?
    private readonly Dictionary<Guid, bool> _gameObjectGuidToMouseHoverIndex = new();

    protected override void UpdateCore(GameTime gameTime, IMouseHoverable obj)
    {
        _gameObjectGuidToMouseHoverIndex.TryGetValue(obj.Guid, out bool oldStateIsHover);

        Vector2 mousePosition = mouseService.GetMousePosition();
        Vector2 mousePositionInWorld = cameraService.PositionByCamera(mousePosition);

        bool newStateIsHover = obj.IsCursorOver(mousePositionInWorld);
        switch (oldStateIsHover)
        {
            case false when newStateIsHover:
                obj.OnHoverEnter();
                break;
            case true when !newStateIsHover:
                obj.OnHoverExit();
                break;
        }

        if (obj is IClickable objClickable)
        {
            if (newStateIsHover && mouseService.IsLeftClick())
            {
                objClickable.OnClick();
            }
        }

        _gameObjectGuidToMouseHoverIndex[obj.Guid] = newStateIsHover;
    }
}