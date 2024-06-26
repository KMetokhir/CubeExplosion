using UnityEngine;

public class Selector : MonoBehaviour
{
    private Camera _camera;
    private Ray _ray;
    private KeyCode _createRayButton = KeyCode.Mouse0;

    private void Awake()
    {
        _camera = Camera.main;
    }

    private void Update()
    {
        if (Input.GetKeyDown(_createRayButton))
        {
            _ray = _camera.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(_ray, out hit))
            {
                if (hit.transform.TryGetComponent(out ISelectable selectable))
                {
                    selectable.Select();
                }
            }
        }
    }
}
