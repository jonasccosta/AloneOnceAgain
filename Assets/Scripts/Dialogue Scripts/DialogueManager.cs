using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DialogueManager : MonoBehaviour
{
    public TextMeshProUGUI dialogueUIText;
    public Canvas dialogueCanvas;
    public GameObject character;
    public GameObject dialogueBorder;
    public GameObject characterPortrait;
    public GameObject audioSource;
    public bool endedDialogue;
    public bool skip;
    public bool typing;

    private DialogueTree dialogue;
    private Sentence currentSentence = null;
    private string currentFacePortrait = null;

    // Sprites for Violet's character portraits
    Sprite VIOLETNEUTRAL;
    Sprite VIOLETANGRY;
    Sprite VIOLETHAPPY;
    Sprite VIOLETSAD;
    Sprite VIOLETAFRAID;
    Sprite VIOLETSURPRISE;
    Sprite FATHER;
    Sprite FRIENDS;

    void Start(){
        VIOLETNEUTRAL =  Resources.Load <Sprite>("CharacterPortraits/Portrait_Neutral");
        VIOLETANGRY = Resources.Load <Sprite>("CharacterPortraits/Portrait_Angry");
        VIOLETHAPPY = Resources.Load <Sprite>("CharacterPortraits/Portrait_Happy");
        VIOLETSAD = Resources.Load <Sprite>("CharacterPortraits/Portrait_Sad");
        VIOLETAFRAID = Resources.Load <Sprite>("CharacterPortraits/Portrait_Afraid");
        VIOLETSURPRISE = Resources.Load <Sprite>("CharacterPortraits/Portrait_Surprise");
        FATHER = Resources.Load <Sprite>("CharacterPortraits/Portrait_Father");
        FRIENDS = Resources.Load <Sprite>("CharacterPortraits/Portrait_Friends");
    }

    // Disables player movement when starting dialogue
    public void StartDialogue(DialogueTree dialogueTree){
        dialogue = dialogueTree;
        endedDialogue = false;
        skip = false;
        typing = false;
        currentSentence = dialogue.startingSentence;
        dialogueCanvas.enabled = true;
        dialogueBorder.SetActive(true);
        characterPortrait.SetActive(true);
        character.GetComponent<CharacterScript>().currentAction = "Idle";
        character.GetComponent<CharacterScript>().anim.Play("Idle Animation");
        character.GetComponent<CharacterScript>().walk.Pause();
        character.GetComponent<CharacterScript>().jump.Pause();
        character.GetComponent<CharacterScript>().enabled = false;
        DisplaySentence();
    }

    public void AdvanceSentence(){
        currentSentence = currentSentence.nextSentence;
        skip = false;
        typing = false;
        DisplaySentence();
    }

    public void DisplaySentence(){
        if (currentSentence == null){
            EndDialogue();
            return;
        }
        string sentence = currentSentence.text;
        if (currentSentence.pastDialogue == true){
            dialogueUIText.fontStyle = (FontStyles) FontStyle.Italic;
        }
        else{
            dialogueUIText.fontStyle = (FontStyles) FontStyle.Normal;
        }

        if (currentSentence.attachedEvent == "EnterHouse"){
            Destroy(character);
            audioSource.GetComponent<AudioSource>().enabled = true;
        }

        StopAllCoroutines();
        if(!skip){
            StartCoroutine(TypeSentence(sentence));
        }
        else if(skip){
            dialogueUIText.text = sentence;
        }
    }

    IEnumerator TypeSentence(string sentence){
        typing = true;
        dialogueUIText.text = "";
        SetCharacterPortrait();
        foreach(char letter in sentence.ToCharArray()){
            dialogueUIText.text += letter;
            if (!skip){
                yield return new WaitForSeconds(0.05f);
            }
        }
        typing = false;
    }

    void SetCharacterPortrait(){
        currentFacePortrait = currentSentence.attachedFacePortrait;
        if (currentFacePortrait == "VioletNeutral"){
            characterPortrait.GetComponent<Image>().sprite = VIOLETNEUTRAL;
        }
        else if (currentFacePortrait == "VioletAngry"){
            characterPortrait.GetComponent<Image>().sprite = VIOLETANGRY;
        }
        else if (currentFacePortrait == "VioletHappy"){
            characterPortrait.GetComponent<Image>().sprite = VIOLETHAPPY;
        }
        else if (currentFacePortrait == "VioletSad"){
            characterPortrait.GetComponent<Image>().sprite = VIOLETSAD;
        }
        else if (currentFacePortrait == "VioletAfraid"){
            characterPortrait.GetComponent<Image>().sprite = VIOLETAFRAID;
        }
        else if (currentFacePortrait == "VioletSurprise"){
            characterPortrait.GetComponent<Image>().sprite = VIOLETSURPRISE;
        }
        else if (currentFacePortrait == "Father"){
            characterPortrait.GetComponent<Image>().sprite = FATHER;
        }
        else if (currentFacePortrait == "Friends"){
            characterPortrait.GetComponent<Image>().sprite = FRIENDS;
        }
    }

    void EndDialogue(){
        dialogueUIText.text = "";
        endedDialogue = true;
        dialogueBorder.SetActive(false);
        characterPortrait.SetActive(false);
        character.GetComponent<CharacterScript>().enabled = true;
    }
}
