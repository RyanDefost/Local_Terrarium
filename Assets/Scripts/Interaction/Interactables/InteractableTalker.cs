using UnityEngine;

public class InteractableTalker : MonoBehaviour, Interactable
{
    [SerializeField] Quest _connectedQuest;
    private DialogueSystem _dialogueSystem;
    private QuestManager _questManager;
    private AudioSource _audioSource;

    private void Start()
    {
        _questManager = MultiServiceLocator.GetService<QuestManager>();
        _dialogueSystem = MultiServiceLocator.GetService<DialogueSystem>();
        _audioSource = this.gameObject.GetComponent<AudioSource>();
    }

    public void Interact()
    {
        _dialogueSystem.OnStopDialogue += OnStopDialogue;
        if (_questManager._currentQuest.HasPlaced)
            _dialogueSystem.ActivateDialogue(_connectedQuest.finishDialogue);
        else
            _dialogueSystem.ActivateDialogue(_connectedQuest.dialogue);

        StateChanger.Instance.SetState("Talk");
        _audioSource.Play();
    }

    private void OnStopDialogue()
    {
        _dialogueSystem.OnStopDialogue -= OnStopDialogue;
        if (_questManager.IsCurrentQuest(_connectedQuest))
        {
            _questManager._currentQuest.HasTalked = true;
        }

        if (_questManager.IsCurrentQuest(_connectedQuest) && _questManager._currentQuest.HasPlaced == true)
        {
            _questManager._currentQuest.HasTalked = true;
            _questManager._currentQuest.HasQuestEnd = true;
        }
    }

    //TEMP
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            _dialogueSystem.DeactivateDialogue();
        }
    }
}
