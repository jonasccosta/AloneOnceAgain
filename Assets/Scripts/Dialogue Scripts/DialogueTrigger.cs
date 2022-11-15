using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueTrigger : MonoBehaviour
{
    public DialogueTree dialogue;
    public DialogueManager dialogueManager;
    public GameObject disabler;

    public void OnTriggerEnter2D(Collider2D other){
        dialogueManager.StartDialogue(dialogue);
    }

    // Calls to disable the trigger once the player finishes the dialogue tree, when they exit the trigger
    public void OnTriggerExit2D(Collider2D other){
        disabler.GetComponent<DialogueTriggerDisabler>().disableTrigger();
    }
}
