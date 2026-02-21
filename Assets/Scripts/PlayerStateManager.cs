using System;
using UnityEngine;

public class PlayerStateManager : MonoBehaviour
{
    public static PlayerStateManager Instance;

    
    //public static event Action<bool> OnToggleControls;

    void Awake() => Instance = this;

    public void SetControls(bool state)
    {
        
        //OnToggleControls?.Invoke(state);
    }
}