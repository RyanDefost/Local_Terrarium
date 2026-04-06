using UnityEngine;

public class TerrariumState : State
{
    public override void OnEnterState()
    {
        _playerObject.GetComponent<TerrariumManager>().isActive = true;
        _playerObject.GetComponent<TerrariumManager>().ToggleVisableObject(true);

        Camera.SetupCurrent(_camera);
        Cursor.lockState = CursorLockMode.Confined;
    }

    public override void OnExitState()
    {
        _playerObject.GetComponent<TerrariumManager>().isActive = false;
    }

    public override void UpdateState()
    {
        if (Input.GetKeyDown(KeyCode.LeftShift) || Input.GetKeyDown(KeyCode.Tab)
        || Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.D))
        {
            var TerrariumManager = _playerObject.GetComponent<TerrariumManager>();
            if (TerrariumManager.CurrentObject != null)
                TerrariumManager.ToggleVisableObject(false);

            StateChanger.Instance.SetState("Move");
        }
    }
}
