using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueInteract : MonoBehaviour
{
    public DialogueManager dialogueManager;
    // Update is called once per frame
    void Update()
    {
        // Player presses X to advance on-screen dialogue
        if(Input.GetKeyDown(KeyCode.X)){
            dialogueManager.dialogueUIText.text = "";
            dialogueManager.AdvanceSentence();
        }
    }
}
