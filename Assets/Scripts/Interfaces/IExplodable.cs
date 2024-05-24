using UnityEngine;

public interface IExplodable
{
    public Rigidbody Rigidbody { get; }
    public Vector3 Position { get; }
    public float ExplosionForce { get; }
    public float ExplosionRadius { get; }
}
