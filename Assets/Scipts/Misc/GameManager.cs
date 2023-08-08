using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GameManager : SingletonMonobehaviour<GameManager>
{
    [SerializeField] TextMeshProUGUI scoreText;
    float scoreVal;
    float curMultiplayer = 1;

    protected override void Awake()
    {
        base.Awake();

    }

    void Start()
    {
        scoreVal = 0;
    }

    //buttons
    public void onMainButtonPress()
    {
        scoreVal += curMultiplayer;
        UpdateScore();
    }


    //gameMethods
    void UpdateScore()
    {
        scoreText.text = scoreVal.ToString();
    }
}
