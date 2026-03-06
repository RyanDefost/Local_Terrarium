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

        _currentObject = _manager.CurrentObject;
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
        _copiedObject.transform.localPosition = _manager.CurrentObject.transform.localPosition;
    }

    private void SetRelease()
    {
        _copiedObject.transform.localPosition = _manager.CurrentObject.transform.localPosition;
    }

    private void SetPickup()
    {
        _copiedObject = Instantiate(_manager.CurrentObject, new Vector2(0, -100), _manager.CurrentObject.transform.rotation);
        _copiedObject.transform.localScale = _manager.CurrentObject.transform.lossyScale * _scaleFactor;
    }
}
