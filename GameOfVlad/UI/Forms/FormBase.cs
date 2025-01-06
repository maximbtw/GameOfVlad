using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace GameOfVlad.UI.Forms;

public abstract class FormBase : UiComponent
{
    private readonly List<UiComponent> _uiComponents = new();
    
    protected override void InitCore(ContentManager content)
    {
        foreach (UiComponent component in GetUiComponents(content))
        {
            component.Init(content);
            _uiComponents.Add(component);
        }
    }
    
    protected abstract IEnumerable<UiComponent> GetUiComponents(ContentManager content);
    
    protected override void DrawCore(GameTime gameTime, SpriteBatch spriteBatch)
    {
        base.DrawCore(gameTime, spriteBatch);
        
        _uiComponents.ForEach(component => component.Draw(gameTime, spriteBatch));
    }

    protected override void UpdateCore(GameTime gameTime)
    {
        base.UpdateCore(gameTime);
        
        _uiComponents.ForEach(component => component.Update(gameTime));
    }
}