using System;
using System.Collections.Generic;
using System.Linq;
using GameOfVlad.GameObjects.Interfaces;
using GameOfVlad.GameObjects.UI.Interfaces;
using Microsoft.Xna.Framework;

namespace GameOfVlad.GameRenderer.Handlers;

public class GameObjectCollisionRendererHandler : BaseRendererObjectHandler<IColliderGameObject>, IRendererObjectHandler
{
    private readonly Dictionary<Guid, HashSet<IColliderGameObject>> _previousCollisions = new();

    protected override void UpdateCore(
        GameTime gameTime, 
        IColliderGameObject obj,
        IEnumerable<IColliderGameObject> otherObjects)
    {
        if (obj is IUiComponent)
        {
            return;
        }
        
        var currentCollisions = new HashSet<IColliderGameObject>();

        foreach (IColliderGameObject anotherObj in otherObjects)
        {
            if (anotherObj is IUiComponent)
            {
                continue;
            }
            
            if (anotherObj != obj && anotherObj.Intersects(obj))
            {
                obj.OnCollision(anotherObj);

                currentCollisions.Add(anotherObj);

                bool collisionEnter =
                    !_previousCollisions.TryGetValue(obj.Guid, out HashSet<IColliderGameObject> previous) ||
                    !previous.Contains(anotherObj);

                if (collisionEnter)
                {
                    obj.OnCollisionEnter(anotherObj);
                }
            }
        }

        if (_previousCollisions.TryGetValue(obj.Guid, out HashSet<IColliderGameObject> previousCollisions))
        {
            foreach (IColliderGameObject exitedObj in previousCollisions.Except(currentCollisions))
            {
                obj.OnCollisionExit(exitedObj);
            }
        }

        _previousCollisions[obj.Guid] = currentCollisions;
    }
}