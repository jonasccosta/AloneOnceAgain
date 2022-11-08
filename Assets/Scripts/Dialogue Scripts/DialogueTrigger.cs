using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueTrigger : MonoBehaviour
{
    public DialogueTree dialogue;
    public DialogueManager dialogueManager;

    public void OnTriggerEnter2D(Collider2D other){
        dialogueManager.StartDialogue(dialogue);
    }
}
