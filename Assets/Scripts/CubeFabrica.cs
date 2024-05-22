//using System;
using System.Collections.Generic;
using UnityEngine;

public class CubeFabrica : MonoBehaviour
{
    [SerializeField] private Cube _cubePrefab;

    private float _scaleDivider = 2;
    private float _creationProbabilityDivider = 2;
    private float _spherSpawnRadiusMultiplier = 2;

    private int _maxCubesPerCreation = 6;
    private int _minCubesPerCreation = 2;    

    public bool TryCreateCubes( int creationProbability, Vector3 cubeScale, Vector3 CubePosition, out List<Rigidbody> cubes)
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
                Cube cube = CreateCube(CubePosition, creationProbability, cubeScale);
                Rigidbody cubeRigidBody = cube.GetComponent<Rigidbody>();
                cubes.Add(cubeRigidBody);
            }

            isSuccess = true;
        }

        return isSuccess;
    }

    private Cube CreateCube(Vector3 cubePosition, int creationProbability, Vector3 cubeScale)
    {
        float spawnRadius = cubeScale.magnitude;
        Vector3 spawnYOffSet =new Vector3(0,spawnRadius*2, 0);
        Vector3 spawnPosition = cubePosition + Random.insideUnitSphere * spawnRadius * _spherSpawnRadiusMultiplier+ spawnYOffSet;

        Cube cube = Instantiate(_cubePrefab, spawnPosition, Quaternion.identity);

        cube.SetDivisionProbability((int)(creationProbability / _creationProbabilityDivider));
        Vector3 scale = cubeScale / _scaleDivider;
        cube.transform.localScale = scale;      

        return cube;
    }    
   
}
