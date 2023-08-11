using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class InfoPanelPassive : SingletonMonobehaviour<InfoPanelPassive>
{
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

    public void Move(Vector3 refer)
    {
        // Get the current position of the reference panel
        Vector3 referencePosition = refer;

        // Calculate the new position for the panel to move
        Vector3 newPosition = new Vector3(referencePosition.x, referencePosition.y, referencePosition.z);

        // Set the new position for the panel to move
        transform.position = newPosition;
    }

    public void SetInfo(int i)
    {
        if (curI == i)
        {
            ToggleInfo(false);
            return;
        }
        curI = i;
        bonus.text = Settings.upgradeInfoPerSecond[i].ToString();

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
