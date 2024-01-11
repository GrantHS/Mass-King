using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DialogueManager : Singleton<DialogueManager>
{
    public GameObject dialogueCanvas;
    public TextMeshProUGUI characterName;
    public TextMeshProUGUI text;
    public Image iD;

    private bool _nextText = false;
    private void Start()
    {
        dialogueCanvas.SetActive(false);
    }
    private IEnumerator StartDialogue(DialogueData dialogue)
    {
        Time.timeScale = 0;
        dialogueCanvas.SetActive(true);
        iD.enabled = true;
        text.enabled = true;
        characterName.text = dialogue.characterName;
        iD.color = dialogue.characterColor;
        int numStrings = dialogue.text.Count;
        for (int i = 0; i < numStrings; i++)
        {
            text.text = dialogue.text[i];
            while(!_nextText) 
                yield return null;
            _nextText = false;
        }

        EndDialogue();
    }

    private void EndDialogue()
    {
        dialogueCanvas.SetActive(false);
        text.enabled = false;
        Time.timeScale = 1;
    }

    public void BeginDialogue(DialogueData dialogue)
    {
        StartCoroutine(StartDialogue(dialogue));
    }

    public void OnDialogueClicked()
    {
        _nextText = true;
    }

}
