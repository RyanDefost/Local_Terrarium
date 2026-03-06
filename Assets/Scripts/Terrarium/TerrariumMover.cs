using System.Collections;
using System.Net;
using Unity.VisualScripting;
using UnityEditor;
using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(LineRenderer))]
public class TerrariumMover : MonoBehaviour
{
    [Header("Refrences")]
    [SerializeField] Camera _camera;
    [SerializeField] LineRenderer _lineRenderer;

    [Space, Header("Move Settings")]
    [SerializeField] float _offsetHeight;
    [SerializeField] float _speed;

    [Header("Place Settings")]
    [SerializeField] AnimationCurve _fallCurve;
    [SerializeField] float _fallDuration = 0.1f;
    private float _timeElapsed;

    private GameObject _currentObject;
    private GameObject _hoverProp;
    private Vector3 _pointPosition;

    private TerrariumManager _manager;

    public void Init(TerrariumManager manager)
    {
        _manager = manager;
    }

    public void UpdateMover()
    {
        DrawLine();

        _currentObject = _manager.CurrentObject;
        if (_currentObject == null) return;

        TryMoveObject();
        TryPlacement();
    }

    private void TryMoveObject()
    {
        Vector2 mousePosition = Mouse.current.position.ReadValue();
        Ray rayPosition = _camera.ScreenPointToRay(mousePosition);

        if (Physics.Raycast(rayPosition, out var hit, 10f, _manager.PropMask))
        {

            _currentObject.transform.position =
                Vector3.Lerp(
                    _currentObject.transform.position,
                    GetObjectPosition(hit.point),
                    Time.deltaTime * _speed
                );

            _hoverProp = hit.transform.gameObject;
            _pointPosition = hit.point;
        }
    }

    private void TryPlacement()
    {
        if (!Input.GetKeyDown(KeyCode.Space)) return;

        PropObject prop = _hoverProp.transform.GetComponent<PropObject>();

        if (prop == null) return;

        if (prop.GetPlaceable())
        {
            StartCoroutine(MoveTo(_pointPosition, _manager.CurrentObject));
            //_manager.CurrentObject.transform.position = _pointPosition;
            _manager.ReleaseObject();
        }
    }

    private IEnumerator MoveTo(Vector3 EndPoint, GameObject currentObject)
    {
        var startPos = currentObject.transform.position;
        while (_timeElapsed < _fallDuration)
        {
            //a + (b - a) * t;
            currentObject.transform.position = startPos + (EndPoint - startPos) * _fallCurve.Evaluate(_timeElapsed / _fallDuration);
            //currentObject.transform.position = Vector3.Lerp(startPos, EndPoint, curve.Evaluate(timeElapsed / lerpDuration));
            _timeElapsed += Time.deltaTime;
            yield return new WaitForSeconds(Time.deltaTime);
        }

        _timeElapsed = 0;
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

    private void DrawLine()
    {
        if (_manager.CurrentObject == null)
        {
            _lineRenderer.positionCount = 0;
            return;
        }

        _lineRenderer.positionCount = 2;

        _lineRenderer.SetPosition(0, _pointPosition);
        _lineRenderer.SetPosition(1, _manager.CurrentObject.transform.position);

    }
}
