using System.Collections.Generic;
using UnityEngine;

public class CubeGameLogic : MonoBehaviour
{
    [SerializeField] private CubeFabrica _fabrica;
    [SerializeField] private Divider _divider;
    [SerializeField] private Exploder _exploder;

    private void OnEnable()
    {
        _fabrica.CubeCreatedEvent += OnCubeCreated;
    }

    private void OnDisable()
    {
        _fabrica.CubeCreatedEvent -= OnCubeCreated;
    }

    private void OnCubeCreated(Cube cube)
    {
        cube.SelectedEvent += OnCubeSelected;
    }

    private void OnCubeSelected(Cube cube)
    {
        if (_divider.TryDivide(cube, _fabrica, out List<Rigidbody> dividedRigidbodies))
        {
            _exploder.Explode(cube, dividedRigidbodies);
        }
        else
        {
            _exploder.Explode(cube);
        }

        cube.SelectedEvent -= OnCubeSelected;
    }
}
