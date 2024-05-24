using System.Collections.Generic;
using UnityEngine;

public class Exploder : MonoBehaviour
{
    public void Explode(IExplodable explodable)
    {
        List<Rigidbody> explodableRigidbodies = GetExplodableRigidbodies(explodable.Position, explodable.ExplosionRadius);

        Explode(explodable, explodableRigidbodies);
    }

    public void Explode(IExplodable explodable, List<Rigidbody> explodableRigidbodies)
    {
        foreach (Rigidbody explodableRigidbody in explodableRigidbodies)
        {
            explodableRigidbody.AddExplosionForce(explodable.ExplosionForce, explodable.Position, explodable.ExplosionRadius);
        }
    }

    private List<Rigidbody> GetExplodableRigidbodies(Vector3 position, float explosionRadius)
    {

        List<Rigidbody> rigidbodies = new List<Rigidbody>();

        Collider[] hitColliders = Physics.OverlapSphere(position, explosionRadius);

        foreach (Collider hitCollider in hitColliders)
        {
            if (hitCollider.transform.TryGetComponent(out IExplodable explodable))
            {
                rigidbodies.Add(explodable.Rigidbody);
            }
        }

        return rigidbodies;
    }
}
