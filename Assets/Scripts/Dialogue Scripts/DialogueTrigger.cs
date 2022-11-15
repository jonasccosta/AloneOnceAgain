using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueTrigger : MonoBehaviour
{
    public DialogueTree dialogue;
    public DialogueManager dialogueManager;

    // Only allows for one interation of a dialogue tree to be executed (no repeat dialogue)
    public void OnTriggerEnter2D(Collider2D other){
        dialogueManager.StartDialogue(dialogue);
    }
}
