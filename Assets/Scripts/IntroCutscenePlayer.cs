using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class IntroCutscenePlayer : MonoBehaviour
{
    void Start()
    {
        GameObject camera = GameObject.Find("Main Camera");
        var videoPlayer = camera.GetComponent<UnityEngine.Video.VideoPlayer>();
    }

    void StartGame(){
        SceneManager.LoadScene("MainScene");
    }
}
