using System;
using UnityEngine;

public class CubeFabrica : MonoBehaviour
{
    [SerializeField] private Cube _cubePrefab;

    private float _startExplosionRadiusMultiplier = 3;

    public event Action<Cube> CubeCreatedEvent;

    private float StartExplosionRadious => _cubePrefab.transform.localScale.magnitude * _startExplosionRadiusMultiplier;

    public Cube Spawn(Vector3 spawnPosition, int divisionProbability, float explosionForce)
    {
        return Spawn(spawnPosition, divisionProbability, explosionForce, StartExplosionRadious, _cubePrefab.LocalScale);
    }

    public Cube Spawn(Vector3 spawnPosition, int divisionProbability, float explosionForce, float explosionRadius, Vector3 localScale)
    {
        Cube cube = Instantiate(_cubePrefab, spawnPosition, Quaternion.identity);

        cube.Init(GetRandomColor(), divisionProbability, explosionForce, explosionRadius, localScale);

        CubeCreatedEvent?.Invoke(cube);

        return cube;
    }

    private Color GetRandomColor()
    {
        return new Color(UnityEngine.Random.value, UnityEngine.Random.value, UnityEngine.Random.value);
    }

}
