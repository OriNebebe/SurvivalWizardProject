using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UI_Manager : MonoBehaviour
{

    [Header("Secrets")] //hidden from player's view unless the criteria is met
    [SerializeField] private Button continueButton;
    [SerializeField] private GameObject settingsPanel;
    [SerializeField] private GameObject creditsPanel;

    private bool isSettingsOpen = false;
    private bool isCreditsOpen = false;
    public bool hasData = false; //will be used for enabling or disabling the 'continue button'

    private void Start()
    {
        creditsPanel.SetActive(false);
        settingsPanel.SetActive(false);
    }

    public void OnApplicationQuit()
    {
        Application.Quit();
        Debug.Log("Exiting the game.");
    }

    public void StartNewGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex +1);
        Debug.Log("Loading Scene 1");
    }

    public void SettingsOpen()
    {
        if (isSettingsOpen == false) 
        {
            settingsPanel.SetActive(true);
            isSettingsOpen = true;
        }

    }

    public void SettingsClose()
    {
        if (isSettingsOpen == true)
        {
            settingsPanel.SetActive(false);
            isSettingsOpen = false;
        }
    }

    public void CreditsOpen()
    {
        if (isCreditsOpen == false) 
        { 
            creditsPanel.SetActive(true);
            isCreditsOpen = true;
        }
    }

    public void CreditsClose()
    {
        if (isCreditsOpen == true)
        {
            creditsPanel.SetActive(false);
            isCreditsOpen = false;
        }
    }
}
