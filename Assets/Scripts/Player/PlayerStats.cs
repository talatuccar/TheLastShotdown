using UnityEngine;

public static class PlayerStats
{
    public static int headshotCounter;
    public static int totalShootedBullet;
    public static float totalHitDistance;
    public static int totalSuccessfulHits;
    public static float HeadShotRatio()
    {

       
        float headshotRatio = ((float)headshotCounter / totalShootedBullet) * 100;
        return Mathf.RoundToInt(headshotRatio);
    }

   
    public static float AverageHitDistance()
    {
        if (totalSuccessfulHits == 0) return 0;
        return Mathf.RoundToInt(totalHitDistance / totalSuccessfulHits);
    }

    public static int GetAccuracy()
    {
        if (totalShootedBullet == 0) return 0;

        float accuracy = ((float)totalSuccessfulHits / totalShootedBullet) * 100;
        return Mathf.RoundToInt(accuracy);
    }
}
