using System.Collections.Generic;
using UnityEngine;

public class StartGame : MonoBehaviour
{
    [SerializeField] private CubeFactory _factory;
    [SerializeField] private List<Transform> _startSpawnPoints = new List<Transform>();
    [SerializeField] private float _cubeStartExplosionForce;

    private int _maxCubeDivisionProbability = 100;

    private void Start()
    {
        SpawnStartCubes();
    }

    private void SpawnStartCubes()
    {
        foreach (Transform spawnPoint in _startSpawnPoints)
        {
            _factory.Spawn(spawnPoint.position, _maxCubeDivisionProbability, _cubeStartExplosionForce);
        }
    }
}
