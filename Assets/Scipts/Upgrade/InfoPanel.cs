using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class InfoPanel : SingletonMonobehaviour<InfoPanel>
{
    [SerializeField] TextMeshProUGUI perSecondAmount;
    [SerializeField] TextMeshProUGUI amount;
    [SerializeField] TextMeshProUGUI madeSoFar;

    int curI = -1;

    protected override void Awake()
    {
        base.Awake();

    }

    void Start()
    {
        ToggleInfo(false);
    }

    //button
    public void OnCloseButton()
    {
        ToggleInfo(false);
    }
    
    //otherMethods
    public void SetInfo(int i)
    {
        if (curI == i)
        {
            ToggleInfo(false);
            return;
        }
        curI = i;
        perSecondAmount.text = Settings.upgradeInfoPerSecond[i].ToString();
        amount.text = Settings.upgradeInfoAmount[i].ToString();
        madeSoFar.text = Settings.upgradeInfoSoFar[i].ToString();

        ToggleInfo(true);
    }

    public void ToggleInfo(bool val)
    {
        if (!val)
        {
            curI = -1;
        }
        gameObject.SetActive(val);
    }
}
