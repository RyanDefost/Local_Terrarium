using UnityEngine;

public class Findable : MonoBehaviour
{
    private QuestManager _questManager;

    private void OnEnable()
    {
        _questManager = MultiServiceLocator.GetService<QuestManager>();
    }


}
