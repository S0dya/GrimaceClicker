using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UpgradeTab : MonoBehaviour
{
    [SerializeField] GameObject lockedImage;

    [HideInInspector] public bool lockedPrice;
    [HideInInspector] public bool locked;

    [SerializeField] TextMeshProUGUI priceText;

    void Awake()
    {
        locked = true;
    }

    public void BuyUpgrade()
    {
        Settings.totalAmountOfActiveUpgrades++;
        GameManager.I.UpdateMultiplayer();
    }

    public void LockPrice()
    {
        if (lockedPrice)
            return;

        lockedPrice = true;
        priceText.color = new Color(255, 0, 0);
    }

    public void UnlockPrice()
    {
        if (!lockedPrice)
            return;

        lockedPrice = false;
        priceText.color = new Color(0, 255, 0);
    }

    public void Unlock()
    {
        locked = false;
        lockedImage.SetActive(false);
    }

    public void Lock()
    {
        locked = true;
        lockedImage.SetActive(true);
    }

    public void UpdatePrice(float val)
    {
        priceText.text = Settings.Format(val);
    }
}
