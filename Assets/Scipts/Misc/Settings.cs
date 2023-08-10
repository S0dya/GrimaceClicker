using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Settings
{
    public static float scoreVal;
    public static float clickMultiplayer = 1;
    public static float upgradeMultiplayer = 0;


    public static float[] upgradeCost= { 10, 100, 2, };
    public static float[] upgradePerSecond = { 0.013f, 1f, 2, };

    public static float[] upgradeInfoPerSecond = { 0.2f, 1f, 2, };
    public static float[] upgradeInfoAmount = { 0.2f, 1f, 2, };
        public static float[] upgradeInfoSoFar = { 0.2f, 1f, 2, };


    public static string Format(float number)
    {
        const float billion = 1e9f;
        const float million = 1e6f;
        const float thousand = 1e3f;

        if (number >= billion)
            return (number / billion).ToString("F1") + "B";
        else if (number >= million)
            return (number / million).ToString("F1") + "M";
        else if (number >= thousand)
            return (number / thousand).ToString("F1") + "K";
        else
            return number.ToString("F0");
    }
}
