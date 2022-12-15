using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChanger : MonoBehaviour
{
    public float changeTime;
    public string sceneName;

    void Update()
    {
        changeTime -= Time.deltaTime;
        if(changeTime <= 0){
            if(sceneName == "Quit"){
                Application.Quit();
            }
            else{
                SceneManager.LoadScene(sceneName);
            }
        }
    }
}
