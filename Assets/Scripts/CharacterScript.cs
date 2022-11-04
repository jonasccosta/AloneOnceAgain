using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterScript : MonoBehaviour
{
    public float speed = 5.0f;
    Rigidbody2D rigidBody;
    bool isGrounded = true;
    bool intWObj = false;

    // Start is called before the first frame update
    void Start()
    {
        rigidBody = GetComponent<Rigidbody2D>();
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
        else {
            movement.x = 0;
        }
        rigidBody.velocity = movement;

        if(Input.GetKey(KeyCode.X) == true){
            intWObj = true; 
            speed = 3.0f;   
        }
        else if (Input.GetKeyUp(KeyCode.X) == true){
            intWObj = false;
            speed = 5.0f;
        }

        // Allow jumps only if the character is in the ground and is not interacting with an object (pushing/pulling)
        if (Input.GetKeyDown(KeyCode.UpArrow) && isGrounded && !intWObj){
            rigidBody.AddForce(new Vector2(0, speed), ForceMode2D.Impulse);
            isGrounded = false;
        }

        // Allows the character to run (by using left shift)
        if (Input.GetKeyDown(KeyCode.LeftShift)){
            speed = 6.5f;
        }
        else if (Input.GetKeyUp(KeyCode.LeftShift)){
            speed = 5.0f;
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        Rigidbody2D objectBody = collision.rigidbody;

        if (collision.gameObject.tag == "Floor")
        {
            // If the characters collides with the floor, then set isGrounded to true
            isGrounded = true;
        }
    }
}
