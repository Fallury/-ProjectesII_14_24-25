using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    [SerializeField] private GameObject pauseButton;

    [SerializeField] private GameObject pauseMenu;

    private bool gamePause;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape)) 
        {
            if (gamePause)
            {

                ResumeButton();

            }
            else
            {

                Pause();
            }
        
        
        }
    }
    public void Pause(){

        gamePause = true;
        Time.timeScale = 0f;
        pauseButton.SetActive(false);
        pauseMenu.SetActive(true);
    }

    public void ResumeButton()
    {
        gamePause = false;
        Time.timeScale = 1f;
        pauseButton.SetActive(true);
        pauseMenu.SetActive(false);

    }
    public void MenuButton()
    {
        gamePause = false;
        Time.timeScale = 1f;
        SceneManager.LoadScene("MainMenu");

    }

    public void VolumeButton()
    {
       

    }










}
