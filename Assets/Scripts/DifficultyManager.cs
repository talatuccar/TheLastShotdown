using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DifficultyManager : MonoBehaviour
{
    public ActiveDifficultySO activeDifficulty;

    public void SetEnemyCount(DifficultyData difficultyData)
    {
        // Eðer obje yok ediliyorsa (hata almamak için) iþlemi durdur
        if (this == null || activeDifficulty == null) return;

        //activeDifficulty.currentDifficulty = difficultyData;
        PlayerPrefs.SetInt("dif", difficultyData.spawnedEnemyCount);

        SceneManager.LoadScene("LoadingScene");
    }

    
}