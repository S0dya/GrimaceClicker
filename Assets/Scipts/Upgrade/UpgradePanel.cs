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
    [SerializeField] GameObject[] upgradeTabsObjs;
    [SerializeField] UpgradeTab[] upgradeTabs;

    [SerializeField] GameObject[] passiveUpgradeTabsObjs;
    [SerializeField] UpgradeTabPassive[] passiveUpgradeTabs;

    int[] prices;

    bool inActive;
    bool inPassive;

    int curBonusIndex;

    protected override void Awake()
    {
        base.Awake();

    }

    void Start()
    {
        inActive = true;

        for (int i = 0; i < upgradeTabs.Length; i++)
        {
            upgradeTabs[i].UpdatePrice(Settings.upgradeCost[i]);
        }
        for (int i = 0; i < passiveUpgradeTabs.Length; i++)
        {
            passiveUpgradeTabs[i].UpdatePrice(Settings.passiveUpgradeCost[i]);
        }
    }

    //buttons
    public void onBuyButtonActive(int i)
    {
        if (Settings.scoreVal < Settings.upgradeCost[i])
            return;

        Settings.scoreVal -= Settings.upgradeCost[i];
        Settings.upgradeCost[i] *= 1.15f;
        Settings.upgradeMultiplayer += Settings.upgradePerSecond[i] * Settings.upgradesMultiplayers[i];
        upgradeTabs[i].UpdatePrice(Settings.upgradeCost[i]);
    }

    public void OnInfoButtonActive(int i)
    {
        infoPanel.Move(upgradeTabsObjs[i].transform.position);
        infoPanel.SetInfo(i);
    }



    public void OnChangeBonus(int i)
    {
        curBonusIndex = i;
    }
    public void onBuyButtonPassive(int i)
    {
        if (Settings.scoreVal < Settings.passiveUpgradeCost[i])
            return;

        Settings.scoreVal -= Settings.passiveUpgradeCost[i];
        Debug.Log(Settings.upgradePerSecond[i]);
        Settings.upgradePerSecond[i] *= Settings.passiveUpgradeBonus[curBonusIndex];
        Debug.Log(Settings.upgradePerSecond[i]);
        passiveUpgradeTabs[i].BuyUpgrade();
    }

    public void onBuyButtonPassiveClick(int i)
    {
        if (Settings.scoreVal < Settings.passiveUpgradeCost[i])
            return;

        Settings.scoreVal -= Settings.passiveUpgradeCost[i];
        Settings.clickMultiplayer *= Settings.passiveUpgradeBonus[curBonusIndex];
        passiveUpgradeTabs[i].BuyUpgrade();
    }

    

    public void OnInfoButtonPassive(int i)
    {
        infoPanelPassive.Move(passiveUpgradeTabsObjs[i].transform.position);
        infoPanelPassive.SetInfo(i);
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
        for (int i = 0; i < upgradeTabs.Length; i++)
        {
            if (upgradeTabs[i].locked && Settings.scoreVal >= Settings.upgradeCost[i] / 2)
            {
                upgradeTabs[i].Unlock();
            }
            else if (Settings.scoreVal >= Settings.upgradeCost[i])
            {
                upgradeTabs[i].UnlockPrice();
            }
            else
            {
                upgradeTabs[i].LockPrice();
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

}
