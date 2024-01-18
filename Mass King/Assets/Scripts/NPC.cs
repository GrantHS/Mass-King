using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPC : MonoBehaviour
{
    [SerializeField]
    private DialogueData dialogueData;
    private bool hasSpoken = false;
    public bool leaveAfterTalk;
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player") && !hasSpoken)
        {
            if (leaveAfterTalk)
            {
                DialogueManager.Instance.BeginDialogue(dialogueData, this.gameObject);
            }
            else DialogueManager.Instance.BeginDialogue(dialogueData);

            hasSpoken = true;
        }
    }
}
