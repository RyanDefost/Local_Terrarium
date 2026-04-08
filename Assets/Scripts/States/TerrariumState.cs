using UnityEngine;

public class TerrariumState : State
{
    public override void OnEnterState()
    {
        _camera.enabled = true;
        Camera.SetupCurrent(_camera);
        Cursor.lockState = CursorLockMode.Confined;

        StartCoroutine(_playerObject.GetComponent<TerrariumManager>().ToggleTerrarium(true, 0.5f));
        _playerObject.GetComponent<TerrariumManager>().ToggleVisableObject(true);
    }

    public override void OnExitState()
    {
        StartCoroutine(_playerObject.GetComponent<TerrariumManager>().ToggleTerrarium(false, 0.5f));
        _camera.enabled = false;
    }

    public override void UpdateState()
    {
        if (Input.GetKeyDown(KeyCode.LeftShift) || Input.GetKeyDown(KeyCode.Tab)
        || Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.D))
        {
            var TerrariumManager = _playerObject.GetComponent<TerrariumManager>();
            // if (TerrariumManager.CurrentObject.Count < 0)
            //     TerrariumManager.ToggleVisableObject(false);

            StateChanger.Instance.SetState("Move");
        }
    }
}
