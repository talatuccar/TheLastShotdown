using UnityEngine;
using TMPro;

public class SensitivitySettings : MonoBehaviour
{
    
    public enum SensitivityLevel { Low, Medium, High }
    void Start()
    {
       
        int savedIndex = PlayerPrefs.GetInt("SensitivityIndex", 1);
        GetComponent<TMP_Dropdown>().value = savedIndex;
    }
    public void SetSensitivity(int index)
    {
        float sensitivityValue = 5f; 

        
        SensitivityLevel selectedLevel = (SensitivityLevel)index;

        switch (selectedLevel)
        {
            case SensitivityLevel.Low:
                sensitivityValue = 2f;
                break;
            case SensitivityLevel.Medium:
                sensitivityValue = 5f;
                break;
            case SensitivityLevel.High:
                sensitivityValue = 8f;
                break;
        }

        PlayerPrefs.SetFloat("MouseSensitivity", sensitivityValue);
        PlayerPrefs.SetInt("SensitivityIndex", index);
        PlayerPrefs.Save();

        
    }
}