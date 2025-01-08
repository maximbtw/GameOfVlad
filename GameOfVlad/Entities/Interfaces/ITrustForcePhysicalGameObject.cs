namespace GameOfVlad.Entities.Interfaces;

public interface ITrustForcePhysicalGameObject : IPhysicalGameObject
{
    public float TrustPower { get; set; }
    
    public float Rotation { get; set; }
}