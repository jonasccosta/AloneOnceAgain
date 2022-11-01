using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterScript : MonoBehaviour
{
    public float speed = 5.0f;
    Rigidbody2D rigidBody;
    public Transform playerPos;

    bool isGrounded = true;
    bool intWObj = false;

    // Start is called before the first frame update
    void Start()
    {
        rigidBody = GetComponent<Rigidbody2D>();
        playerPos = gameObject.transform;
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 movement;
        movement.y = rigidBody.velocity.y;
        if(Input.GetKey(KeyCode.RightArrow) == true){
            movement.x = speed;    
        }
        else if (Input.GetKey(KeyCode.LeftArrow) == true){
            movement.x = -speed;
        }
        else{
            movement.x = 0;
        }
        rigidBody.velocity = movement;

        // Allow jumps only if the character is in the ground
        if (Input.GetKeyDown(KeyCode.UpArrow) && isGrounded){
            rigidBody.AddForce(new Vector2(0, speed), ForceMode2D.Impulse);
            isGrounded = false;
        }

        if (Input.GetKeyDown(KeyCode.X)){
            intWObj = true;
        }
        else if (Input.GetKeyUp(KeyCode.X)){
            intWObj = false;
            speed = 5.0f;
        }

    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        Rigidbody2D objectBody = collision.rigidbody;

        if (collision.gameObject.tag == "Floor")
        {
            //If the characters collides with the floor, then set isGrounded to true
            isGrounded = true;
        }
    }

    void OnCollisionStay2D(Collision2D collision)
    {
        Rigidbody2D objectBody = collision.rigidbody;

        // If the player collides if a movable object and presses X, they are able to push the object
        // The object needs to have a high mass so it doesn't slide too fast
        if (intWObj == true && collision.gameObject.tag == "MovableObject")
        {
            objectBody.constraints = RigidbodyConstraints2D.None;
            speed = 3.0f;
        }
        else if (intWObj == false && collision.gameObject.tag == "MovableObject")
        {
            objectBody.constraints = RigidbodyConstraints2D.FreezePositionX | RigidbodyConstraints2D.FreezePositionX | 
            RigidbodyConstraints2D.FreezePositionY | RigidbodyConstraints2D.FreezeRotation;
        }
    }
}
