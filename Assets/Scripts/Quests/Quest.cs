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
    public GameObject QuestItem;
    [Space]
    public List<Quest> preQuests;

    [Header("Flags")]
    [HideInInspector] public bool HasPlaced;
    [HideInInspector] public bool HasTalked;
    [HideInInspector] public bool HasFoundItem;
    public bool IsCompleted => HasPlaced && HasTalked && HasPlaced;
}