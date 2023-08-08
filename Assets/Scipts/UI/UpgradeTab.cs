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


    public void BuyUpgrade()
    {

    }

    public void LockPrice()
    {
        lockedPriceImage.SetActive(true);
        priceText.color = new Color(255, 0, 0);
    }

    public void UnlockPrice()
    {
        lockedPriceImage.SetActive(false);
        priceText.color = new Color(0, 255, 0);
    }

    public void Unlock()
    {
        lockedImage.SetActive(false);
    }
}
