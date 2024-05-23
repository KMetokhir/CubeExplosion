using System.Collections.Generic;
using UnityEngine;

public class StartGame : MonoBehaviour
{
    [SerializeField] private CubeFabrica _fabrica;
    [SerializeField] private List<Transform> _startSpawnPoints  = new List<Transform>();

    private int _maxCubeDivisionProbability = 100;

    private void Start()
    {
        SpawnStartCubes();
    }

    private void SpawnStartCubes()
    {
        foreach (Transform spawnPoint in _startSpawnPoints) 
        {
            _fabrica.SpawnCube(spawnPoint.position,  _maxCubeDivisionProbability);
        }
    } 
}
