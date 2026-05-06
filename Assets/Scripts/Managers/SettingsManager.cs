using UnityEngine;

public static class SettingsManager
{
   
    public static int SelectedCrosshairID
    {
        get => PlayerPrefs.GetInt("SelectedCrosshair"); 
        set => PlayerPrefs.SetInt("SelectedCrosshair", value);
    }
}
