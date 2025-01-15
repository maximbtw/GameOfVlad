using GameOfVlad.GameObjects.Entities.Enemies;
using GameOfVlad.GameObjects.Entities.Player;
using Microsoft.Xna.Framework;

namespace GameOfVlad.GameRenderer.Handlers;

public class PlayerFinderRendererHandler(PlayerV2 player) : BaseRendererObjectHandler<IPlayerFinder>, IRendererObjectHandler
{
    protected override void UpdateCore(GameTime gameTime, IPlayerFinder obj)
    {
        obj.UpdateByPlayer(player);
        
        base.UpdateCore(gameTime, obj);
    }
}