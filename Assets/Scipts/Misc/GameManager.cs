using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GameManager : SingletonMonobehaviour<GameManager>
{

    [SerializeField] TextMeshProUGUI scoreText;
    [SerializeField] TextMeshProUGUI multiplayerText;

    [SerializeField] GameObject updgradeTab;
    [SerializeField] GameObject statsTab;
    [SerializeField] GameObject settingsTab;

    [SerializeField] GameObject newGameTab;
    

    bool inUpgrade;
    bool inPassiveUpgrade;
    bool inStats;
    bool inSettings;

    protected override void Awake()
    {
        base.Awake();

    }

    void Start()
    {
        Settings.scoreVal = 0;
        ToggleNewGame(false);
    }

    void FixedUpdate()
    {
        Debug.Log(Settings.upgradeMultiplayer);
        Settings.scoreVal += Settings.upgradeMultiplayer;
        Settings.totalAmount += Settings.upgradeMultiplayer;
        UpdateScore();
    }

    //buttons
    public void onGameTabButton()
    {
        ClosePrevTabs();
    }

    public void onUpgradeTabButton()
    {
        UpgradePanel.I.UpdateUpgrade();
        ClosePrevTabs();
        inUpgrade = true;
        updgradeTab.SetActive(true);
    }

    public void onStatsTabButton()
    {
        ClosePrevTabs();
        StatsPanel.I.UpdatePassiveUps();
        inStats = true;
        statsTab.SetActive(true);
    }

    public void onSettingsTabButton()
    {
        ClosePrevTabs();
        inSettings = true;
        settingsTab.SetActive(true);
    }

    //newGameButtons
    public void OnNewGameConfirmButton()
    {
        Debug.Log("load scene");
    }
    public void OnNewGameCloseButton()
    {
        ToggleNewGame(false);
    }


    //gameMethods
    public void UpdateScore()
    {
        scoreText.text = Settings.Format(Settings.scoreVal);

        if (inUpgrade)
        {
            UpgradePanel.I.UpdateUpgrade();
        }
        else if (inStats)
        {
            StatsPanel.I.UpdateStats();
        }
    }
    public void UpdateMultiplayer()
    {
        multiplayerText.text = Settings.FormatForMultiplayer(Settings.upgradeMultiplayerPerSec) + "/s";
    }

    void ClosePrevTabs()
    {
        //Debug.Log(inUpgrade + " " +inStats + " " + inSettings);
        if (inUpgrade)
        {
            updgradeTab.SetActive(false);
            inUpgrade = false;
            InfoPanel.I.ToggleInfo(false);
            InfoPanelPassive.I.ToggleInfo(false);
        }
        else if (inStats)
        {
            statsTab.SetActive(false);
            inStats = false;
            StatsPanel.I.TogglePassiveTab(false);
        }
        else
        {
            settingsTab.SetActive(false);
            inSettings = false;
        }
    }

    //newGameMethods
    public void ToggleNewGame(bool val)
    {
        newGameTab.SetActive(val);
        if (val)
        {
            Time.timeScale = 0;
        }
        else
        {
            Time.timeScale = 0.25f;
        }
    }


    //gameManagerMethods

    private void OnApplicationPause(bool pauseStatus)
    {
        if (pauseStatus)
        {
            //im.sprite = sp;
        }
    }
}
