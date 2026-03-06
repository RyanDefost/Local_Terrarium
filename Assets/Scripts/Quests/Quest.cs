using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "QuestItem", menuName = "ScriptableObjects/Quests", order = 1)]
public class Quest : ScriptableObject
{
    [Header("Input")]
    public List<string> DialogueText;
    public GameObject QuestItem;

    //Flags
    [Space, Header("Flags")]
    public bool IsCompleted;
    public bool HasTalked;
    public bool HasFoundItem;
}
