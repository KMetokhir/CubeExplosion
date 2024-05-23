using UnityEngine;

public interface IExplodable
{
    public Rigidbody Rigidbody { get; }
    public void Explode();
}
