using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
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

    [SerializeField]
    private List<DialogueData> dialogues;

    private int numReactions = 0;
    private int branch = 0;

    private bool _nextText = false;
    private bool posReact = true;
    private bool tutorial = true;
    private bool inDialogue = false;
    private bool answered = false;

    public bool Tutorial { get => tutorial; set => tutorial = value; }
    public bool InDialogue { get => inDialogue; private set => inDialogue = value; }

    private void Start()
    {
        dialogueCanvas.SetActive(false);
    }
    private IEnumerator StartDialogue(DialogueData dialogue, GameObject speaker)
    {
        Time.timeScale = 0;
        text.text = dialogue.text[0];
        dialogueCanvas.SetActive(true);
        reactions.SetActive(false);
        iD.enabled = true;
        inDialogue = true;
        posReact = true;
        branch = 0;
        answered = false;
       
        characterName.text = dialogue.characterName;
        iD.color = dialogue.characterColor;
        int numStrings = dialogue.text.Count;
        for (int temp = 0; temp < numStrings; temp++)
        {
            Debug.Log("temp: " + temp);
            text.enabled = true;
            reactions.SetActive(false);
            //Debug.Log(branch);
            if (branch == 0)
            text.text = dialogue.text[temp];
            else
                text.text = dialogue.altText;
            while(!_nextText) 
                yield return null;

            if (text.text == dialogue.altText)
            {
                EndDialogue(speaker);
                CheckNextDialogue(dialogue);
                break;
            }
                

            _nextText = false;

            foreach(int reactable in dialogue.reactables)
            {
                //Debug.Log("temp: " + temp);
                Debug.Log("react: " + reactable);
                if (temp == reactable)
                {
                    text.enabled = false;
                    reactions.SetActive(true);
                    posReaction.text = dialogue.posReactionText;
                    negReaction.text = dialogue.negReactionText;
                    numReactions++;
                    while (!answered)
                        yield return null;


                }
            }

            answered = false;



            if (!posReact)
            {
                temp = 0;
                branch = 1;
                if (dialogue.idNumber == 1) Tutorial = false;
            }

           

        }

        EndDialogue(speaker);
        CheckNextDialogue(dialogue);
    }

    private IEnumerator StartDialogue(DialogueData dialogue)
    {

        Time.timeScale = 0;
        text.text = dialogue.text[0];
        dialogueCanvas.SetActive(true);
        reactions.SetActive(false);
        iD.enabled = true;
        inDialogue = true;
        posReact = true;
        branch = 0;
        answered = false;

        characterName.text = dialogue.characterName;
        iD.color = dialogue.characterColor;
        int numStrings = dialogue.text.Count;
        //Debug.Log(numStrings);
        for (int temp = 0; temp < numStrings; temp++)
        {
            Debug.Log("temp: " + temp);
            text.enabled = true;
            reactions.SetActive(false);

            //Debug.Log(branch);

            if (branch == 0) text.text = dialogue.text[temp];
            else text.text = dialogue.altText;

            while (!_nextText)
                yield return null;

            if (text.text == dialogue.altText)
            {
                Debug.Log("ending talk");
                EndDialogue();
                CheckNextDialogue(dialogue);
                break;
            }


            _nextText = false;

            foreach (int reactable in dialogue.reactables)
            {
                //Debug.Log("temp: " + temp);
                Debug.Log("react: " + reactable);
                Debug.Log("temp: " + temp);
                if (temp == reactable)
                {
                    text.enabled = false;
                    reactions.SetActive(true);
                    posReaction.text = dialogue.posReactionText;
                    negReaction.text = dialogue.negReactionText;
                    numReactions++;
                    while (!answered)
                        yield return null;


                }
            }

            answered = false;

            if (dialogue.reactables == null)
            {
                Debug.Log("null react");
            }



            if (!posReact)
            {
                temp = 0;
                branch = 1;
                if (dialogue.idNumber == 1) Tutorial = false;
            }

           

        }

        Debug.Log("ending of loop");
        EndDialogue();
        CheckNextDialogue(dialogue);
    }

    private void CheckNextDialogue(DialogueData dialogue)
    {
        if (dialogue.idNumber == 2 && Tutorial) BeginDialogue(dialogues[0]);
        if (dialogue.idNumber == 4 && Tutorial) BeginDialogue(dialogues[1]);
        if (dialogue.idNumber == 7 && Tutorial) BeginDialogue(dialogues[2]);
    }

    private void EndDialogue(GameObject speaker)
    {
        branch = 0;
        dialogueCanvas.SetActive(false);
        text.enabled = false;
        numReactions = 0;
        Time.timeScale = 1;
        speaker.SetActive(false);
        inDialogue = false;

    }

    private void EndDialogue()
    {
        branch = 0;
        dialogueCanvas.SetActive(false);
        text.enabled = false;
        numReactions = 0;
        Time.timeScale = 1;
        inDialogue = false;
    }

    public void BeginDialogue(DialogueData dialogue, GameObject speaker)
    {
        Debug.Log("starting talk");
        StartCoroutine(StartDialogue(dialogue, speaker));
    }
    public void BeginDialogue(DialogueData dialogue)
    {
        Debug.Log("starting talk");
        StartCoroutine(StartDialogue(dialogue));
    }


    public void OnPositiveReaction()
    {
        posReact = true;
        answered = true;
    }

    public void OnNegativeReaction()
    {
        posReact = false;
        answered = true;
    }

    public void OnDialogueClicked()
    {
        
            _nextText = true;
    }

    

}
