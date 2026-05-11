using UnityEngine;
using UnityEngine.SceneManagement;

public class DifficultyManager : MonoBehaviour
{
    public ActiveDifficultySO activeDifficulty;

    public void SetEnemyCount(DifficultyData difficultyData)
    {
        
        if (this == null || activeDifficulty == null) return;

       
        PlayerPrefs.SetInt("difficulty", difficultyData.spawnedEnemyCount);

        SceneManager.LoadScene("LoadingScene");
    }    
}