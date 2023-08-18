using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UpgradeTabPassive : MonoBehaviour
{

    [SerializeField] GameObject lockedImage;
    [SerializeField] GameObject boughtImage;

    [HideInInspector] public bool lockedPrice;
    [HideInInspector] public bool locked;
    [HideInInspector] public bool bought;

    [SerializeField] TextMeshProUGUI priceText;

    void Awake()
    {
        locked = true;

    }

    public void BuyUpgrade(int i)
    {
        Settings.totalAmountOfPassiveUpgrades++;
        SetBought();
        Settings.passiveUpsUnlocked[i] = true;
    }

    public void SetBought()
    {
        bought = true;
        boughtImage.SetActive(true);
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

    public void UpdatePrice(float val)
    {
        priceText.text = Settings.Format(val);
    }
}
