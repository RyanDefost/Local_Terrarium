using UnityEngine;

public class MovementState : State
{
    public override void OnEnterState()
    {
        _playerObject.SetActive(true);
        Camera.SetupCurrent(_camera);

        Cursor.lockState = CursorLockMode.Locked;
    }

    public override void OnExitState()
    {
        _playerObject.SetActive(false);
    }

    public override void UpdateState()
    {

    }
}
