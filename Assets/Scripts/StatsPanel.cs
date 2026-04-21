using TMPro;
using UnityEngine;

public class StatsPanel : MonoBehaviour
{
    public TextMeshProUGUI headShotRatioTxt;
    public TextMeshProUGUI AverageHitDistanceTxt;
    public TextMeshProUGUI AccuracyTxt;
    void Start()
    {
        AccuracyTxt.text = PlayerStats.GetAccuracy().ToString();
        headShotRatioTxt.text = PlayerStats.HeadShotRatio().ToString();
        AverageHitDistanceTxt.text = PlayerStats.AverageHitDistance().ToString() + " m";
    }

    // Update is called once per frame
    void Update()
    {

    }
}
