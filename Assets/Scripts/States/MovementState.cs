using ElmanGameDevTools.PlayerSystem;
using UnityEngine;

public class MovementState : State
{
    public override void OnEnterState()
    {
        _playerObject.GetComponentInChildren<PlayerController>().enabled = true;
        _camera.enabled = true;
        Camera.SetupCurrent(_camera);

        Cursor.lockState = CursorLockMode.Locked;
    }

    public override void OnExitState()
    {
        _camera.enabled = false;
        _playerObject.GetComponentInChildren<PlayerController>().enabled = false;
    }

    public override void UpdateState()
    {

    }
}
