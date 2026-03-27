using System;
using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine;

[Serializable]
struct QuestSettings
{
    public Quest Quest;
    public GameObject Findable;

    [Space]
    public List<GameObject> QuestSpawnables;
}

public class QuestManager : MonoBehaviour
{
    [SerializeField] List<QuestSettings> _quests = new();
    private List<Findable> _findables = new();
    private List<GameObject> _currentQuestSpawnables;
    private GameObject _currentFindable;
    private int _questIndex = 0;

    public Quest _currentQuest { get; private set; }

    public Action OnStartQuest;
    public Action OnEndQuest;

    private void Awake()
    {
        MultiServiceLocator.Provide<QuestManager>(this);

        _questIndex = 0;
        EnterQuest();
    }

    private void Update()
    {
        if (_currentQuest.IsCompleted) NextQuest();
    }

    public void SubscribeFindable(Findable findable)
    {
        if (!_findables.Contains(findable)) _findables.Add(findable);
    }
    public void UnSubscribeFindable(Findable findable)
    {
        if (_findables.Contains(findable)) _findables.Remove(findable);
    }

    public void NextQuest()
    {
        if (_questIndex == _quests.Count) return;

        ExitQuest();
        ++_questIndex;
        EnterQuest();
    }

    public bool IsCurrentQuest(Quest quest)
    {
        if (_currentQuest == quest) return true;
        else return false;
    }

    public void TrySetFoundObject(Findable findable)
    {
        if (_currentQuest.HasFoundItem == true) return;

        if (findable == _currentFindable)
        {
            _currentQuest.HasFoundItem = true;
            findable.OnFound();
        }
        else
        {
            findable.OnWrongFound();
        }
    }

    private void ToggleSpawnables(bool activeState)
    {
        foreach (var item in _currentQuestSpawnables)
        {
            item.SetActive(activeState);
        }
    }

    private void EnterQuest()
    {
        OnStartQuest?.Invoke();

        _currentQuest = _quests[_questIndex].Quest;
        _currentFindable = _quests[_questIndex].Findable;
        _currentQuestSpawnables = _quests[_questIndex].QuestSpawnables;

        ToggleSpawnables(true);
    }

    private void ExitQuest()
    {
        OnEndQuest?.Invoke();

        ToggleSpawnables(false);
    }

}