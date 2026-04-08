using UnityEngine;

public class EndState : State
{
    public override void OnEnterState()
    {
        _playerObject.gameObject.SetActive(true);

        Camera.SetupCurrent(_camera);
        Cursor.lockState = CursorLockMode.Confined;
    }

    public override void OnExitState()
    {
    }

    public override void UpdateState()
    {

    }
}
