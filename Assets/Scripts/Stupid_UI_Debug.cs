using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Stupid_UI_Debug : MonoBehaviour
{

    [Header("Pages Panels")]
    [SerializeField] GameObject RaportPanel;
    [SerializeField] GameObject ResourcesPanel;
    [SerializeField] GameObject TeamPanel;
    [SerializeField] GameObject EventsPanel;
    [SerializeField] GameObject MapPanel;
    [SerializeField] GameObject UpgradesPanel;
    [SerializeField] GameObject SettingsPanel;

    private bool isRaportOpen = true;
    private bool isResourcesOpen = false;
    private bool isTeamOpen = false;
    private bool isEventsOpen = false;
    private bool isMapOpen = false;
    private bool isUpgradesOpen = false;
    private bool isSettingsOpen = false;


    private void Start()
    {
        
    }

    public void BackToTitleScreen()
    {
        SceneManager.LoadScene((0));
        Debug.Log("Loading Title Screen");
    }

    public void RaportPanelOpen()
    {
        if (isRaportOpen == false)
        {
            RaportPanel.SetActive(true);
            isRaportOpen = true;
        }

    }

    public void ResourcePanelOpen()
    {
        if (isResourcesOpen == false)
        {
            ResourcesPanel.SetActive(true);
            isResourcesOpen = true;
        }
    }

    public void TeamPanelOpen() 
    {
        if (isTeamOpen == false) 
        { 
            TeamPanel.SetActive(true);
            isTeamOpen = true;

        }
    }

    public void EventsPanelOpen()
    {
        if(isEventsOpen == false)
        {
            EventsPanel.SetActive(true);
            isEventsOpen = true;
        }
    }

    public void MapPanelOpen() 
    {
        if (isMapOpen == false)
        {
            MapPanel.SetActive(true); 
            isMapOpen = true;
        }
    }

    public void UpgradesPanelOpen()
    {
        if (isUpgradesOpen == false)
        {
            UpgradesPanel.SetActive(true);
            isUpgradesOpen = true;
        }
    }

    public void SettingsPanelOpen()
    {
        if (isSettingsOpen == false)
        {
            SettingsPanel.SetActive(true);
            isSettingsOpen = true;
        }
    }

    private void ClosingPanels()
    {
        if (isRaportOpen == true)
        {
            isMapOpen = false;
            isEventsOpen= false;
            isResourcesOpen= false;
            isSettingsOpen= false;
            isTeamOpen= false;
            isUpgradesOpen= false;
        }
    }
}
