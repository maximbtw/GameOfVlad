using GameOfVlad.GameRenderer;

namespace GameOfVlad.GameObjects.Effects;

public interface IEffectDrawer : IRendererObject
{
    public void AddEffect(IEffect effect);
    
    public void ClearEffects();
}