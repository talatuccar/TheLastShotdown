using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    [Header("Sub Managers")]
    public PasswordManager passwordManager;
    public PasswordUI passwordManagerUI; 
    public NumpadUI numpadUI;
    public SpawnManager spawnManager;
    //public SoundManager soundManager;
    //public PlayerInventory playerInventory; 

    public ActiveDifficultySO activeDifficulty; 


    private void Awake()
    {
       
        if (Instance == null)
        {
            Instance = this;
            //DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
            return;
        }

        
        if (passwordManager != null) passwordManager.Initialize();

       
    }

    private void Start()
    {
        TriggerSpawn();
       
    }

 
    void TriggerSpawn()
    {
        //if (DifficultyManager.ChosenDifficulty != null)
        //{
        //    spawnManager.SpawnEnemies(DifficultyManager.ChosenDifficulty.spawnedEnemyCount);
        //}

        //spawnManager.SpawnEnemies(activeDifficulty.currentDifficulty.spawnedEnemyCount);

        spawnManager.SpawnEnemies(PlayerPrefs.GetInt("dif"));
       
    }
}
