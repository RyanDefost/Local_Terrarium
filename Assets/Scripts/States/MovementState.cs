using UnityEngine;

public class MovementState : State
{
    public override void OnEnterState()
    {
        Cursor.lockState = CursorLockMode.Locked;

        _camera.enabled = true;
        _playerObject.SetActive(true);
    }

    public override void OnExitState()
    {
        _camera.enabled = false;
        _playerObject.SetActive(false);
    }

    public override void UpdateState()
    {

    }
}
