using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SanityScript : MonoBehaviour
{
    public float lostSanity = 0.0f;
    public float maxSanity = 120.0f;
    public Slider sanityMeter;
    public bool dead = false;
    public Image sanityImage;
    private Color objectColor;

    private float hue, saturation, value;

    IEnumerator Start() 
      {
          objectColor = sanityImage.color;
          Color.RGBToHSV(objectColor, out hue, out saturation, out value);
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

        // Change hue of the sanity object
        objectColor = Color.HSVToRGB(hue*(1-(lostSanity)/sanityMeter.maxValue), saturation, value);
        sanityImage.color = objectColor;

        // If sanity is greater than or equal to the maximum value of the slider, then game over
        if(lostSanity >= sanityMeter.maxValue){
          dead = true;
        }
      }


}

