using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DumpsterScript : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    // Collision between the dumpster and obstacles destroy the obstacle if the dumbster is 
    // attached to the character
    void OnCollisionEnter2D(Collision2D collision)
    {
        if(transform.parent != null){
            if(transform.parent.tag == "Player" && collision.gameObject.tag == "Obstacle"){
                Destroy(collision.gameObject);
            }
       }
    }
}
