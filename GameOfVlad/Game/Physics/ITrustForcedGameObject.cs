namespace GameOfVlad.Game.Physics;

public interface ITrustForcedGameObject : IPhysicalGameObject
{
    public float TrustPower { get; set; }
    
    public float Rotation { get; set; }
}