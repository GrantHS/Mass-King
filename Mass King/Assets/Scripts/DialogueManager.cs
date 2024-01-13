using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DialogueManager : Singleton<DialogueManager>
{
    public GameObject dialogueCanvas;
    public GameObject reactions;
    public TextMeshProUGUI posReaction, negReaction;
    public TextMeshProUGUI characterName;
    public TextMeshProUGUI text;
    public Image iD;

    private int numReactions = 0;

    private bool _nextText = false;
    private bool posReact = true;

    private void Start()
    {
        dialogueCanvas.SetActive(false);
    }
    private IEnumerator StartDialogue(DialogueData dialogue, GameObject speaker)
    {
        int branch = 0;
        Time.timeScale = 0;
        text.text = dialogue.text[0];
        dialogueCanvas.SetActive(true);
        reactions.SetActive(false);
        iD.enabled = true;
       
        characterName.text = dialogue.characterName;
        iD.color = dialogue.characterColor;
        int numStrings = dialogue.text.Count;
        for (int temp = 0; temp < numStrings; temp++) //change back to default i
        {
            text.enabled = true;
            reactions.SetActive(false);

            if (branch == 0)
            text.text = dialogue.text[temp];
            else
                text.text = dialogue.altText;
            while(!_nextText) 
                yield return null;

            if (text.text == dialogue.altText)
            {
                EndDialogue(speaker);
                break;
            }
                

            _nextText = false;

            foreach(int reactable in dialogue.reactables)
            {
                if(temp == reactable)
                {
                    text.enabled = false;
                    reactions.SetActive(true);
                    posReaction.text = dialogue.posReactionText;
                    negReaction.text = dialogue.negReactionText;
                    numReactions++;
                    while (!_nextText)
                        yield return null;


                }
            }

            

            if (!posReact)
            {
                temp = 0;
                branch = 1;
            }

            _nextText = false;

        }

        EndDialogue(speaker);
    }

    private void EndDialogue(GameObject speaker)
    {
        dialogueCanvas.SetActive(false);
        text.enabled = false;
        numReactions = 0;
        Time.timeScale = 1;
        speaker.SetActive(false);
    }

    public void BeginDialogue(DialogueData dialogue, GameObject speaker)
    {
        StartCoroutine(StartDialogue(dialogue, speaker));
    }

    public void OnPositiveReaction()
    {
        posReact = true;
        _nextText = true;
    }

    public void OnNegativeReaction()
    {
        posReact = false;
        _nextText = true;
    }

    public void OnDialogueClicked()
    {
        if (!_nextText)
            _nextText = true;
    }

    

}
