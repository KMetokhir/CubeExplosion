using System.Collections.Generic;
using UnityEngine;

public class CubeGameLogic : MonoBehaviour
{
    [SerializeField] private CubeFactory _factory;
    [SerializeField] private Divider _divider;
    [SerializeField] private Exploder _exploder;

    private void OnEnable()
    {
        _factory.CubeCreated += OnCubeCreated;
    }

    private void OnDisable()
    {
        _factory.CubeCreated -= OnCubeCreated;
    }

    private void OnCubeCreated(Cube cube)
    {
        cube.Selected += OnCubeSelected;
    }

    private void OnCubeSelected(Cube cube)
    {
        if (_divider.TryDivide(cube, _factory, out List<Rigidbody> dividedRigidbodies))
        {
            _exploder.Explode(cube, dividedRigidbodies);
        }
        else
        {
            _exploder.Explode(cube);
        }

        cube.Selected -= OnCubeSelected;
    }
}
