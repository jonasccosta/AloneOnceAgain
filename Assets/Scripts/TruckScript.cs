using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TruckScript : MonoBehaviour
{

    public AudioSource truckAudio;
    public Rigidbody2D rigidBody;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

         if (rigidBody.velocity.magnitude >= 0.1 && !truckAudio.isPlaying ){
            truckAudio.Play();

         }

         else if (rigidBody.velocity.magnitude < 0.1){
            truckAudio.Pause();
         }
        
    }
}
