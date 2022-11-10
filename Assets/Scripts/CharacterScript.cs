using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CharacterScript : MonoBehaviour
{
    public float speed = 3.0f;
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
        GameOver();
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        Rigidbody2D objectBody = collision.rigidbody;

        if (collision.gameObject.tag == "Floor" )
        {
            // If the characters collides with the floor, then set isGrounded to true
            isGrounded = true;
            
        }
         
        if (collision.gameObject.tag == "Obstacle"){
            CollisionWithObstacle(collision);
         }

        
    }

    void CollisionWithObstacle(Collision2D collision){

        // Find if Character has a MovableObject attached
        bool attached = false;
        for (var i = 0; i < transform.childCount; ++i) {
            if( transform.GetChild(i).tag == "MovableObject"){
                attached = true;
            }
            
        }

        // If the character collides with an obstacle without the dumpster, restart its position
        if(!attached) {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }

        // If the characters collides with an obstacle with the dumpster, remove the obstacle
        else {
            
           Destroy(collision.gameObject);
        }

    }

    // Update the Character Animation based on the current action the player is doing
    void Animate(){
        if (!isGrounded){
             anim.Play("Jumping Animation");
        }

        else if (Input.GetKey(KeyCode.RightArrow) == false && Input.GetKey(KeyCode.LeftArrow) == false && Input.GetKey(KeyCode.UpArrow) == false && Input.GetKey(KeyCode.X) == false && Input.GetKey(KeyCode.LeftShift) == false)
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
            rigidBody.AddForce(new Vector2(0, 1.3f*speed), ForceMode2D.Impulse);
            isGrounded = false;
            currentAction = "Jumping";
        }
    }

    void PushPull(){
        Vector2 movement;
        movement.y = rigidBody.velocity.y;

        if(Input.GetKey(KeyCode.X) == true){
            intWObj = true; 
            speed = 2.0f;   
            movement.x = speed;
            rigidBody.velocity = movement; 
        }
        else if (Input.GetKeyUp(KeyCode.X) == true){
            intWObj = false;
            speed = 3.0f;
            movement.x = speed;
            rigidBody.velocity = movement; 
        }

        

    }

    // Allows the character to run (by using left shift)
    void Run(){
        Vector2 movement;
        movement.y = rigidBody.velocity.y;
        

        if (Input.GetKey(KeyCode.LeftShift)){
             if(Input.GetKey(KeyCode.RightArrow) == true){
                speed = 6.5f;
                movement.x = speed;
                currentAction = "Running Foward";   
            }

            else if (Input.GetKey(KeyCode.LeftArrow) == true){
                speed = 6.5f;
                movement.x = -speed;
                currentAction = "Running Left";    
            
            }

            else {
                speed = 3.0f;
                movement.x = 0;    
            }


            rigidBody.velocity = movement; 
            
         
         }


    }

    // Restart the scene if Violet Drops
    void GameOver(){
        if (rigidBody.position.y < -5.0f){
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }
}
