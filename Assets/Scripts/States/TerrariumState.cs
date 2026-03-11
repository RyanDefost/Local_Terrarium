using UnityEngine;

public class TerrariumState : State
{
    public override void OnEnterState()
    {
        _playerObject.GetComponent<TerrariumManager>().isActive = true;

        Camera.SetupCurrent(_camera);
        Cursor.lockState = CursorLockMode.Confined;
    }

    public override void OnExitState()
    {
        _playerObject.GetComponent<TerrariumManager>().isActive = false;
    }

    public override void UpdateState()
    {
        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            StateChanger.Instance.SetState("Move");
        }
    }
}
