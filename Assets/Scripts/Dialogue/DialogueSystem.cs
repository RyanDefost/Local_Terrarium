using System.Collections;
using UnityEngine;
using TMPro;
using System;

public class DialogueSystem : MonoBehaviour
{
    [SerializeField] KeyCode _interactionKey = KeyCode.Space;
    [SerializeField] GameObject _dialogueBox;

    public Action OnStartDialogue;
    public Action OnStopDialogue;

    private Dialogue _currentDialogue;
    private int _dialogueIndex = 0;

    [SerializeField] private DialogueDeck _dialogueDeck;

    private float _speed = 0.1f;

    private QuestManager _questManager;

    //Add Actions
    private bool isActive;

    private void Awake()
    {
        MultiServiceLocator.Provide<DialogueSystem>(this);
    }

    // private void Update()
    // {
    //     if (!this.isActive) return;

    //     if (Input.GetKeyDown(_interactionKey))
    //     {
    //         ActivateDialogue(_currentDialogue);
    //         _dialogueDeck.StartDialogue();
    //     }
    // }

    public void ActivateDialogue(Dialogue dialogue)
    {
        //if (this._currentDialogue != null) DeactivateDialogue();
        this.isActive = true;

        this._dialogueBox.SetActive(true);
        this._currentDialogue = dialogue;
        this._dialogueDeck.dialogue = _currentDialogue.dialogueItems;
        this._dialogueDeck.StartDialogue();


        this.OnStartDialogue?.Invoke();
        //this._dialogueDeck.NextDialogue();
    }

    public void DeactivateDialogue()
    {
        this._dialogueDeck.StopAllCoroutines();

        this._dialogueIndex = 0;
        this._currentDialogue = null;

        this._dialogueBox.SetActive(false);
        this.isActive = false;

        StateChanger.Instance.SetState("Move");

        this.OnStopDialogue?.Invoke();
    }

    public void NextText()
    {
        if (!this.isActive) return;

        if (_dialogueIndex < _currentDialogue.dialogueItems.Count)
        {
            //StopAllCoroutines();
            //this._TMPgui.text = "";

            //StartCoroutine(SetText(_currentDialogue.dialogueItems[_dialogueIndex].Text));
            this._dialogueIndex++;
        }
        else
        {
            DeactivateDialogue();
        }
    }

    IEnumerator SetText(string text)
    {
        var currentText = "";
        for (int i = 0; i < text.Length; i++)
        {
            currentText += text[i];
            //this._TMPgui.text = currentText;

            yield return new WaitForSeconds(this._speed);
        }
        //this._TMPgui.text = text;
    }
}
