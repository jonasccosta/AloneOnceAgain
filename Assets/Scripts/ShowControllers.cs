using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShowControllers : MonoBehaviour
{
    
    public GameObject controllerImage;
    private static int count = 0;

    // Start is called before the first frame update
    void Start()
    {
        controllerImage.SetActive(false);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player" && count < 1)
        {
            controllerImage.SetActive(true);    
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        controllerImage.SetActive(false);
        count++;
    }


}
