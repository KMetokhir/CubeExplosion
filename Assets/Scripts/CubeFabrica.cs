using UnityEngine;

public class CubeFabrica : MonoBehaviour
{
    [SerializeField] private Cube _cubePrefab;

    [SerializeField] private float _explosionForceMultiplier;
    [SerializeField] private float _explosionRadiusMultiplier;

    private float _startExplosionRadious => _cubePrefab.transform.localScale.magnitude * _explosionRadiusMultiplier;

    private float _scaleDivider = 2;
    private float _divisionProbabilityDivider = 2;
    private float _spherSpawnRadiusMultiplier = 2;

    private int _maxCubesPerCreation = 6;
    private int _minCubesPerCreation = 2;

    private void OnValidate()
    {
        _explosionForceMultiplier = Mathf.Abs(_explosionForceMultiplier);
        _explosionRadiusMultiplier = Mathf.Abs(_explosionRadiusMultiplier);
    }

    public Cube SpawnCube(Vector3 spawnPosition, int divisionProbability, float explosionForce)
    {
        return SpawnCube(spawnPosition, divisionProbability, explosionForce, _startExplosionRadious);
    }

    public Cube SpawnCube(Vector3 spawnPosition, int divisionProbability, float explosionForce, float explosionRadius)
    {
        Cube cube = Instantiate(_cubePrefab, spawnPosition, Quaternion.identity);

        cube.SetFabrica(this);
        cube.SetDivisionProbability(divisionProbability);
        cube.SetColor(GetRandomColor());
        cube.SetExplosionForce(explosionForce);
        cube.SetExplosionRadious(explosionRadius);

        return cube;
    }

    public void CreateChildCubes(IDividable dividable)
    {
        int minProbability = 0;
        int maxProbability = 100;

        if (dividable.DivisionProbability >= Random.Range(minProbability, maxProbability))
        {
            int cubesCount = Random.Range(_minCubesPerCreation, _maxCubesPerCreation);

            for (int i = 0; i < cubesCount; i++)
            {
                CreateChildCube(dividable.Position, dividable.DivisionProbability, dividable.Scale, dividable.ExplosionRadius, dividable.ExplosionForce);
            }
        }
    }

    private Cube CreateChildCube(Vector3 cubePosition, int divisionProbability, Vector3 cubeScale, float explosionRadius, float explosionForce)
    {
        float spawnRadius = cubeScale.magnitude;
        var yOffSetMultiplier = 2;
        Vector3 spawnYOffSet = new Vector3(0, spawnRadius * yOffSetMultiplier, 0);
        Vector3 spawnPosition = cubePosition + Random.insideUnitSphere * spawnRadius * _spherSpawnRadiusMultiplier + spawnYOffSet;

        Cube cube = SpawnCube(spawnPosition, (int)(divisionProbability / _divisionProbabilityDivider),
            explosionForce * _explosionForceMultiplier, explosionRadius * _explosionRadiusMultiplier);

        Vector3 scale = cubeScale / _scaleDivider;
        cube.transform.localScale = scale;

        return cube;
    }

    private Color GetRandomColor()
    {
        return new Color(Random.value, Random.value, Random.value);
    }
}
