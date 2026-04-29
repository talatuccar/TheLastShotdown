using UnityEngine;
using UnityEngine.UI;

public class DayNightSettings : MonoBehaviour
{
    public Toggle nightModeToggle; 

    void Start()
    {
      
        bool isNight = PlayerPrefs.GetInt("IsNightMode", 0) == 1;

        if (nightModeToggle != null)
        {
            nightModeToggle.isOn = isNight;
        }
    }

    public void SetNightMode(bool isNight)
    {
       
        PlayerPrefs.SetInt("IsNightMode", isNight ? 1 : 0);
        PlayerPrefs.Save();
    }
}