using System.Linq;
using UnityEngine;
using UnityEngine.Assertions.Comparers;

public class TerrariumScaler : MonoBehaviour
{
    [SerializeField] GameObject _scaledParent;

    [Tooltip("Does not scale dynamicly with parent object")]
    [SerializeField] float _scaleFactor;
    private GameObject _currentObject;
    private GameObject _copiedObject;

    private TerrariumManager _manager;

    public void Init(TerrariumManager manager)
    {
        _manager = manager;
        _manager.Onrelease += SetRelease;
        _manager.OnPickup += SetPickup;

        if (_manager.CurrentObject.Count > 0)
            _currentObject = _manager.CurrentObject.First();
    }

    private void OnDisable()
    {
        _manager.Onrelease -= SetRelease;
        _manager.OnPickup -= SetPickup;

    }

    public void UpdateScaler()
    {
        if (_manager.CurrentObject == null) return;

        SetScaledPosition();
    }

    private void SetScaledPosition()
    {
        _copiedObject.transform.SetParent(_scaledParent.transform);
        _copiedObject.transform.localPosition = _manager.CurrentObject.First().transform.localPosition;
    }

    private void SetRelease()
    {
        _copiedObject.transform.localPosition = _manager.CurrentObject.First().transform.localPosition;
        _manager.SizedObject = _copiedObject;
    }

    private void SetPickup()
    {
        _copiedObject = Instantiate(_manager.CurrentObject.First(), new Vector2(0, -100), _manager.CurrentObject.First().transform.rotation);
        _copiedObject.transform.localScale = _manager.CurrentObject.First().transform.lossyScale * _scaleFactor;
    }
}