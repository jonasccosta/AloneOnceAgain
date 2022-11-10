using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShowControllers : MonoBehaviour
{
    
    public GameObject controllerImage;

    // Start is called before the first frame update
    void Start()
    {
        controllerImage.SetActive(false);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            Debug.Log("siuuuuuuuuuuuuuuuuu");

            controllerImage.SetActive(true);
            
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        Debug.Log("me saliiiiiiii");
        controllerImage.SetActive(false);

    }


}
