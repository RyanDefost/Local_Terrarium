using System;
using Unity.VisualScripting;
using UnityEngine;

[RequireComponent(typeof(TerrariumMover), typeof(TerrariumScaler))]
public class TerrariumManager : MonoBehaviour
{
    [Header("Input")]
    public GameObject CurrentObject;
    public GameObject OtherObject;
    public bool isActive = false;

    public Action OnPickup;
    public Action Onrelease;


    [Header("Settings")]
    public LayerMask PropMask;

    private TerrariumMover _terrariumMover;
    private TerrariumScaler _terrariumScaler;

    private void OnEnable()
    {
        TryGetComponent<TerrariumMover>(out _terrariumMover);
        TryGetComponent<TerrariumScaler>(out _terrariumScaler);

        _terrariumMover.Init(this);
        _terrariumScaler.Init(this);

        SetPickup(CurrentObject); //TEMP, THERE ISNT ANY OUTSIDE CALLS YET.
    }

    private void OnDisable()
    {
        _terrariumMover = null;
        _terrariumScaler = null;
    }

    private void Update()
    {
        if (!isActive) return;

        if (Input.GetKeyDown(KeyCode.F)) //TEMP
        {
            SetPickup(OtherObject);
        }

        _terrariumMover.UpdateMover();
        _terrariumScaler.UpdateScaler();
    }

    public void ReleaseObject()
    {
        Onrelease?.Invoke();
        CurrentObject = null;
    }

    public void SetPickup(GameObject pickup)
    {
        CurrentObject = pickup;
        OnPickup?.Invoke();
    }
}
