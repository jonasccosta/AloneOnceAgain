using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkateboardScript : MonoBehaviour
{
    private AudioSource skateboardAudio;
    public Rigidbody2D rigidBody;
    // Start is called before the first frame update
    void Start()
    {
     skateboardAudio = GetComponent<AudioSource>();     
    }

    // Update is called once per frame
    void Update()
    {
        if (rigidBody.velocity.magnitude >= 0.1 && !skateboardAudio.isPlaying ){
            skateboardAudio.Play();

         }

         else if (rigidBody.velocity.magnitude < 0.1){
            skateboardAudio.Pause();
         }

        
    }
}
