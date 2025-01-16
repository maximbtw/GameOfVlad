namespace GameOfVlad.GameObjects.Effects;

public sealed class EffectDrawer : DrawerGameObject, IEffectDrawer
{
    public int UpdateOrder => 1;
    
    public void AddEffect(IEffect effect)
    {
       AddGameObject(effect);
    }

    public void ClearEffects()
    {
        Clear();
    }
}