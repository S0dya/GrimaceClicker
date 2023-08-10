using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UpgradeTab : MonoBehaviour
{
    [SerializeField] GameObject lockedImage;
    [SerializeField] GameObject lockedPriceImage;

    [HideInInspector] public bool lockedPrice;
    [HideInInspector] public bool locked;

    [SerializeField] TextMeshProUGUI priceText;

    void Start()
    {
        locked = true;
    }

    public void BuyUpgrade()
    {

    }

    public void LockPrice()
    {
        if (lockedPrice)
            return;

        lockedPrice = true;
        lockedPriceImage.SetActive(true);
        priceText.color = new Color(255, 0, 0);
    }

    public void UnlockPrice()
    {
        if (!lockedPrice)
            return;

        lockedPrice = false;
        lockedPriceImage.SetActive(false);
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
