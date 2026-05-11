using UnityEngine;
using UnityEngine.SceneManagement;
public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }
    public GameObject playerPrefab;
    [Header("Sub Managers")]
    public PasswordManager passwordManager;
    public PasswordUI passwordManagerUI; 
    public NumpadUI numpadUI;
    public SpawnManager spawnManager;
    
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
        

        spawnManager.SpawnEnemies(PlayerPrefs.GetInt("difficulty"));
       
    }

    public void CompleteLevel(GameObject gameEndPanel, AudioClip gameEndSFX)
    {
        Time.timeScale = 0f;

        var playerInput = playerPrefab.GetComponent<FPSInput>();
        if (playerInput != null) playerInput.enabled = false;
        
       

       
        if (gameEndPanel != null) gameEndPanel.SetActive(true);

       
        if (gameEndSFX != null)
        {
            SoundManager.Instance.AudioSource.loop = true;
            SoundManager.Instance.PlayAudioClip(gameEndSFX);
           
        }

       
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;

       
    }

    public void GoToMainMenu()
    {
       
        Time.timeScale = 1f;
        SceneManager.LoadScene("Menu_Scene"); 
    }

    public void QuitGame()
    {
       
        Application.Quit();
      
    }
}
