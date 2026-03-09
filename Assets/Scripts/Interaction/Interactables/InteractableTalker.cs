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

        _dialogueSystem.OnStopDialogue += Tester;
    }


    public void Interact()
    {
        _dialogueSystem.ActivateDialogue(_connectedQuest.dialogue);
    }

    private void Tester()
    {
    }

    //TEMP
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            _dialogueSystem.ActivateDialogue(_connectedQuest.dialogue);
        }
    }
}
