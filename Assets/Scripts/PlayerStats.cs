using System.Runtime.InteropServices.WindowsRuntime;
using UnityEngine;

public static class PlayerStats
{
    public static int headshotCounter;
    public static int totalShootedBullet;

    public static float HeadShotRatio()
    {

        //int headshotRatio = (headshotCounter / totalShootedBullet) * 100;
        //return headshotRatio;
        float ratio = ((float)headshotCounter / totalShootedBullet) * 100;
        return Mathf.RoundToInt(ratio);
    }

}
