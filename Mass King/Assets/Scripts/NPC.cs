using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPC : MonoBehaviour
{
    [SerializeField]
    private DialogueData dialogueData;
    private bool hasSpoken = false;
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player") && !hasSpoken)
        {
            DialogueManager.Instance.BeginDialogue(dialogueData, this.gameObject);
            hasSpoken = true;
        }
    }
}
