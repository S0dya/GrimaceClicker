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
        Vector3 newPosition = new Vector3(referencePosition.x, referencePosition.y + 200, referencePosition.z);

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
