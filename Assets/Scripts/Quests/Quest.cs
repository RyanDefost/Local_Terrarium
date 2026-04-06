using System.Collections.Generic;
using System.Globalization;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "QuestItem", menuName = "ScriptableObjects/Quests", order = 1)]
public class Quest : ScriptableObject
{
    [Header("Input")]
    public Dialogue dialogue;
    public Dialogue finishDialogue;
    [Space]
    public GameObject QuestItem;
    [Space]
    public List<Quest> preQuests;

    [Header("Flags")]
    public bool HasPlaced;
    public bool HasTalked;
    public bool HasFoundItem;
    public bool HasQuestEnd;
    public bool IsCompleted => HasPlaced && HasTalked && HasPlaced && HasQuestEnd;
}