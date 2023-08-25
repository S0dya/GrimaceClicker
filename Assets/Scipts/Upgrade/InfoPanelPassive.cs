using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class InfoPanelPassive : SingletonMonobehaviour<InfoPanelPassive>
{
    [SerializeField] TextMeshProUGUI bonusType;

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
    public void SetInfo(int i, int type)
    {
        if (curI == i)
        {
            ToggleInfo(false);
            return;
        }
        curI = i;
        bonusType.text = Settings.bonusText[type];

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
