using TMPro;
using UnityEngine;

public class TaskList : MonoBehaviour
{
    [SerializeField] string TalkText;
    [SerializeField] string findText;
    [SerializeField] string placeText;
    [SerializeField] string endText;

    [Space]
    [SerializeField] Canvas TaskUI;
    private TextMeshProUGUI text;

    private QuestManager _questManager;
    private Quest currentQuest;

    //
    bool hasTalked = false;
    bool hasFound = false;
    bool hasPlaced = false;

    void Start()
    {
        text = TaskUI.GetComponentInChildren<TextMeshProUGUI>();

        _questManager = MultiServiceLocator.GetService<QuestManager>();
        _questManager.OnStartQuest += SetCurrentQuestText;

        SetCurrentQuestText();
    }

    private void SetCurrentQuestText()
    {
        Debug.Log("SETTINGTEXT");
        text.text = TalkText;
        currentQuest = _questManager._currentQuest;

        hasTalked = false;
        hasPlaced = false;
        hasFound = false;
    }

    private void Update()
    {
        if (currentQuest.HasTalked) text.text = findText;
        if (currentQuest.HasFoundItem) text.text = placeText;
        if (currentQuest.HasPlaced) text.text = endText;
    }
}