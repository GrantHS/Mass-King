using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

[CreateAssetMenu(fileName = "DialogueScriptableObject", menuName = "Scriptable Objects/Dialogue Data")]
public class DialogueData : ScriptableObject
{
    public List<string> text;
    public string characterName;
    public Color characterColor;


}
