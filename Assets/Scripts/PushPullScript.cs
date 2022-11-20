using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PushPullScript : MonoBehaviour
{
    Rigidbody2D playerBody;
    bool pushPull;
    // Start is called before the first frame update
    void Start()
    {
        playerBody = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        // Keeps track of if player is holding down X to push/pull objects
        if(Input.GetKey(KeyCode.X)){
            pushPull = true; 
        }
        else if (Input.GetKeyUp(KeyCode.X)){
            pushPull = false;
        }

        for (var i = 0; i < transform.childCount; ++i) {
            if( transform.GetChild(i).tag == "MovableObject" && !pushPull){
                transform.GetChild(i).SetParent(null);
            }
            
        }
    }

    // When the character enters a trigger collision with an object that is tagged as 
    // "MovableObject", makes it the parent of the player so it moves in accordance to the player
    // Allows player to push/pull correctly tagged objects
    public void OnTriggerEnter2D(Collider2D obj)
    {
        if (obj.tag == "MovableObject" && pushPull){
            obj.transform.SetParent(playerBody.transform);
            print("parent made"); // for debugging
        }
    }

    // Removes the player as a parent from the object
    public void OnTriggerExit2D(Collider2D obj)
    {
        if (obj.tag == "MovableObject" && !pushPull){
            obj.transform.SetParent(null);
            print("detached children"); // for debugging
        }
    }
}
