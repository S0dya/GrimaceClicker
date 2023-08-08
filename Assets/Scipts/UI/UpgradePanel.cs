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

    //buttons
    public void onBuyButton(int i)
    {

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
            if (upgradeTabs[i].locked && Settings.scoreVal / 2 >= Settings.upgradeCost[i])
            {
                upgradeTabs[i].Unlock();
            }
            if (upgradeTabs[i].lockedPrice && Settings.scoreVal >= Settings.upgradeCost[i])
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
