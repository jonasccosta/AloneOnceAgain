using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShowControllers : MonoBehaviour
{
    
    public GameObject controllerImage;
    private static int count = 0; // count is used to not show controolers more than once

    // Start is called before the first frame update
    void Start()
    {
        controllerImage.SetActive(false);
    }

    void OnTriggerEnter2D(Collider2D other)
    {   // Function that shows game controllers if player is standing on
        //designated trigger and count is smaller than one

        if (other.tag == "Player" && count < 1)
        {
            Debug.Log("siuuuuuuuuuuuuuuuuu");

            controllerImage.SetActive(true);
            
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {   //functions that hides the controllers images if player exits the trigger
        Debug.Log("me saliiiiiiii");
        controllerImage.SetActive(false);
        count ++;

    }


}
