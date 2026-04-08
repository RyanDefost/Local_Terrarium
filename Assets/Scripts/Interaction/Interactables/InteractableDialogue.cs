using UnityEngine;

public class InteractableDialogue : MonoBehaviour, Interactable
{
    [SerializeField] Dialogue _dialogue;
    private DialogueSystem _dialogueSystem;

    private void Start() => _dialogueSystem = MultiServiceLocator.GetService<DialogueSystem>();

    public void Interact()
    {
        _dialogueSystem.ActivateDialogue(_dialogue);
    }
}
