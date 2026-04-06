using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;
using TMPro;
using NUnit.Framework.Internal;

public class AsyncLoader : MonoBehaviour
{
    [Header("Ayarlar")]
    public string targetSceneName = "SampleScene"; 
    public float fakeLoadingSpeed = 0.5f; 

    [Header("UI Elemanlarý")]
    public Image progressCircle; 
    public TextMeshProUGUI percentText;
    //public Sprite[] infoSprites;
    //public Image infoImage;

    public MenuInfoDataSo[] menuInfoDataSo;
    public GameObject infoPanel;
    //public ActiveDifficultySO activeDifficulty;
    //void Start()
    //{
    //    //activeDifficulty.currentDifficulty.spawnedEnemyCount = PlayerPrefs.GetInt("dif");
    //   
    //    //StartCoroutine(LoadLevelAsync());
    //    Invoke("StartCOR", 2f);
    //}

    //void StartCOR()
    //{


    //    StartCoroutine(LoadLevelAsync());
    //}

    //IEnumerator LoadLevelAsync()
    //{

    //    AsyncOperation operation = SceneManager.LoadSceneAsync(targetSceneName);


    //    operation.allowSceneActivation = false;

    //    float progress = 0;

    //    while (!operation.isDone)
    //    {
    //        
    //        float targetProgress = Mathf.Clamp01(operation.progress / 0.9f);

    //       
    //        progress = Mathf.MoveTowards(progress, targetProgress, fakeLoadingSpeed * Time.deltaTime);

    //        if (progressCircle != null) progressCircle.fillAmount = progress;
    //        if (percentText != null) percentText.text = "%" + (progress * 100).ToString("F0");

    //       
    //        if (progress >= 0.99f && operation.progress >= 0.9f)
    //        {
    //            operation.allowSceneActivation = true;
    //        }

    //        yield return null;
    //    }
    //}
    private void Awake()
    {
        SetInfoData();
    }

    void Start()
    {
      
        if (progressCircle != null) progressCircle.fillAmount = 0;
        if (percentText != null) percentText.text = "%0";

       
        StartCoroutine(LoadLevelAsync());
    }

    void SetInfoData()
    {
        int random = Random.Range(0, menuInfoDataSo.Length);
        infoPanel.transform.GetChild(0).GetComponent<Image>().sprite = menuInfoDataSo[random].menuIcon;
        infoPanel.GetComponentInChildren<TextMeshProUGUI>().text = menuInfoDataSo[random].menuDescription;

    }

    IEnumerator LoadLevelAsync()
    {
      
        yield return new WaitForSeconds(0.2f);

        AsyncOperation operation = SceneManager.LoadSceneAsync(targetSceneName);
        operation.allowSceneActivation = false;

        float progress = 0;

        while (!operation.isDone)
        {
            // Unity'nin gerçek yükleme deðeri
            float targetProgress = Mathf.Clamp01(operation.progress / 0.9f);

            // Barý yumuþakça doldur
            progress = Mathf.MoveTowards(progress, targetProgress, fakeLoadingSpeed * Time.deltaTime);

            if (progressCircle != null) progressCircle.fillAmount = progress;
            if (percentText != null) percentText.text = "%" + (progress * 100).ToString("F0");

            if (progress >= 0.99f && operation.progress >= 0.9f)
            {
                operation.allowSceneActivation = true;
            }

            yield return null;
        }
    }
}