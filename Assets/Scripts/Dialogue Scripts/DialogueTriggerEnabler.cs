using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueTriggerEnabler : MonoBehaviour
{
    public GameObject trigger;
    public GameObject enabler;
    private static bool introSeen = false;
    private static bool dumpsterSeen = false;
    private static bool truckSeen = false;
    private static bool skateSeen = false;

    // Only allows for one iteration of dialogue to be played, even after dying
    // Note: This is an inefficient method, but for now it works
    public void OnTriggerEnter2D(Collider2D other){
        if (!introSeen){
            trigger.SetActive(true);
        }
        else if (introSeen && !dumpsterSeen && this.gameObject.name == "DumpsterEnabler"){
            trigger.SetActive(true);
        }
        else if (introSeen && dumpsterSeen && !truckSeen && this.gameObject.name == "TruckEnabler"){
            trigger.SetActive(true);
        }
        else if (introSeen && dumpsterSeen && truckSeen && !skateSeen && this.gameObject.name == "SkateEnabler"){
            trigger.SetActive(true);
        }
    }

    public void OnTriggerExit2D(Collider2D other){
        enabler.SetActive(false);
        if(this.gameObject.name == "IntroEnabler"){
            introSeen = true;
        }
        else if(this.gameObject.name == "DumpsterEnabler"){
            dumpsterSeen = true;
        }
        else if(this.gameObject.name == "TruckEnabler"){
            truckSeen = true;
        }
        else if(this.gameObject.name == "SkateEnabler"){
            skateSeen = true;
        }
    }
}
