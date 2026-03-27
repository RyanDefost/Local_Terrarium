using UnityEngine;

public class InteractableStateChanger : MonoBehaviour, Interactable
{
    public string StateName;

    public void Interact()
    {
        StateChanger.Instance.SetState(StateName);
    }
}
