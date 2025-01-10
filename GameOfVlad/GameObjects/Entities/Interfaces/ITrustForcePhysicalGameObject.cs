namespace GameOfVlad.GameObjects.Entities.Interfaces;

public interface ITrustForcePhysicalGameObject : IPhysicalGameObject
{
    public float TrustPower { get; set; }
    
    public float GetTrustForce();
}