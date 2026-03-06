using UnityEngine;

public class TerrariumState : State
{
    public override void OnEnterState()
    {
        Cursor.lockState = CursorLockMode.Confined;
        _playerObject.GetComponent<TerrariumManager>().isActive = true;
        _camera.enabled = true;
    }

    public override void OnExitState()
    {
        _playerObject.GetComponent<TerrariumManager>().isActive = false;
        _camera.enabled = false;
    }

    public override void UpdateState()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            StateChanger.Instance.SetState("Move");
        }
    }
}
