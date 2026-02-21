using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    [Header("Sub Managers")]
    public PasswordManager passwordManager;
    public PasswordUI passwordManagerUI; 
    public NumpadUI numpadUI;

    //public SoundManager soundManager;
    //public PlayerInventory playerInventory; 

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            // DontDestroyOnLoad(gameObject); 
        }
        else
        {
            Destroy(gameObject);
            return;
        }

        
        if (passwordManager != null) passwordManager.Initialize();

       
    }
}
