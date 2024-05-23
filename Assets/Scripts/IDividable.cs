using UnityEngine;

public interface IDividable 
{
    public int DivisionProbability { get; }
    public Vector3 Position { get; }
    public Vector3 Scale { get; }
    public float ExplosionForce { get; }
    public float ExplosionRadius {  get; }
}
