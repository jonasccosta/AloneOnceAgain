using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DialogueManager : MonoBehaviour
{
    public TextMeshProUGUI dialogueUIText;
    public Canvas dialogueCanvas;
    public GameObject character;
    public GameObject dialogueBorder;

    private DialogueTree dialogue;
    private Sentence currentSentence = null;

    // Disables player movement when starting dialogue
    public void StartDialogue(DialogueTree dialogueTree){
        dialogue = dialogueTree;
        currentSentence = dialogue.startingSentence;
        dialogueCanvas.enabled = true;
        dialogueBorder.SetActive(true);
        character.GetComponent<CharacterScript>().currentAction = "Idle";
        character.GetComponent<CharacterScript>().anim.Play("Idle Animation");
        character.GetComponent<CharacterScript>().enabled = false;
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
            yield return new WaitForSeconds(0.05f);
        }
    }

    void EndDialogue(){
        dialogueUIText.text = "";
        dialogueBorder.SetActive(false);
        character.GetComponent<CharacterScript>().enabled = true;
    }
}
