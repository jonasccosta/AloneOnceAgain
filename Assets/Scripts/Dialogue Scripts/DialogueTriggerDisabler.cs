using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueTriggerDisabler : MonoBehaviour
{
    public DialogueManager dialogueManager;
    public GameObject trigger;

    public void disableTrigger()
    {
        if (dialogueManager.endedDialogue == true){
            trigger.SetActive(false);
        }
    }
}
