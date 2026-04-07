using System;
using System.Collections;
using System.Linq;
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

    private bool canMove { get; set; }

    private GameObject _currentObject;
    private GameObject _hoverProp;
    private PropObject _currentPropObject;
    private Vector3 _pointPosition;

    private TerrariumManager _manager;

    public void Init(TerrariumManager manager)
    {
        _manager = manager;
        canMove = true;
    }

    public void UpdateMover()
    {
        DrawLine();

        if (_manager.CurrentObject.Count > 0)
            _currentObject = _manager.CurrentObject.First();
        else _currentObject = null;

        if (_currentObject == null || !canMove) return;

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
        _currentPropObject = _hoverProp.transform.GetComponent<PropObject>();
        if (_currentPropObject == null) return;

        bool isPlaceable = _currentPropObject.GetPlaceable();
        if (isPlaceable && canMove)
        {
            if (Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.Mouse0))
            {
                canMove = false;
                StartCoroutine(MoveTo(_pointPosition, _manager.CurrentObject.First()));
            }
        }
    }

    private IEnumerator MoveTo(Vector3 EndPoint, GameObject currentObject)
    {
        var startPos = currentObject.transform.position;
        while (_timeElapsed < _fallDuration)
        {
            //a + (b - a) * t;
            currentObject.transform.position = startPos + (EndPoint - startPos) * _fallCurve.Evaluate(_timeElapsed / _fallDuration);
            _timeElapsed += Time.deltaTime;
            yield return new WaitForSeconds(Time.deltaTime);
        }
        currentObject.transform.position = EndPoint;

        var questManager = MultiServiceLocator.GetService<QuestManager>();
        questManager._currentQuest.HasPlaced = true;

        _manager.ReleaseObject();
        if (_manager.CurrentObject.Count > 0)
            _manager.OnPickup?.Invoke();

        _timeElapsed = 0;
        canMove = true;
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
        if (_manager.CurrentObject == null || _currentPropObject == null || canMove == false)
        {
            _lineRenderer.positionCount = 0;
            return;
        }

        Color color = _currentPropObject.GetPlaceable() ? Color.green : Color.red;
        _lineRenderer.SetColors(color, color);

        _lineRenderer.positionCount = 2;

        _lineRenderer.SetPosition(0, _pointPosition);
        if (_manager.CurrentObject.Count <= 0)
        {
            _lineRenderer.SetPosition(1, Vector3.down);
            return;
        }

        _lineRenderer.SetPosition(1, _manager.CurrentObject.First().transform.position);
    }
}
