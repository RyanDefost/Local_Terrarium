using System;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

[RequireComponent(typeof(TerrariumMover), typeof(TerrariumScaler))]
public class TerrariumManager : MonoBehaviour
{
    [Header("Input")]
    public List<GameObject> CurrentObject = new();

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

        _terrariumMover.UpdateMover();
        _terrariumScaler.UpdateScaler();
    }

    public void ReleaseObject()
    {
        LastObject = CurrentObject.First();
        Onrelease?.Invoke();
        CurrentObject.Remove(LastObject);
    }

    public void SetPickup(List<GameObject> pickup)
    {
        CurrentObject.AddRange(pickup);
        OnPickup?.Invoke();
    }
    public void SetPickup(GameObject pickup)
    {
        CurrentObject.Add(pickup);
        OnPickup?.Invoke();
    }

    public void ClearHoldBuffer()
    {
        if (CurrentObject.Count > 0) CurrentObject.First().transform.localPosition = Vector3.down;
        CurrentObject.Clear();
        isActive = false;
    }

    public void ToggleVisableObject(bool state)
    {
        if (SizedObject != null) SizedObject.GetComponent<MeshRenderer>().enabled = state;
        if (CurrentObject.Count > 0) CurrentObject.First().GetComponent<MeshRenderer>().enabled = state;
    }
}
