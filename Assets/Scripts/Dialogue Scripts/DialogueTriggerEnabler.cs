using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueTriggerEnabler : MonoBehaviour
{
    public GameObject trigger;
    public GameObject enabler;

    public void OnTriggerEnter2D(Collider2D other){
        trigger.SetActive(true);
    }

    public void OnTriggerExit2D(Collider2D other){
        enabler.SetActive(false);
    }
}
