using TMPro;
using UnityEngine;

public class TaskList : MonoBehaviour
{
    [SerializeField] string TalkText;
    private string altTalkText = "[V] Talk to the visitor downstairs.";

    [SerializeField] string findText;
    private string altFindText = "[V] Find requested object.";

    [SerializeField] string placeText;
    private string altPlaceText = "[V] Place requested object on Terrarium.";

    [Space]
    [SerializeField] Canvas TaskUI;
    private TextMeshProUGUI text;

    private QuestManager _questManager;
    private Quest currentQuest;

    void Start()
    {
        text = TaskUI.GetComponentInChildren<TextMeshProUGUI>();

        _questManager = MultiServiceLocator.GetService<QuestManager>();
        currentQuest = _questManager._currentQuest;
    }

    void Update()
    {
        UpdateText();
        SetText();
    }

    private void SetText()
    {
        text.text = (
          "TODO:" + "\n" +
           TalkText + "\n"
         + findText + "\n"
         + placeText
        );
    }

    private void UpdateText()
    {
        if (currentQuest.IsCompleted) return;

        if (currentQuest.HasTalked) TalkText = altTalkText;
        if (currentQuest.HasFoundItem) findText = altFindText;
        if (currentQuest.HasPlaced) placeText = altPlaceText;
    }
}