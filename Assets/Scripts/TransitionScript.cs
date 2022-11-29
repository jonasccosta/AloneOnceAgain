using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class TransitionScript : MonoBehaviour
{
    public Image blackOutObject;

    public void Update(){
    }

    public IEnumerator FadeInBlackOutSquare(float fadeSpeed = 0.5f){
    
        Color objectColor = blackOutObject.color;
        float fadeAmount;

        while(blackOutObject.color.a < 1){
            fadeAmount = objectColor.a + (fadeSpeed * Time.deltaTime);
            objectColor = new Color(objectColor.r, objectColor.g, objectColor.b, fadeAmount);
            blackOutObject.color = objectColor;
            yield return null;
        }
    }

    public IEnumerator FadeOutBlackSquare(float fadeSpeed = 0.5f){
        Color objectColor = blackOutObject.color;
        float fadeAmount;

        while(blackOutObject.color.a > 0){
            fadeAmount = objectColor.a - (fadeSpeed * Time.deltaTime);
            objectColor = new Color(objectColor.r, objectColor.g, objectColor.b, fadeAmount);
            blackOutObject.color = objectColor;
            yield return null;
        }
    }
}
