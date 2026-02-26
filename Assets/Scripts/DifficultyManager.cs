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

        activeDifficulty.currentDifficulty = difficultyData;

#if UNITY_EDITOR
        UnityEditor.EditorUtility.SetDirty(activeDifficulty);
        UnityEditor.AssetDatabase.SaveAssets();
#endif
        StartCoroutine(LoadSceneDelayed("SampleScene", 0.5f));
    }

    private IEnumerator LoadSceneDelayed(string sceneName, float delay)
    {
        // Butonun birden fazla basýlmasýný engelleyebilirsin (Opsiyonel)
        // gameObject.GetComponent<UnityEngine.UI.Button>().interactable = false;

        Debug.Log("<color=orange>Sahne yükleniyor, bekleyin...</color>");

        // Yarým saniye beklemek Unity'nin kendine gelmesi için yeterlidir
        yield return new WaitForSeconds(delay);

        // Sahneyi asenkron yüklemek referans hatalarýný azaltýr
        SceneManager.LoadScene(sceneName);
    }
}