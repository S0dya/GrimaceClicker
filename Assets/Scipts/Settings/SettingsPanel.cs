using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SettingsPanel : SingletonMonobehaviour<SettingsPanel>
{
    [SerializeField] Image[] settingsButtonImages;
    [SerializeField] TextMeshProUGUI[] settingsTexts;


    protected override void Awake()
    {
        base.Awake();

        Toggle(Settings.isMusicOn, 0);
        Toggle(Settings.isParticlesOn, 1);
    }

    //buttons
    public void OnSoundButton()
    {
        Toggle(!Settings.isMusicOn, 0);
    }

    public void OnParticlesButton()
    {
        Toggle(!Settings.isParticlesOn, 1);
    }

    public void OnNewGameButton()
    {
        GameManager.I.ToggleNewGame(true);
    }

    //methods
    public void Toggle(bool val, int i)
    {
        Debug.Log(i);
        switch(i)
        {
            case 0:
                Settings.isMusicOn = val;//maybeDelLater
                break;
            case 1:
                Settings.isParticlesOn = val;//maybeDelLater
                break;
            default:
                Debug.Log("setPan");
                break;

        }

        ToggleSetting(val, settingsButtonImages[i], settingsTexts[i]);
    }

    void ToggleSetting(bool val, Image buttonImg, TextMeshProUGUI text)
    {
        if (val)
        {
            buttonImg.color = new Color(0, 155, 0);
            text.text = "ON";
        }
        else
        {
            buttonImg.color = new Color(155, 0, 0);
            text.text = "OFF";
        }
    }
}
