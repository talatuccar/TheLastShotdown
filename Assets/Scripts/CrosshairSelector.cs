using UnityEngine;

public class CrosshairSelector : MonoBehaviour
{
    public void OnCrosshairButtonClicked(int id)
    {
        SettingsManager.SelectedCrosshairID = id;
       
    }
}
