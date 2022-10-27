using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SanityScript : MonoBehaviour
{
    public float lostSanity = 0;
    public int maxSanity = 100;
    public Slider sanityMeter;

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
      }


}

