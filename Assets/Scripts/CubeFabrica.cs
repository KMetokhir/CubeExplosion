using System.Collections.Generic;
using UnityEngine;

public class CubeFabrica : MonoBehaviour
{
    [SerializeField] private Cube _cubePrefab;

    private float _scaleDivider = 2;
    private float _divisionProbabilityDivider = 2;
    private float _spherSpawnRadiusMultiplier = 2;

    private int _maxCubesPerCreation = 6;
    private int _minCubesPerCreation = 2;

    public Cube SpawnCube(Vector3 spawnPosition, int divisionProbability)
    {
        Cube cube = Instantiate(_cubePrefab, spawnPosition, Quaternion.identity);

        cube.SetFabrica(this);
        cube.SetDivisionProbability(divisionProbability);
        cube.SetColor(GetRandomColor());

        return cube;
    }

    public bool TryCreateChildCubes(int creationProbability, Vector3 cubeScale, Vector3 CubePosition, out List<Rigidbody> cubes)
    {
        bool isSuccess = false;
        cubes = new List<Rigidbody>();

        int minProbability = 0;
        int maxProbability = 100;

        if (creationProbability >= UserUtils.GenerateRandomNumber(minProbability, maxProbability))
        {
            int cubesCount = UserUtils.GenerateRandomNumber(_minCubesPerCreation, _maxCubesPerCreation);

            for (int i = 0; i < cubesCount; i++)
            {
                Cube cube = CreateChildCube(CubePosition, creationProbability, cubeScale);
                cubes.Add(cube.Rigidbody);
            }

            isSuccess = true;
        }

        return isSuccess;
    }

    private Cube CreateChildCube(Vector3 cubePosition, int divisionProbability, Vector3 cubeScale)
    {
        float spawnRadius = cubeScale.magnitude;
        var yOffSetMultiplier = 2;
        Vector3 spawnYOffSet = new Vector3(0, spawnRadius * yOffSetMultiplier, 0);
        Vector3 spawnPosition = cubePosition + Random.insideUnitSphere * spawnRadius * _spherSpawnRadiusMultiplier + spawnYOffSet;

        Cube cube = SpawnCube(spawnPosition, (int)(divisionProbability / _divisionProbabilityDivider));

        Vector3 scale = cubeScale / _scaleDivider;
        cube.transform.localScale = scale;

        return cube;
    }

    private Color GetRandomColor()
    {
        return new Color(Random.value, Random.value, Random.value);
    }
}
