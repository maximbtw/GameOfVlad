using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace GameOfVlad.GameRenderer;

public interface IRendererObject
{
    /// <summary>
    /// Возвращает номер в очердели на отрисовку.
    /// </summary>
    int DrawOrder { get; }

    /// <summary>
    /// Возвращает номер в очердели на обновление.
    /// </summary>
    int UpdateOrder { get; }

    /// <summary>
    /// Возвращает уникальный идентификатор
    /// </summary>
    Guid Guid { get; }

    /// <summary>
    /// Возвращает или устанавливает признак того, что объект следует удалить.
    /// </summary>
    bool Destroyed { get; set; }

    /// <summary>
    /// Возвращает признак того, что объект загружен.
    /// </summary>
    bool Loaded { get; }

    /// <summary>
    /// Возвращает или устанавливает признак, нужно ли обновлять объект.
    /// </summary>
    bool IsActive { get; set; }

    /// <summary>
    /// Возвращает или устанавливает признак, нужно ли отрисовывать объект.
    /// </summary>
    bool Visible { get; set; }

    /// <summary>
    /// Возвращает или устанавливает родитлеьский объект.
    /// </summary>
    IRendererObject Parent { get; set; }

    /// <summary>
    /// Возвращает или устанавливает дочерние элементы, которые отрисовываются перед объектом.
    /// </summary>
    IEnumerable<IRendererObject> ChildrenBefore { get; set; }

    /// <summary>
    /// Возвращает или устанавливает дочерние элементы, которые отрисовываются после объекта.
    /// </summary>
    IEnumerable<IRendererObject> ChildrenAfter { get; set; }

    void Load();

    void Unload();

    void Draw(GameTime gameTime, SpriteBatch spriteBatch);

    void Update(GameTime gameTime);
}