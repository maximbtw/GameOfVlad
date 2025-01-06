using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;

namespace GameOfVlad.Services.Graphic;

public class GraphicService(ContentManager contentManager, GraphicsDeviceManager graphicsDeviceManager) : IGraphicService
{
    public ContentManager GetContentManager() => contentManager;

    public GraphicsDeviceManager GetGraphicsDeviceManager() => graphicsDeviceManager;
}