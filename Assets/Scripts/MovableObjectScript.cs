using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Handles manipulation of the constraints of a movable object
public class MovableObjectScript : MonoBehaviour
{

    Rigidbody2D rigidBody;

    // Start is called before the first frame update
    void Start()
    {
        // Make object not movable at the beginning
        rigidBody = GetComponent<Rigidbody2D>();
        rigidBody.constraints = RigidbodyConstraints2D.FreezeAll;
    }

    // Update is called once per frame
    void Update()
    {
        CheckParent();
    }

    /* Unfreeze all constraints but rotation when it is the child of the player, 
     * which means that the player is pulling the object, otherwise keep the object
     * constrained
    */
    void CheckParent(){
       if(transform.parent != null){
            if(transform.parent.tag == "Player"){
                rigidBody.constraints = RigidbodyConstraints2D.None;
                rigidBody.constraints = RigidbodyConstraints2D.FreezeRotation;
            }

            else {
                rigidBody.constraints = RigidbodyConstraints2D.FreezeAll;
            }
       }
    }
}
