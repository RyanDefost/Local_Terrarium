using ElmanGameDevTools.PlayerSystem;
using UnityEngine;

public class TalkingState : State
{
    public override void OnEnterState()
    {
        _playerObject.SetActive(true);
        Camera.SetupCurrent(_camera);

        _playerObject.GetComponentInChildren<PlayerController>().enabled = false;
    }

    public override void OnExitState()
    {
        _playerObject.GetComponentInChildren<PlayerController>().enabled = true;
        _playerObject.SetActive(false);
    }

    public override void UpdateState()
    {
    }
}
