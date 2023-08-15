using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using static LeanTween;

public class BonusButton : SingletonMonobehaviour<BonusButton>, IPointerDownHandler
{
    Image image;
    Button button;
    CanvasGroup canvasGroup;
    RectTransform rectTransform;

    Coroutine appearCor;

    float width = Screen.width/3;
    float height = Screen.height/3;

    bool ckicked;

    protected override void Awake()
    {
        base.Awake();

        image = GetComponent<Image>();
        button = GetComponent<Button>();
        canvasGroup = GetComponent<CanvasGroup>();
        rectTransform = GetComponent<RectTransform>();
    }

    void Start()
    {
        SetNextBonus();
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        ckicked = true;
        if (appearCor != null)
            StopCoroutine(appearCor);

        StopBlinkingBonus();

        GameManager.I.ToggleBonus(true);
    }

    public void ShowBonus()
    {
        rectTransform.anchoredPosition = new Vector2(Random.Range(-width, width), Random.Range(-height, height));
        transform.localScale = new Vector3(1, 1, 1);

        StartBlinkingBonus();
        appearCor = StartCoroutine(BonusAppearCor());
    }
    public void SetNextBonus()
    {
        int cur = Settings.totalClicks;
        Settings.nextBonus = Random.Range(cur + 100, cur + 250);
    }

    void StartBlinkingBonus()
    {
        LeanTween.value(gameObject, 0.1f, 1f, 0.2f).setLoopPingPong().setOnUpdate(UpdateAlphaBonus);
        image.enabled = true;
        button.enabled = true;
    }

    void UpdateAlphaBonus(float alpha)
    {
        canvasGroup.alpha = alpha;
    }
    
    void StopBlinkingBonus()
    {
        button.enabled = false;
        LeanTween.cancel(gameObject);
        canvasGroup.alpha = 1f;
        StartPopEffect();
    }


    void StartPopEffect()
    {
        LeanTween.scale(gameObject, Vector2.zero, 0.5f).setEaseOutBack().setOnComplete(OnPopComplete);
    }

    void OnPopComplete()
    {
        if (!ckicked)
        {
            SetNextBonus();
        }
        image.enabled = false;
        ckicked = false;
    }


    IEnumerator BonusAppearCor()
    {
        yield return new WaitForSeconds(Random.Range(1f, 3f));

        StopBlinkingBonus();
    }
}
