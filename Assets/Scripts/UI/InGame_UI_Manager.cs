using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class InGame_UI_Manager : MonoBehaviour
{

    [Header("Secrets")] //hidden from player's view unless the criteria is met
    [SerializeField] private GameObject pausePanel;
    [SerializeField] private GameObject settingsPanel;

    private bool isPaused = false;
    private bool openSettings = false;

    
    void Start()
    {
        pausePanel.SetActive(false);
        settingsPanel.SetActive(false);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            TogglePause();
        }
    }

    public void TogglePause() // written by chat gpt to make it a lil cleaner ig, since im not the best at programming, lol
    {
        isPaused = !isPaused; // Switch the state (paused / not paused)

        if (isPaused)
        {
            Time.timeScale = 0;
            pausePanel.SetActive(true);
            
        }
        else
        {
            Time.timeScale = 1;
            pausePanel.SetActive(false );
        }
    }

    public void ToggleSettings()
    { 
        openSettings = !openSettings; 

        if(openSettings)
        {
            settingsPanel.SetActive(true);
        }
        else
        {
            settingsPanel.SetActive(false);
        }
    
    }

    public void OnApplicationQuit()
    {
        Application.Quit();
        Debug.Log("Exiting the game.");
    }

    public void ReturnToTitleScreen()
    {
        SceneManager.LoadScene("TitleScreen");
    }
}
