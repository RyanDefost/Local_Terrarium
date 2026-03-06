using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public struct DialogueContent
{
    [TextArea] public string Text;
    public TextType type;
}

public enum TextType { DEFAULT, FAST, SLOW }

[CreateAssetMenu(fileName = "DialogueItem", menuName = "ScriptableObjects/Dialogue", order = 1)]
public class Dialogue : ScriptableObject
{
    public List<DialogueContent> dialogueItems = new();
}

