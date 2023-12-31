using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UpgradePanel : SingletonMonobehaviour<UpgradePanel>
{
    [SerializeField] GameObject activeUpgradeTab;
    [SerializeField] GameObject passiveUpgradeTab;
    [SerializeField] InfoPanel infoPanel;
    [SerializeField] InfoPanelPassive infoPanelPassive;
    [SerializeField] GameObject[] upgradeBarObjs;
    [SerializeField] UpgradeTab[] upgradeBars;
    [SerializeField] TextMeshProUGUI[] upgradeAmountTexts;

    [SerializeField] GameObject[] passiveUpgradeTabsObjs;
    [SerializeField] UpgradeTabPassive[] passiveUpgradeTabs;


    int[] prices;

    bool inActive;
    bool inPassive;

    int curBonusType;
    int curBonusIndex;
    
    protected override void Awake()
    {
        base.Awake();

    }

    void Start()
    {
        inActive = true;

        for (int i = 0; i < upgradeBars.Length; i++)
        {
            upgradeBars[i].UpdatePrice(Settings.upgradeCost[i]);

            if (Settings.openedActiveUps[i])
            {
                upgradeBars[i].Unlock();
            }
            
            UpdateUpgradeAmount(i);

        }

        for (int i = 0; i < passiveUpgradeTabs.Length; i++)
        {
            passiveUpgradeTabs[i].UpdatePrice(Settings.passiveUpgradeCost[i]);
            
            if (Settings.openedPassiveUps[i])
            {
                passiveUpgradeTabs[i].Unlock();
            }

            if (Settings.boughtPassiveUps[i])
            {
                passiveUpgradeTabs[i].SetBought();
            }
        }

        UpdateUpgrade();
    }

    //buttons
    public void onBuyButtonActive(int i)
    {
        if (Settings.scoreVal < Settings.upgradeCost[i])
            return;

        Settings.scoreVal -= Settings.upgradeCost[i];
        Settings.upgradeCost[i] *= 1.15f;
        float newUpMult = Settings.upgradePerSecond[i] * Settings.upgradesMultiplayers[i];
        float newUpMultPerSec = Settings.upgradeInfoPerSecond[i] * Settings.upgradesMultiplayers[i];
        if (Settings.isBonusOn)
        {
            newUpMult *= 2;
            newUpMultPerSec *= 2;
        }
        Settings.upgradeMultiplayer += newUpMult;
        Settings.upgradeMultiplayerPerSec += newUpMultPerSec;
        Settings.upgradeInfoAmount[i]++;
        UpdateUpgradeAmount(i);

        upgradeBars[i].UpdatePrice(Settings.upgradeCost[i]);
        upgradeBars[i].BuyUpgrade();
    }

    public void OnInfoButtonActive(int i)
    {
        infoPanel.SetInfo(i);
    }



    public void OnChangeBonusType(int i)
    {
        curBonusType = i;
    }
    public void OnChangeBonusIndex(int i)
    {
        curBonusIndex = i;
    }
    public void onBuyButtonPassive(int i)
    {
        if (Settings.scoreVal < Settings.passiveUpgradeCost[i])
            return;
        Settings.boughtPassiveUps[i] = true;

        Settings.scoreVal -= Settings.passiveUpgradeCost[i];
        if (curBonusType == 2)
        {
            Settings.passiveUpgradeCost[curBonusIndex] *= Settings.passiveUpgradeBonus[curBonusType];
        }
        else
        {
            Settings.upgradePerSecond[curBonusIndex] *= Settings.passiveUpgradeBonus[curBonusType];
        }
        passiveUpgradeTabs[i].BuyUpgrade(i);
    }

    public void onBuyButtonPassiveClick(int i)
    {
        if (Settings.scoreVal < Settings.passiveUpgradeCost[i])
            return;
        Settings.boughtPassiveUps[i] = true;

        Settings.scoreVal -= Settings.passiveUpgradeCost[i];
        Settings.clickMultiplayer *= Settings.passiveUpgradeBonus[curBonusType];
        passiveUpgradeTabs[i].BuyUpgrade(i);
    }
    public void OnInfoButtonPassive(int i)
    {
        infoPanelPassive.SetInfo(i, curBonusType);
    }



    public void OnActiveUpgradePanel()
    {
        if (inActive)
            return;

        inPassive = false;
        inActive = true;
        activeUpgradeTab.SetActive(true);
        passiveUpgradeTab.SetActive(false);
        InfoPanelPassive.I.ToggleInfo(false);
    }

    public void OnPassiveUpgradePanel()
    {
        if (inPassive)
            return;

        inPassive = true;
        inActive = false;
        activeUpgradeTab.SetActive(false);
        passiveUpgradeTab.SetActive(true);
        InfoPanel.I.ToggleInfo(false);
    }

    //otherMethods
    public void UpdateUpgrade()//---------------CHANGELATER
    {
        CheckActive();
        CheckPassive();
    }
    public void CheckActive()
    {
        for (int i = 0; i < upgradeBars.Length; i++)
        {
            if (upgradeBars[i].locked && Settings.scoreVal >= Settings.upgradeCost[i] / 2)
            {
                upgradeBars[i].Unlock();
                Settings.openedActiveUps[i] = true;
            }
            else if (Settings.scoreVal >= Settings.upgradeCost[i])
            {
                upgradeBars[i].UnlockPrice();
            }
            else
            {
                upgradeBars[i].LockPrice();
            }
        }
    }
    public void CheckPassive()
    {
        for (int i = 0; i < passiveUpgradeTabs.Length; i++)
        {
            if (passiveUpgradeTabs[i].bought)
            {
                continue;
            }

            if (passiveUpgradeTabs[i].locked && Settings.scoreVal >= Settings.passiveUpgradeCost[i] / 2)
            {
                passiveUpgradeTabs[i].Unlock();
                Settings.openedPassiveUps[i] = true;
            }
            else if (Settings.scoreVal >= Settings.passiveUpgradeCost[i])
            {
                passiveUpgradeTabs[i].UnlockPrice();
            }
            else
            {
                passiveUpgradeTabs[i].LockPrice();
            }
        }
    }

    void UpdateUpgradeAmount(int i)
    {
        if (Settings.upgradeInfoAmount[i] != 0)
        {
            upgradeAmountTexts[i].text = Settings.upgradeInfoAmount[i].ToString();
        }
        else
        {
            upgradeAmountTexts[i].text = "";
        }
    }

    public void CLear()
    {
        OnActiveUpgradePanel();

        for (int i = 0; i < upgradeBars.Length; i++)
        {
            Debug.Log(Settings.upgradeCost[i]);
            upgradeBars[i].UpdatePrice(Settings.upgradeCost[i]);
            upgradeBars[i].Lock();

            UpdateUpgradeAmount(i);
        }

        for (int i = 0; i < passiveUpgradeTabs.Length; i++)
        {
            passiveUpgradeTabs[i].UpdatePrice(Settings.passiveUpgradeCost[i]);
            passiveUpgradeTabs[i].Lock();
        }
    }
}
