using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SanityScript : MonoBehaviour
{
    public float lostSanity = 0.0f;
    public float maxSanity = 60.0f;
    public Slider sanityMeter;
    public bool dead = false;

    IEnumerator Start() 
      {
          while(true) 
          {
              yield return new WaitForSeconds(1.5f);
              UpdateSanity();
          }
      }

      void UpdateSanity() 
      {
        lostSanity += 0.5f;
        sanityMeter.value = lostSanity;
        if(lostSanity >= sanityMeter.maxValue){
          dead = true;
        }
      }


}

