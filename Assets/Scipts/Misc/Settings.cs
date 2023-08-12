using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Settings
{
    public static float scoreVal;
    public static float clickMultiplayer = 1;
    public static float upgradeMultiplayer = 0;

    //Upgrades/Info
    public static float[] upgradeCost= { 10, 100, 2, };
    public static float[] upgradePerSecond = { 0.013f, 1f, 2, };

    public static float[] upgradeInfoPerSecond = { 0.2f, 1f, 2, };
    public static float[] upgradeInfoAmount = { 0.2f, 1f, 2, };
    public static float[] upgradeInfoSoFar = { 0.2f, 1f, 2, };

    public static float[] upgradesMultiplayers = { 1, 1, 1, 1 };

    public static float[] passiveUpgradeCost = { 10, 50, 100, 200};
    public static float[] passiveUpgradeBonus = { 2, 1.2f, 0.8f };
    public static string[] bonusText = { "increases by ", "increases by", "decreases price by" };

    public static float[] passiveUpgradeInfoBonus = { 0.2f, 1f, 2, };

    //stats
    public static float totalAmount;
    public static float totalClicks;
    public static float totalAmountOfActiveUpgrades;
    public static float totalAmountOfPassiveUpgrades;

    public static bool[] passiveUpsUnlocked = new bool[6]; //passiveUpgradeCost.Length




    public static string Format(float number)
    {
        const float thousand = 1e3f;
        const float million = 1e6f;
        const float billion = 1e9f;
        const float trillion = 1e12f;
        const float quadrillion = 1e15f;
        const float quintillion = 1e18f;

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
}
