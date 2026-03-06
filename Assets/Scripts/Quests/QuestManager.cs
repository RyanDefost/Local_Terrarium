using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
struct QuestSettings
{
    public Quest Quest;
    public GameObject Findable;
}


public class QuestManager : MonoBehaviour
{
    [SerializeField] List<QuestSettings> _quests = new();
    private GameObject _currentFindable;
    private Quest _currentQuest;

    public Action OnStartQuest;
    public Action OnEndQuest;

    private void OnEnable()
    {
        MultiServiceLocator.Provide<QuestManager>(this);

        _currentQuest = _quests[0].Quest;
        _currentFindable = _quests[0].Findable;
    }

    private void InitQuest()
    {

    }

    private void Update()
    {

    }

    public void SetQuest()
    {

    }

    public void GetQuest()
    {

    }
}
