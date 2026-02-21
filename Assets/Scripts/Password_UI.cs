using TMPro;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PasswordUI : MonoBehaviour
{
   
    public List<TextMeshProUGUI> passwordSlots;

    [Header("Settings")]
    public Color highlightedColor = Color.yellow; 
    public Color normalColor = Color.white;
   

    void Start()
    {
       
        foreach (var slot in passwordSlots)
        {
            slot.text = "?";
            slot.color = normalColor;
        }
    }

    public void ShowPasswordFragment(int order, int value)
    {
       
        int index = order - 1;

        if (index >= 0 && index < passwordSlots.Count)
        {
            UpdateSlotRoutine(index, value);
        }
    }

    void UpdateSlotRoutine(int index, int value)
    {
       
        passwordSlots[index].text = value.ToString();
        passwordSlots[index].color = highlightedColor;

      
    }
}