using Unity.VisualScripting;
using UnityEngine;

public abstract class State : MonoBehaviour
{
    public GameObject _playerObject;
    public Camera _camera;
    public string Name;
    public abstract void UpdateState();
    public abstract void OnEnterState();
    public abstract void OnExitState();
}
