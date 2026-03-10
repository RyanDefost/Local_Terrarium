using UnityEngine;

public class Findable : MonoBehaviour, Interactable
{
    private QuestManager _questManager;

    public void Interact()
    {
        _questManager.TrySetFoundObject(this);
    }

    private void Start()
    {
        _questManager = MultiServiceLocator.GetService<QuestManager>();
        _questManager.SubscribeFindable(this);
    }

    private void OnEnable()
    {
        _questManager?.SubscribeFindable(this);
    }

    private void OnDisable()
    {
        _questManager?.UnSubscribeFindable(this);
    }

    public void OnFound()
    {
        Debug.Log("Correct item found.");
    }

    public void OnWrongFound()
    {
        Debug.Log("Wrong item found.");
    }
}
