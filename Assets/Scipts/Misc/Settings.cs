using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public static class Settings
{
    //game
    public static float scoreVal;
    public static float clickMultiplayer = 1;
    public static float upgradeMultiplayer;
    public static float upgradeMultiplayerPerSec;

    public static bool isBonusOn;
    public static int nextBonus;

    //Upgrades/Info
    public static float[] upgradeCost= { 10, 100, 2, };
    public static float[] upgradePerSecond = { 0.013f, 1f, 2, };

    public static float[] upgradeInfoPerSecond = { 0.2f, 1f, 2, };
    public static float[] upgradeInfoAmount = { 0, 0, 0, };
    public static float[] upgradeInfoSoFar = { 0.2f, 1f, 2, };

    public static float[] upgradesMultiplayers = { 1, 1, 1, 1 };

    public static float[] passiveUpgradeCost = { 10, 50, 100, 200};
    public static float[] passiveUpgradeBonus = { 2, 1.2f, 0.8f };
    public static string[] bonusText = { "increases by ", "increases by", "decreases price by" };

    public static float[] passiveUpgradeInfoBonus = { 0.2f, 1f, 2, };

    //stats
    public static float totalAmount;
    public static int totalClicks;
    public static float totalAmountOfActiveUpgrades;
    public static float totalAmountOfPassiveUpgrades;
    public static float totalAmountOfWatchedAdx10;
    public static float totalAmountOfWatchedAdx2;

    public static bool[] passiveUpsUnlocked = new bool[6]; //passiveUpgradeCost.Length

    //settings
    public static bool isMusicOn;
    public static bool isParticlesOn;



    //save
    public static string GameScene = SceneManager.GetActiveScene().name;


    //format
    const float thousand = 1e3f;
    const float million = 1e6f;
    const float billion = 1e9f;
    const float trillion = 1e12f;
    const float quadrillion = 1e15f;
    const float quintillion = 1e18f;

    public static string Format(float number)
    {
        if (float.IsPositiveInfinity(number))
            return "MAXIMUM VALUE";

        string formattedNumber;

        switch (number)
        {
            case var _ when number >= quintillion:
                formattedNumber = (number / quintillion).ToString("F1") + "QT";
                break;
            case var _ when number >= quadrillion:
                formattedNumber = (number / quadrillion).ToString("F1") + "QD";
                break;
            case var _ when number >= trillion:
                formattedNumber = (number / trillion).ToString("F1") + "T";
                break;
            case var _ when number >= billion:
                formattedNumber = (number / billion).ToString("F1") + "B";
                break;
            case var _ when number >= million:
                formattedNumber = (number / million).ToString("F1") + "M";
                break;
            case var _ when number >= thousand:
                formattedNumber = (number / thousand).ToString("F1") + "K";
                break;
            default:
                formattedNumber = number.ToString("F0");
                break;
        }

        return formattedNumber;
    }

    public static string FormatForMultiplayer(float number)
    {
        if (float.IsPositiveInfinity(number))
            return "MAXIMUM VALUE";

        string formattedNumber;

        switch (number)
        {
            case var _ when number >= quintillion:
                formattedNumber = (number / quintillion).ToString("F1") + "QT";
                break;
            case var _ when number >= quadrillion:
                formattedNumber = (number / quadrillion).ToString("F1") + "QD";
                break;
            case var _ when number >= trillion:
                formattedNumber = (number / trillion).ToString("F1") + "T";
                break;
            case var _ when number >= billion:
                formattedNumber = (number / billion).ToString("F1") + "B";
                break;
            case var _ when number >= million:
                formattedNumber = (number / million).ToString("F1") + "M";
                break;
            case var _ when number >= thousand:
                formattedNumber = (number / thousand).ToString("F1") + "K";
                break;
            default:
                formattedNumber = number.ToString("F1");
                break;
        }

        return formattedNumber;
    }
}
