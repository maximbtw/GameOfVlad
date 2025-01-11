using GameOfVlad.GameRenderer;
using Microsoft.Xna.Framework;

namespace GameOfVlad.GameObjects.Interfaces;

public interface IMouseHoverable : IRendererObject
{
    /// <summary>
    /// Вызывается, когда курсор наведен на элемент.
    /// </summary>
    void OnHoverEnter();

    /// <summary>
    /// Вызывается, когда курсор покидает элемент.
    /// </summary>
    void OnHoverExit();

    /// <summary>
    /// Проверяет, находится ли курсор внутри элемента.
    /// </summary>
    /// <param name="cursorPosition">Позиция курсора в мировых координатах.</param>
    /// <returns>True, если курсор наведен на элемент, иначе False.</returns>
    bool IsCursorOver(Vector2 cursorPosition);
}