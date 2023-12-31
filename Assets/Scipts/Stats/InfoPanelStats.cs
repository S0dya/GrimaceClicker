using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class InfoPanelStats : SingletonMonobehaviour<InfoPanelStats>
{
    [SerializeField] GameObject infoTab;
    [SerializeField] GameObject highlight;
    [SerializeField] TextMeshProUGUI bonusType;
    [SerializeField] TextMeshProUGUI bonus;


    int curI;

    protected override void Awake()
    {
        base.Awake();


    }

    void Start()
    {
        curI = -1;
    }

    public void SetInfo(int i, int type, Vector2 pos)
    {
        if (curI == i)
        {
            ToggleInfo(false);
            return;
        }
        curI = i;
        bonusType.text = Settings.bonusText[type];
        bonus.text = Settings.passiveUpgradeBonus[type].ToString();

        highlight.transform.position = pos;

        ToggleInfo(true);
    }

    public void ToggleInfo(bool val)
    {
        if (!val)
        {
            curI = -1;
        }
        infoTab.SetActive(val);
        highlight.SetActive(val);
    }
}
