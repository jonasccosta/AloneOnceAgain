using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class CharacterScript : MonoBehaviour
{
    public float speed = 3.0f;
    Rigidbody2D rigidBody;
    bool isGrounded = true;
    bool intWObj = false;
    public Animator anim;
    public bool dead = false;
    private SanityScript sanityScript;
    public GameObject transitionCanvas;
    private TransitionScript transitionScript;
    public GameObject gameOverScreen;
    string reason = "";
    public GameObject reasonForDying;
    public AudioSource scream;
    public AudioSource walk;

    private bool onFloorLastFrame = false;

    public AudioSource jump;
    bool skateboarding = false;
    PauseMenu pauseMenu; // to call function 
    

    // Stores current action of the character or idle if there is no action
    public string currentAction = "Idle";

    // Start is called before the first frame update
    void Start()
    {
        transitionScript = transitionCanvas.GetComponent<TransitionScript>();
        StartCoroutine(transitionScript.FadeOutBlackSquare());
        rigidBody = GetComponent<Rigidbody2D>();
        anim = gameObject.GetComponentInChildren(typeof(Animator)) as Animator;
        sanityScript = gameObject.GetComponent<SanityScript>();
        gameOverScreen.SetActive(false);  
        pauseMenu = FindObjectOfType<PauseMenu>(); 
        
    }

    // Update is called once per frame
    void Update()
    {   
        Idle();
        Walk();
        Jump();
        PushPull();
        Run();
        Animate();
        GameOver();
        PlaySound();
        stopAnimation();
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        Rigidbody2D objectBody = collision.rigidbody;

        if (collision.gameObject.name == "Skateboard"){
            skateboarding = true;
        }

        else{
            skateboarding = false;
        }

        if (collision.gameObject.tag == "Floor" || (isGrounded == false && collision.gameObject.tag == "MovableObject"))
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
            dead = true;
            scream.Play();
            reason = "debris";
        } 

        // If the characters collides with an obstacle with the dumpster, remove the obstacle
        else {
           Destroy(collision.gameObject);
        }

    }

    // Flips a sprite if it is moving from right to left
    void FlipSprite(){
        Vector2 scale = transform.localScale;
        if( rigidBody.velocity.x < 0.0f && scale.x > 0.01f && !intWObj){
            scale.x *= -1.0f;
            transform.localScale = scale;
        }

        else if (rigidBody.velocity.x > 0.0f && scale.x < 0.01f && !intWObj){
            scale.x *= -1.0f;
            transform.localScale = scale;
        }
        
    }

    // Update the Character Animation based on the current action the player is doing
    void Animate(){

        // Check if it the sprite needs to be fliped
        FlipSprite();

        if (dead || sanityScript.dead)
         {
            anim.Play("Death Animation");
            StartCoroutine("GameOverScreen");
            StartCoroutine("Dead");
            if(sanityScript.dead){
                reason = "sanityDeath";
            }
         }


        else if (!isGrounded){
             anim.Play("Jumping Animation");

             // If running and jumping, decrease the animation speed
             if(speed > 3.0f){
                anim.speed = 0.6f;
            }

            else{
                anim.speed = 0.75f;
            }
        }

        else{
            anim.Play(currentAction + " Animation");

        }


    }

    // Set current action to idle if none of the game controllers are being pressed
    void Idle(){
        if (Input.GetKey(KeyCode.RightArrow) == false && Input.GetKey(KeyCode.LeftArrow) == false && Input.GetKey(KeyCode.UpArrow) == false && Input.GetKey(KeyCode.X) == false && Input.GetKey(KeyCode.LeftShift) == false){
            currentAction = "Idle";
            intWObj = false;
            speed = 3.0f;
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
            currentAction = "Walking Foward";    
        
        }

        else {
            movement.x = 0;    
        }


        rigidBody.velocity = movement; 
        
        
    }

    // Allow jumps only if the character is in the ground and is not interacting with an object (pushing/pulling)
    void Jump(){
        if (Input.GetKeyDown(KeyCode.UpArrow) && isGrounded && !intWObj){
            if(speed > 3.0f){
                 rigidBody.AddForce(new Vector2(0, 1.5f*speed), ForceMode2D.Impulse);
            }

            else{
                rigidBody.AddForce(new Vector2(0, 2.5f*speed), ForceMode2D.Impulse);
            }
           
            isGrounded = false;
            currentAction = "Jumping";


        }
    }

    void PushPull(){
        Vector2 movement;
        movement.y = rigidBody.velocity.y;

        // Push if moving foward
        if(Input.GetKey(KeyCode.RightArrow) == true){
            if(Input.GetKey(KeyCode.X) == true){
                intWObj = true; 
                speed = 2.0f;   
                movement.x = speed;
                rigidBody.velocity = movement; 
                currentAction = "Pushing";
            }
            else if (Input.GetKeyUp(KeyCode.X) == true){
                intWObj = false;
                speed = 3.0f;
                movement.x = speed;
                rigidBody.velocity = movement; 
                currentAction = "Pushing";
         }

        }

        // Push if moving to the left
        else if (Input.GetKey(KeyCode.LeftArrow) == true){
            if(Input.GetKey(KeyCode.X) == true){
                intWObj = true; 
                speed = -2.0f;   
                movement.x = speed;
                rigidBody.velocity = movement; 
                currentAction = "Pushing";
            }
            else if (Input.GetKeyUp(KeyCode.X) == true){
                intWObj = false;
                speed = 3.0f;
                movement.x = speed;
                rigidBody.velocity = movement; 
                currentAction = "Pushing";
         }

        }

        else{
             intWObj = false;
        }

        
        
        

    }

    // Allows the character to run (by using left shift)
    void Run(){
        Vector2 movement;
        movement.y = rigidBody.velocity.y;
        

        if (Input.GetKey(KeyCode.LeftShift) && currentAction != "Pushing"){
             if(Input.GetKey(KeyCode.RightArrow) == true){
                movement.x = CollisionWithSkateBoard();
                currentAction = "Running Foward";   
            }

            else if (Input.GetKey(KeyCode.LeftArrow) == true){
                movement.x = -CollisionWithSkateBoard();
                currentAction = "Running Foward";    
            
            }

            else{
                speed = 3.0f;
                movement.x = 0;    
            }

            rigidBody.velocity = movement; 
         
         }

         else if (Input.GetKeyUp(KeyCode.LeftShift)){
            speed = 3.0f;
            movement.x = 0;    
            rigidBody.velocity = movement; 
         }


    }

    // Restart the scene if Violet Drops
    void GameOver(){
        if (rigidBody.position.y < -3.0f){
            dead = true;
            reason = "pit";
        }
    }
 
    IEnumerator Dead()
     {
        StartCoroutine(transitionScript.FadeInBlackOutSquare());
        yield return new WaitForSeconds(1.72f);
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
     }

    IEnumerator GameOverScreen()
    // Transition Game Over screen for 5 seconds
    {;
        ReasonDying(reason);
        gameOverScreen.SetActive(true);
        yield return new WaitForSeconds(5.0f);
        gameOverScreen.SetActive(false);
    }

    void ReasonDying(string reason)
    // Changes reason for dying depending on event
    {   
        if (reason == "debris"){
            reason = "You hit debris.";
        }
        else if (reason == "pit"){
            reason = "You fell into a pit.";
        }
        else if (reason == "sanityDeath"){
            reason = "Your sanity reached 0.";
        }
        reasonForDying.GetComponent<TMP_Text>().text = reason;
    }


    float CollisionWithSkateBoard(){
        if (skateboarding){
            return 7.0f;
        }

        return 5.0f;
        

    }

    void PlaySound(){
        
        if (!isGrounded && !jump.isPlaying){
           StartCoroutine(PlaySounds());
        }

        else if( (currentAction == "Walking Foward" || currentAction == "Pushing" || currentAction == "Running Foward") && isGrounded && !walk.isPlaying ){
            
            walk.Play();
        }


        else{
            walk.Pause();
            jump.Pause();
        }

        onFloorLastFrame = isGrounded;
    }

    IEnumerator PlaySounds() {

        if(speed > 3.0f){
            yield return new WaitForSeconds(2.0f);
            jump.Play();
        }

        else{
            yield return new WaitForSeconds(1.4f);
            jump.Play();
        }
           
        }
    void stopAnimation(){
        // stops animation if pause menu is shown
        if (pauseMenu.animationBoo()){
            anim.gameObject.GetComponent<Animator>().enabled = false;
        }
        else{
            anim.gameObject.GetComponent<Animator>().enabled = true;
        }
    }
 
     

}

