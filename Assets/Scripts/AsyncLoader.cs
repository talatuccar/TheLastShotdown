using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;
using TMPro;

public class AsyncLoader : MonoBehaviour
{
    [Header("Ayarlar")]
    public string targetSceneName = "SampleScene"; 
    public float fakeLoadingSpeed = 0.5f; 

    [Header("UI Elemanlarý")]
    public Image progressCircle; 
    public TextMeshProUGUI percentText;
    public MenuInfoDataSo menuInfoDataSo;
    public GameObject infoPanel;
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
        int randomIndex = Random.Range(0, menuInfoDataSo.menuInfoDatas.Length);
        var selectedData = menuInfoDataSo.menuInfoDatas[randomIndex];
        infoPanel.transform.GetChild(0).GetComponent<Image>().sprite = selectedData.menuIcon;
        infoPanel.GetComponentInChildren<TextMeshProUGUI>().text = selectedData.menuDescription;

    }
    IEnumerator LoadLevelAsync()
    {
        yield return new WaitForSeconds(0.2f);

        AsyncOperation operation = SceneManager.LoadSceneAsync(targetSceneName);
        operation.allowSceneActivation = false;

        float progress = 0;

        while (!operation.isDone)
        {
         
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