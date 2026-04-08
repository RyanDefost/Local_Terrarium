using UnityEngine;

public class PropObject : MonoBehaviour
{
    [SerializeField] bool _canPlace = true;

    public bool GetPlaceable() => _canPlace;
    public bool SetPlaceable(bool state) => _canPlace = state;
    public bool TogglePlaceable() => _canPlace = !_canPlace;
}