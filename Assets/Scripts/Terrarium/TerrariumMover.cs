using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.InputSystem;

public class TerrariumMover : MonoBehaviour
{
    [SerializeField] float _offsetHeight;
    private GameObject _currentObject;
    private Vector3 _pointPosition;

    private TerrariumManager _manager;

    public void Init(TerrariumManager manager)
    {
        _manager = manager;
    }

    public void UpdateMover()
    {
        _currentObject = _manager.CurrentObject;
        if (_currentObject == null) return;

        TryMoveObject();
        TryPlacement();
    }

    private void TryMoveObject()
    {
        Vector2 mousePosition = Mouse.current.position.ReadValue();
        Ray rayPosition = Camera.main.ScreenPointToRay(mousePosition);

        if (Physics.Raycast(rayPosition, out var hit))
        {
            if (hit.transform.GetComponent<Hoverable>() == true)
            {
                _currentObject.transform.position = GetObjectPosition(hit.point);
                _pointPosition = hit.point;
            }
        }
    }

    private void TryPlacement()
    {
        if (!Input.GetKeyDown(KeyCode.Space)) return;
        if (Physics.Raycast(_pointPosition + Vector3.down, Vector3.down, out var hit, Mathf.Infinity))
        {
            PropObject prop = hit.transform.GetComponent<PropObject>();
            print(prop);
            if (prop == null) return;

            if (prop.GetPlaceable())
            {
                //_currentObject.transform.position = hit.point;
                _manager.CurrentObject.transform.position = hit.point;
                _manager.ReleaseObject();
            }
        }
    }

    private Vector3 GetObjectPosition(Vector3 basePosition)
    {
        var position = new Vector3(
            basePosition.x,
            _offsetHeight,
            basePosition.z
        );

        return position;
    }
}
