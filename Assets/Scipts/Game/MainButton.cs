using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;
using static LeanTween;

public class MainButton : SingletonMonobehaviour<MainButton>, IPointerDownHandler
{
    [SerializeField] Transform clickEffectParent;
    [SerializeField] GameObject onClickEffectPrefab;
    [SerializeField] Sprite[] images;

    protected override void Awake()
    {
        base.Awake();
       
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        Settings.scoreVal += Settings.clickMultiplayer;
        Settings.totalAmount += Settings.clickMultiplayer;
        Settings.totalClicks++;
        GameManager.I.UpdateScore();

        if (Settings.totalClicks == Settings.nextBonus)
        {
            BonusButton.I.ShowBonus();
        }

        if (!Settings.isParticlesOn)
            return;

        Vector2 randomVec = new Vector2(Random.Range(-40f, 40f), Random.Range(-40f, 40f));
        GameObject effectObj = Instantiate(onClickEffectPrefab, eventData.position + randomVec, Quaternion.identity, clickEffectParent);

        TextMeshProUGUI text = effectObj.GetComponent<TextMeshProUGUI>();
        Image image = effectObj.GetComponentInChildren<Image>();
        image.sprite = images[Random.Range(0, images.Length)];
        CanvasGroup canvasGroup = effectObj.GetComponent<CanvasGroup>();
        text.text = Settings.Format(Settings.clickMultiplayer);
        if (Settings.isBonusOn)
        {
            text.color = GameManager.I.bonusColor;
        }

        LeanTween.moveLocalY(effectObj, effectObj.transform.position.y, Random.Range(0.5f,0.8f)).setEaseOutQuad();
        LeanTween.alphaCanvas(canvasGroup, 0f, 0.35f).setEaseOutQuad().setOnComplete(() => Destroy(effectObj));
    }
}
