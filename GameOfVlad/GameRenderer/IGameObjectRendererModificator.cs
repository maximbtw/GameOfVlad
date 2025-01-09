using GameOfVlad.GameObjects;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace GameOfVlad.GameRenderer;

public interface IGameObjectRendererModificator
{
    void Update(IGameObject gameObject, GameTime gameTime);
    
    void Draw(IGameObject gameObject, GameTime gameTime, SpriteBatch spriteBatch);
}