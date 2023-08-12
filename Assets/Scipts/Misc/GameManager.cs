using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GameManager : SingletonMonobehaviour<GameManager>
{

    [SerializeField] TextMeshProUGUI scoreText;

    [SerializeField] GameObject updgradeTab;
    [SerializeField] GameObject statsTab;
    [SerializeField] GameObject settingsTab;

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
        Time.timeScale = 0.25f;
    }

    void FixedUpdate()
    {
        Debug.Log(Settings.upgradeMultiplayer);
        Settings.scoreVal += Settings.upgradeMultiplayer;
        Settings.totalAmount += Settings.upgradeMultiplayer;
        UpdateScore();
    }

    //buttons
    public void onMainButtonPress()
    {
        Settings.scoreVal += Settings.clickMultiplayer;
        Settings.totalAmount += Settings.clickMultiplayer;
        Settings.totalClicks++;
        UpdateScore();
    }

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


    //gameMethods
    void UpdateScore()
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


    //gameManagerMethods

    private void OnApplicationPause(bool pauseStatus)
    {
        if (pauseStatus)
        {
            //im.sprite = sp;
        }
    }
}
