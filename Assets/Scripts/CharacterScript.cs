using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterScript : MonoBehaviour
{
    public float speed = 4.0f;
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
        Walk();
        Jump();
        PushPull();
        Run();
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

    // Update the Character Animation based on the current action the player is doing
    void Animate(){
        if (Input.GetKey(KeyCode.RightArrow) == false && Input.GetKey(KeyCode.LeftArrow) == false && Input.GetKey(KeyCode.UpArrow) == false && Input.GetKey(KeyCode.X) == false && Input.GetKey(KeyCode.LeftShift) == false)
        {
            currentAction = "Idle";
            anim.Play("Idle Animation");;
        }

        else if (currentAction == "Jumping"){
            anim.Play("Jumping Animation");
        }

        else if (isGrounded){

            if (currentAction == "Walking Foward"){
                anim.Play("Walking Foward Animation");
            }

            else if (currentAction == "Running Foward"){
                anim.Play("Running Foward Animation");
            }

            else if (currentAction == "Running Left"){
                anim.Play("Running Left Animation");
            }

            else if (currentAction == "Walking To Left"){
                anim.Play("Walking To Left Animation");
            }
        }


    }

    // Updates the character x's position, moving it to the right or left based on user input
    void Walk(){
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
        
        
    }

    // Allow jumps only if the character is in the ground and is not interacting with an object (pushing/pulling)
    void Jump(){
        if (Input.GetKeyDown(KeyCode.UpArrow) && isGrounded && !intWObj){
            rigidBody.AddForce(new Vector2(0, speed), ForceMode2D.Impulse);
            isGrounded = false;
            currentAction = "Jumping";
        }
    }

    void PushPull(){
        if(Input.GetKey(KeyCode.X) == true){
            intWObj = true; 
            speed = 3.0f;   
        }
        else if (Input.GetKeyUp(KeyCode.X) == true){
            intWObj = false;
            speed = 5.0f;
        }

    }

    // Allows the character to run (by using left shift)
    void Run(){
        Vector2 movement;
        movement.y = rigidBody.velocity.y;
        speed = 6.5f;

        if (Input.GetKey(KeyCode.LeftShift)){
             if(Input.GetKey(KeyCode.RightArrow) == true){
                movement.x = speed;
                currentAction = "Running Foward";   
            }

            else if (Input.GetKey(KeyCode.LeftArrow) == true){
                movement.x = -speed;
                currentAction = "Running Left";    
            
            }

            else {
                movement.x = 0;    
            }


            rigidBody.velocity = movement; 
            
         
         }

        
    }
}
