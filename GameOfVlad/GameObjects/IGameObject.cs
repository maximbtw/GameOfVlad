using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace GameOfVlad.GameObjects;

public interface IGameObject
{
    int DrawOrder { get; }
    
    int UpdateOrder { get; }
    
    IGameObject Parent { get; set; }
    
    IEnumerable<IGameObject> Children { get; set; }
    
    bool IsActive { get; set; }
    
    bool Destroyed { get; set; }
    
    void Init(ContentManager content);

    void Terminate();
    
    void Draw(GameTime gameTime, SpriteBatch spriteBatch);
    
    void Update(GameTime gameTime);
}