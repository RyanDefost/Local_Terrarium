using System;
using Unity.VisualScripting;
using UnityEngine;

[RequireComponent(typeof(TerrariumMover))]
public class TerrariumManager : MonoBehaviour
{
    public GameObject CurrentObject;

    public Action OnPickup;
    public Action Onrelease;

    private TerrariumMover _terrariumMover;
    private TerrariumScaler _terrariumScaler;

    private void OnEnable()
    {
        TryGetComponent<TerrariumMover>(out _terrariumMover);
        TryGetComponent<TerrariumScaler>(out _terrariumScaler);

        _terrariumMover.Init(this);
        _terrariumScaler.Init(this);

        OnPickup?.Invoke(); //TEMP, THERE ISNT ANY OUTSIDE CALLS YET.
    }

    private void OnDisable()
    {
        _terrariumMover = null;
        _terrariumScaler = null;
    }


    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            SetPickup(CurrentObject);
        }

        _terrariumMover.UpdateMover();
        _terrariumScaler.UpdateScaler();
    }

    public void ReleaseObject()
    {
        Onrelease?.Invoke();
        CurrentObject = null;
    }
    //Test
    public void SetPickup(GameObject pickup)
    {
        CurrentObject = pickup;
        OnPickup?.Invoke();
    }
}
