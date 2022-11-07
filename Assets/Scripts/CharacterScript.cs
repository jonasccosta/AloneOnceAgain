using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterScript : MonoBehaviour
{
    public float speed = 5.0f;
    Rigidbody2D rigidBody;
    bool isGrounded = true;
    bool intWObj = false;
    public Animator anim;

    // Stores current action of the character or idle if there is no action
    public string currentAction = "Idle";

    // Start is called before the first frame update
    void Start()
    {
        rigidBody = GetComponent<Rigidbody2D>();
        anim = gameObject.GetComponentInChildren(typeof(Animator)) as Animator;
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 movement;
        movement.y = rigidBody.velocity.y;

        if(Input.GetKey(KeyCode.RightArrow) == true){
            movement.x = speed;
            currentAction = "Walking Foward";    
        }

        else if (Input.GetKey(KeyCode.LeftArrow) == true){
            movement.x = -speed;
            currentAction = "Walking To Left";    
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
            currentAction = "Jumping";
        }


        // Allows the character to run (by using left shift)
        if (Input.GetKeyDown(KeyCode.LeftShift)){
            speed = 6.5f;
            currentAction = "Running Foward";
        }
        else if (Input.GetKeyUp(KeyCode.LeftShift)){
            speed = 5.0f;
            currentAction = "Running Left";
        }

        Animate();
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

    void Animate(){
        if (currentAction == "Walking Foward"){
             anim.Play("Walking Foward Animation");
        }

        else if (currentAction == "Running Foward"){
            anim.Play("Running Foward Animation");
        }

        else if (currentAction == "Running Left"){
            anim.Play("Running Left Animation");
        }

        else if (currentAction == "Jumping"){
            anim.Play("Jumping Animation");
        }

        else if (currentAction == "Walking To Left"){
            anim.Play("Walking To Left Animation");
        }

        else {
            anim.Play("Idle Animation");
        }
    }
}
