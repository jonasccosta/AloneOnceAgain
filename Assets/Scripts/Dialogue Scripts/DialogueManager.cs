using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DialogueManager : MonoBehaviour
{
    public TextMeshProUGUI dialogueUIText;
    public Canvas dialogueCanvas;

    private DialogueTree dialogue;
    private Sentence currentSentence = null;

    public void StartDialogue(DialogueTree dialogueTree){
        dialogue = dialogueTree;
        currentSentence = dialogue.startingSentence;
        dialogueCanvas.enabled = true;
        DisplaySentence();
    }

    public void AdvanceSentence(){
        currentSentence = currentSentence.nextSentence;
        DisplaySentence();
    }

    public void DisplaySentence(){
        if (currentSentence == null){
            EndDialogue();
            return;
        }
        string sentence = currentSentence.text;
        dialogueUIText.text = sentence;
        StopAllCoroutines();
        StartCoroutine(TypeSentence(sentence));
    }

    IEnumerator TypeSentence(string sentence){
        dialogueUIText.text = "";
        foreach(char letter in sentence.ToCharArray()){
            dialogueUIText.text += letter;
            print(dialogueUIText.text);
            yield return new WaitForSeconds(0.05f);
        }
    }

    void EndDialogue(){
        dialogueUIText.text = "";
    }
}
