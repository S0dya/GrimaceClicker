using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UpgradePanel : SingletonMonobehaviour<UpgradePanel>
{
    [SerializeField] InfoPanel infoPanel;
    [SerializeField] GameObject[] upgradeTabsObjs;
    [SerializeField] UpgradeTab[] upgradeTabs;
    int[] prices;

    protected override void Awake()
    {
        base.Awake();


    }

    void Start()
    {
        for (int i = 0; i < upgradeTabs.Length; i++)
        {
            upgradeTabs[i].UpdatePrice(Settings.upgradeCost[i]);
        }
    }

    //buttons
    public void onBuyButton(int i)
    {
        if (Settings.scoreVal < Settings.upgradeCost[i])
            return;

        Settings.scoreVal -= Settings.upgradeCost[i];
        Settings.upgradeCost[i] *= 1.15f;
        Settings.upgradeMultiplayer += Settings.upgradePerSecond[i];
        upgradeTabs[i].UpdatePrice(Settings.upgradeCost[i]);
    }

    public void OnInfoButton(int i)
    {
        infoPanel.Move(upgradeTabsObjs[i].transform.position);
        InfoPanel.I.SetInfo(i);

    }

    //otherMethods
    public void UpdateUpgrade()
    {
        for (int i = 0; i < upgradeTabs.Length; i++)
        {
            if (upgradeTabs[i].locked && Settings.scoreVal >= Settings.upgradeCost[i]/2)
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
}
