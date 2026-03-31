using System;
using Unity.VisualScripting;
using UnityEngine;

[RequireComponent(typeof(TerrariumMover), typeof(TerrariumScaler))]
public class TerrariumManager : MonoBehaviour
{
    [Header("Input")]
    public GameObject CurrentObject;

    public GameObject LastObject;
    public GameObject SizedObject;

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

        //SetPickup(CurrentObject); //TEMP, THERE ISNT ANY OUTSIDE CALLS YET.
    }

    private void OnDisable()
    {
        _terrariumMover = null;
        _terrariumScaler = null;
    }

    private void Update()
    {
        if (!isActive) return;

        if (Input.GetKeyDown(KeyCode.F) && LastObject != null) //TEMP
        {
            SetPickup(LastObject);
        }

        _terrariumMover.UpdateMover();
        _terrariumScaler.UpdateScaler();
    }

    public void ReleaseObject()
    {
        LastObject = CurrentObject;
        Onrelease?.Invoke();
        CurrentObject = null;
    }

    public void SetPickup(GameObject pickup)
    {
        CurrentObject = pickup;
        OnPickup?.Invoke();
    }

    public void ToggleVisableObject(bool state)
    {
        if (SizedObject != null) SizedObject.GetComponent<MeshRenderer>().enabled = state;
        if (CurrentObject != null) CurrentObject.GetComponent<MeshRenderer>().enabled = state;
    }
}
