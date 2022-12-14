using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public GameObject pauseMenu;
    public bool isPaused;

    // Start is called before the first frame update
    void Start()
    {
        pauseMenu.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            if (isPaused)
            {
                ResumeGame();
            }
            else
            {
                PauseGame();
            }
        }
    }

    public void PauseGame()
    // pause game
    {
        pauseMenu.SetActive(true);
        Time.timeScale = 0f;
        isPaused = true;
    } 

    public void ResumeGame()
    // resuemes games
    {
        pauseMenu.SetActive(false);
        Time.timeScale = 1f;
        isPaused = false;
    }

    public void GoToMainMenu()
    // takes user to the game title
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("GameTitle");
    }

    public void QuitGame()
    //Quits the game. It only works outside of unity enviroment
    {
        Application.Quit();
    }

    public bool animationBoo()
    // returns the isPaused menu boolean
    {
        return isPaused;
    }

}
