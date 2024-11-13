using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InGame_UI_Manager : MonoBehaviour
{

    [Header("Secrets")] //hidden from player's view unless the criteria is met
    [SerializeField] private GameObject pausePanel;

    
    void Start()
    {
        pausePanel.SetActive(false);
    }

   public void PauseGame()
    {


    }
}
