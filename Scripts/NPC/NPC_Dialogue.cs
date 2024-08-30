using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPC_Dialogue : MonoBehaviour
{
    public float dialogueRange;
    public LayerMask playerLayer;
    public DialogueSettings dialogue;
    private bool playerRit = false;
    private bool isDialogueActive = false;
    public bool IsDialogueActive()
    {
        return isDialogueActive;
    }

    private List<string> sentences = new List<string>();

    void Update()
    {

        if(Input.GetKeyDown(KeyCode.F) && playerRit)
        {
            if(DialogueControl.instance != null)
            {
                if(DialogueControl.instance.IsShowing())
                {
                    DialogueControl.instance.NextSentence();
                }
                else
                {
                    DialogueControl.instance.Speech(sentences.ToArray());
                    isDialogueActive = true;
                }
            }
        }
    }

    void ShowDialogue()
    {
        Collider2D hit = Physics2D.OverlapCircle(transform.position, dialogueRange, playerLayer);
        playerRit = (hit != null);
        
        if(!playerRit && isDialogueActive)
        {
            if (DialogueControl.instance != null && DialogueControl.instance.IsShowing())
            {
                DialogueControl.instance.EndDialogue();
                isDialogueActive = false;
            }
        }
    }

    void FixedUpdate()
    {
        ShowDialogue();
    }
    private void Start()
    {
        GetNPCInfo();
    }

    void GetNPCInfo()
    {
        for(int i = 0; i < dialogue.dialogues.Count; i++)
        {
            switch(DialogueControl.instance.language)
            {
                case DialogueControl.idiom.pt:
                    sentences.Add(dialogue.dialogues[i].sentence.portuguese);
                    break;

                case DialogueControl.idiom.eng:
                    sentences.Add(dialogue.dialogues[i].sentence.english);
                    break;
            }
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(transform.position, dialogueRange);
    }

}
