using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class StatsPanel : SingletonMonobehaviour<StatsPanel>
{
    [SerializeField] TextMeshProUGUI totalAmountText;
    [SerializeField] TextMeshProUGUI totalClicksText;
    [SerializeField] TextMeshProUGUI totalAmountOfActiveUpgrades;
    [SerializeField] TextMeshProUGUI totalAmountOfPassiveUpgrades;
    [SerializeField] TextMeshProUGUI amountOnOneClickText;

    [SerializeField] GameObject passiveUpsTab;

    [SerializeField] GameObject[] passiveUpsObj;

    int curBonusType;

    protected override void Awake()
    {
        base.Awake();

    }

    void Start()
    {

        UpdateStats();
    }

    //buttons
    public void OnAdButtonX10Score()
    {
        AdsManager.I.x10 = true;
        AdsManager.I.x2 = false;

        AdsManager.I.ShowRewardedAd();
    }
    public void OnAdButtonX2Upgrades()
    {
        AdsManager.I.x10 = false;
        AdsManager.I.x2 = true;

        AdsManager.I.ShowRewardedAd();
    }

    public void OnPassiveUpgradesShowButton()
    {
        TogglePassiveTab(!passiveUpsTab.activeSelf);
    }


    public void OnChangePassiveUpgradeType(int i)
    {
        curBonusType = i;
    }
    public void OnPassiveUpgradeButton(int i)
    {
        InfoPanelStats.I.SetInfo(i, curBonusType, passiveUpsObj[i].transform.position);
    }

    //methods
    public void UpdateStats()
    {
        totalAmountText.text = Settings.Format(Settings.totalAmount);
        totalClicksText.text = Settings.Format(Settings.totalClicks);
        totalAmountOfActiveUpgrades.text = Settings.Format(Settings.totalAmountOfActiveUpgrades);
        totalAmountOfPassiveUpgrades.text = Settings.Format(Settings.totalAmountOfPassiveUpgrades);
        amountOnOneClickText.text = Settings.Format(Settings.clickMultiplayer);
    }

    public void UpdatePassiveUps()
    {
        for (int i = 0; i < passiveUpsObj.Length; i++)
        {
            passiveUpsObj[i].SetActive(Settings.passiveUpsUnlocked[i]);
        }
    }

    public void TogglePassiveTab(bool val)
    {
        if (val)
        {
            passiveUpsTab.SetActive(val);
            InfoPanelStats.I.ToggleInfo(false);
        }
        else
        {
            InfoPanelStats.I.ToggleInfo(false);
            passiveUpsTab.SetActive(val);
        }
    }

    //ads
    public void RewardPlayerX10()
    {
        float cur = Settings.scoreVal * 10f;
        Settings.scoreVal = cur;
        Settings.totalAmount += cur;
    }

    public void RewardPlayerX2()
    {
        Settings.upgradeMultiplayer *= 2;
        Settings.upgradeMultiplayerPerSec *= 2;
        GameManager.I.UpdateMultiplayer();
    }

}
