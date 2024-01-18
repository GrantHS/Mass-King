using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

[CreateAssetMenu(fileName = "DialogueScriptableObject", menuName = "Scriptable Objects/Dialogue Data")]
public class DialogueData : ScriptableObject
{
    public List<string> text;
    public string altText;
    [Tooltip("Which of the above elements require a player response")]
    public List<int> reactables;
    public string posReactionText, negReactionText;
    public string characterName;
    public Color characterColor;
    public int idNumber;
    public bool tutorial;
    


}
