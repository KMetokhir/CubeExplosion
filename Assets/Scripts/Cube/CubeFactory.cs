using System;
using UnityEngine;

public class CubeFactory : MonoBehaviour
{
    [SerializeField] private Cube _cubePrefab;

    [SerializeField] private float _explosionRadiusMultiplier = 3f;

    public event Action<Cube> CubeCreated;

    private float DefaultExplosionRadious => _cubePrefab.transform.localScale.magnitude * _explosionRadiusMultiplier;

    public Cube Spawn(Vector3 spawnPosition, int divisionProbability, float explosionForce)
    {
        return Spawn(spawnPosition, divisionProbability, explosionForce, DefaultExplosionRadious, _cubePrefab.LocalScale);
    }

    public Cube Spawn(Vector3 spawnPosition, int divisionProbability, float explosionForce, float explosionRadius, Vector3 localScale)
    {
        Cube cube = Instantiate(_cubePrefab, spawnPosition, Quaternion.identity);

        cube.Init(GetRandomColor(), divisionProbability, explosionForce, explosionRadius, localScale);

        CubeCreated?.Invoke(cube);

        return cube;
    }

    private Color GetRandomColor()
    {
        return new Color(UnityEngine.Random.value, UnityEngine.Random.value, UnityEngine.Random.value);
    }

}
