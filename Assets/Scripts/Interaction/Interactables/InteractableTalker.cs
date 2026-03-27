using UnityEngine;

public class InteractableTalker : MonoBehaviour, Interactable
{
    [SerializeField] Quest _connectedQuest;
    private DialogueSystem _dialogueSystem;
    private QuestManager _questManager;

    private void Start()
    {
        _questManager = MultiServiceLocator.GetService<QuestManager>();
        _dialogueSystem = MultiServiceLocator.GetService<DialogueSystem>();
    }

    public void Interact()
    {
        _dialogueSystem.OnStopDialogue += OnStopDialogue;
        _dialogueSystem.ActivateDialogue(_connectedQuest.dialogue);
        StateChanger.Instance.SetState("Talk");
    }

    private void OnStopDialogue()
    {
        _dialogueSystem.OnStopDialogue -= OnStopDialogue;
        if (_questManager.IsCurrentQuest(_connectedQuest))
        {
            _questManager._currentQuest.HasTalked = true;
        }

    }

    //TEMP
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            _dialogueSystem.ActivateDialogue(_connectedQuest.dialogue);
        }

        if (Input.GetKeyDown(KeyCode.Tab))
        {
            _dialogueSystem.DeactivateDialogue();
        }
    }
}
