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
    public static float[] upgradeCost = { 15f, 100f, 500f, 2000f, 7500f, 50000f, 1000000f, 5000000f, 50000000f, 1000000000f, 50000000000f, 1000000000000f, 100000000000000f };

    public static float[] upgradePerSecond = { 0.2f / 15f, 1f / 15f, 8f / 15f, 47f / 15f, 260f / 15f, 1400f / 15f, 7800f / 15f, 44000f / 15f, 260000f / 15f, 1600000f / 15f, 10000000f / 15f, 65000000f / 15f, 430000000f / 15f };
    public static float[] upgradeInfoPerSecond = { 0.2f, 1f, 8f, 47f, 260f, 1400f, 7800f, 44000f, 260000f, 1600000f, 10000000f, 65000000f, 430000000f};
    public static float[] upgradeInfoAmount = { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 };
    public static float[] upgradeInfoSoFar = { 0.2f, 1f, 8f, 47f, 260f, 1400f, 7800f, 44000f, 260000f, 1600000f, 10000000f, 65000000f, 430000000f };

    public static float[] upgradesMultiplayers = { 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1 };

    public static float[] passiveUpgradeCost = { 150f, 1000f, 5000f, 10000, 25000f, 50000f, 75000f, 100000f, 250000f, 500000f, 750000f, 1000000f, 5000000f, 10000000f, 50000000f, 100000000f, 500000000f, 10000000000f, 500000000000f, 1000000000000f, 100000000000000f };
    public static float[] passiveUpgradeBonus = { 2, 1.2f, 0.8f };
    public static string[] bonusText = { "doubles click efficiency", "increases the effience by 20%", "decreases the price by 20%" };

    //stats
    public static float totalAmount;
    public static int totalClicks;
    public static float totalAmountOfActiveUpgrades;
    public static float totalAmountOfPassiveUpgrades;
    public static float totalAmountOfWatchedAdx10;
    public static float totalAmountOfWatchedAdx2;

    public static bool[] passiveUpsUnlocked = new bool[21]; //passiveUpgradeCost.Length

    //settings
    public static bool isMusicOn = true;
    public static bool isParticlesOn = true;



    //save
    public static string GameScene = "MainScene";
    public static bool[] openedActiveUps = new bool[15];//changelater
    public static bool[] openedPassiveUps = new bool[21];//changelater
    public static bool[] boughtPassiveUps = new bool[21];//changelater
    public static long lastExitTime;


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

    public static void Clear()
    {
        GameManager.I.ToggleBonus(false);

        scoreVal = 0;
        clickMultiplayer = 1;
        upgradeMultiplayer = 0;
        upgradeMultiplayerPerSec = 0;

        upgradeCost = new float[] { 15f, 100f, 500f, 2000f, 7500f, 50000f, 1000000f, 5000000f, 50000000f, 1000000000f, 50000000000f, 1000000000000f, 100000000000000f };
        upgradePerSecond = new float[] { 0.2f / 15f, 1f / 15f, 8f / 15f, 47f / 15f, 260f / 15f, 1400f / 15f, 7800f / 15f, 44000f / 15f, 260000f / 15f, 1600000f / 15f, 10000000f / 15f, 65000000f / 15f, 430000000f / 15f };
        upgradeInfoAmount = new float[] { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 };
        upgradeInfoSoFar = new float[] { 0.2f, 1f, 8f, 47f, 260f, 1400f, 7800f, 44000f, 260000f, 1600000f, 10000000f, 65000000f, 430000000f };
        upgradesMultiplayers = new float[] { 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1 };
        passiveUpgradeCost = new float[] { 150f, 1000f, 5000f, 10000, 25000f, 50000f, 75000f, 100000f, 250000f, 500000f, 750000f, 1000000f, 5000000f, 10000000f, 50000000f, 100000000f, 500000000f, 10000000000f, 500000000000f, 1000000000000f, 100000000000000f };

        totalAmount = 0;
        totalClicks = 0;
        totalAmountOfActiveUpgrades = 0;
        totalAmountOfPassiveUpgrades = 0;
        totalAmountOfWatchedAdx10 = 0;
        totalAmountOfWatchedAdx2 = 0;

        passiveUpsUnlocked = new bool[21]; //passiveUpgradeCost.Length
        openedActiveUps = new bool[15];//changelater
        openedPassiveUps = new bool[21];//changelater
        boughtPassiveUps = new bool[21];//changelater

        isMusicOn = true;
        isParticlesOn = true;
    }
}
