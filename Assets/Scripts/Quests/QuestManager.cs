using System;
using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEditor.IMGUI.Controls;
using UnityEngine;

[Serializable]
struct QuestSettings
{
    public Quest Quest;
    public GameObject Findable;
    public List<GameObject> TerrariumItem;

    [Space]
    public List<GameObject> QuestSpawnables;
}

public class QuestManager : MonoBehaviour
{
    [SerializeField] List<QuestSettings> _quests = new();
    private List<Findable> _findables = new();
    private List<GameObject> _currentQuestSpawnables;
    public GameObject _currentFindable;
    public List<GameObject> _currentTerrariumItem;

    private int _questIndex = 0;
    private bool _isClosingQuest;

    public Quest _currentQuest { get; private set; }

    public Action OnStartQuest;
    public Action OnEndQuest;

    public Action OnLastQuest;

    public Action OnHasTalked;
    public Action OnPickupItem;
    public Action OnHasPlaced;

    [SerializeField] private TerrariumManager _terrariumManager;
    [SerializeField] private QuestReseter _questRester;


    private void Awake()
    {
        MultiServiceLocator.Provide<QuestManager>(this);

        _questIndex = 0;
        EnterQuest();
    }

    public void SetTerrariumItem()
    {
        _terrariumManager.SetPickup(_currentTerrariumItem);
    }

    private void Update()
    {
        if (_currentQuest.IsCompleted && !_isClosingQuest)
        {
            _isClosingQuest = true;
            _terrariumManager.ClearHoldBuffer();

            _questRester.PlayReset();
            //Next quest is set on PlayReset
        }
        if (_currentQuest.IsCompleted == false)
        {
            _isClosingQuest = false;
        }
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
        ExitQuest();
        ++_questIndex;

        if (_questIndex >= _quests.Count)
        {
            Debug.Log("LAST QUEST HAS BEEN REACHED.");
            _questRester.EndQuest();
            return;
        }

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

        if (findable.gameObject == _currentFindable.gameObject)
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

        _quests[_questIndex].Quest.HasTalked = false;
        _quests[_questIndex].Quest.HasFoundItem = false;
        _quests[_questIndex].Quest.HasPlaced = false;
        _quests[_questIndex].Quest.HasQuestEnd = false;

        _currentQuest = _quests[_questIndex].Quest;
        _currentFindable = _quests[_questIndex].Findable;
        _currentTerrariumItem = _quests[_questIndex].TerrariumItem;
        _currentQuestSpawnables = _quests[_questIndex].QuestSpawnables;

        ToggleSpawnables(true);
        OnStartQuest?.Invoke();
    }

    private void ExitQuest()
    {
        OnEndQuest?.Invoke();

        ToggleSpawnables(false);
    }
}