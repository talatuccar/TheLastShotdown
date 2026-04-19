using TMPro;
using UnityEngine;

public class StatsPanel : MonoBehaviour
{
    public TextMeshProUGUI headShotRatioTxt;
    void Start()
    {
        headShotRatioTxt.text = PlayerStats.HeadShotRatio().ToString();
    }

    // Update is called once per frame
    void Update()
    {

    }
}
