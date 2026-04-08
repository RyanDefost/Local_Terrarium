using Unity.VisualScripting;
using UnityEngine;

public class InteractableQuestItem : MonoBehaviour, Interactable
{
    public bool correctQuestItem = false;

    private QuestManager _questManager;

    public void Interact()
    {
        Debug.Log("Interacting");
        _questManager = MultiServiceLocator.GetService<QuestManager>();
        if (correctQuestItem)
        {
            _questManager._currentQuest.HasFoundItem = true;
            Destroy(this.gameObject);
        }
        else
        {
            //Do something.
        }
    }

    private void OnEnable()
    {

    }
}
