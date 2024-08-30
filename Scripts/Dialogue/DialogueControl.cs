using System.Collections;
using System.Collections.Generic;
using Unity.Collections.LowLevel.Unsafe;
using UnityEngine;
using UnityEngine.UI; // para trabalhar com o canvas, interface de usuário

public class DialogueControl : MonoBehaviour
{
    public enum idiom
    {
        pt,
        eng,
    }
    public idiom language;

    [Header("Components")]
    public GameObject dialogueObj; // janela do dialogo
    public Image profileSprite; // sprite do perfil
    public Text speechText; // texto da fala
    public Text actorNameText; // nome do npc


    [Header("Settings")]
    public float typingSpeed; // velocidade da fala
    private bool isShowing;// Variáveis de controle
    public bool IsShowing() // se a janela está visivel
    {
        return isShowing;
    }
    private int index; // rodar em um laço de repetição - index das sentenças
    private string[] sentences;

    public static DialogueControl instance;
    private Coroutine currentDialogueCoroutine;

//awake é chamados antes que todos os Start() na hierarquia de execução de scripts
    private void Awake()
    {
        instance = this;
    }

//é chamado ao inicializar
    void Start()
    {
       
    }

    IEnumerator TypeSentence()
    {
        yield return new WaitForSeconds(0.5f);

        foreach(char letter in sentences[index].ToCharArray())
        {
            speechText.text += letter;
            yield return new WaitForSeconds(typingSpeed);
        }
    }

    private void ClearSpeechText()
    {
        speechText.text = "";
    }

    private void StopCurrentDialogue()
    {
        if (currentDialogueCoroutine != null)
        {
            StopCoroutine(currentDialogueCoroutine);
            currentDialogueCoroutine = null;
        }
    }

    private void StartNewSentence()
    {
        StartCoroutine(TypeSentence());
    }

    //pula para proxima frase/fala
    public void NextSentence()
    {
        if (speechText.text.Length == sentences[index].Length)
        {
            if (index < sentences.Length - 1)
            {
                index++;
                ClearSpeechText();
                StartNewSentence();
            }
            else
            {
                ClearSpeechText();
                index = 0;
                dialogueObj.SetActive(false);
                sentences = null;
            }
        }
        
    }

//chamar a fala do NPC
    public void Speech(string[] txt)
    {
        if(!isShowing)
        {
            ClearSpeechText();
            StopCurrentDialogue();
            dialogueObj.SetActive(true);
            sentences = txt;
            currentDialogueCoroutine = StartCoroutine(TypeSentence());
            isShowing = true;
            
        }
    }

    public void EndDialogue()
    {
        dialogueObj.SetActive(false);
        isShowing = false;
    }
    
}
