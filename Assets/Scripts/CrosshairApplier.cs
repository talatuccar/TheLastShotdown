using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class CrosshairApplier : MonoBehaviour
{
    public Image crosshairImage;
    public List<CrosshairData> allCrosshairs;

    void Awake()
    {
        int selectedID = SettingsManager.SelectedCrosshairID;

        
        CrosshairData data = allCrosshairs.Find(x => x.crosshairID == selectedID);

        if (data != null)
        {
            crosshairImage.sprite = data.crosshairSprite;
        }
    }
}
