using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GameManager : SingletonMonobehaviour<GameManager>
{

    [SerializeField] TextMeshProUGUI scoreText;

    [SerializeField] GameObject updgradePanel;
    [SerializeField] GameObject statsPanel;
    [SerializeField] GameObject settingsPanel;

    bool inUpgrade;
    bool inStats;
    bool inSettings;

    protected override void Awake()
    {
        base.Awake();

    }

    void Start()
    {
        Settings.scoreVal = 0;
    }

    //buttons
    public void onMainButtonPress()
    {
        Settings.scoreVal += Settings.curMultiplayer;
        UpdateScore();
    }

    public void onUpdateTabButton()
    {
        ClosePrevTabs();
        inUpgrade = true;
        updgradePanel.SetActive(true);
    }

    public void onStatsTabButton()
    {
        ClosePrevTabs();
        inStats = true;
        statsPanel.SetActive(true);
    }

    public void onSettingsTabButton()
    {
        ClosePrevTabs();
        inSettings = true;
        settingsPanel.SetActive(true);
    }


    //gameMethods
    void UpdateScore()
    {
        scoreText.text = Settings.scoreVal.ToString();

        if (inUpgrade)
        {
            UpgradePanel.I.UpdateUpgrade();
        }
    }

    void ClosePrevTabs()
    {
        if (inUpgrade)
        {
            updgradePanel.SetActive(false);
        }
        else if (inStats)
        {
            statsPanel.SetActive(false);
        }
        else
        {
            settingsPanel.SetActive(false);
        }
    }

}
