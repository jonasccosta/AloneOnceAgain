using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterScript : MonoBehaviour
{
    public float speed = 5.0f;
    Rigidbody2D rigidBody;

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
        else{
            movement.x = 0;
        }
        rigidBody.velocity = movement;

        if (Input.GetKeyDown(KeyCode.UpArrow)){
            rigidBody.AddForce(new Vector2(0, speed), ForceMode2D.Impulse);
        }
        
    }
}
