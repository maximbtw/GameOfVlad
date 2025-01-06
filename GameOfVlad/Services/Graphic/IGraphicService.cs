using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;

namespace GameOfVlad.Services.Graphic;

public interface IGraphicService
{
    ContentManager GetContentManager();
    
    GraphicsDeviceManager GetGraphicsDeviceManager();
}