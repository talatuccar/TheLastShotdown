using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class HackingSystem : MonoBehaviour
{
    [SerializeField] private Image progressCircle;
    [SerializeField] private float hackDuration = 2.0f; // 2 saniyede dolsun
    [SerializeField] private GameObject scrollingScreen; // Ýkinci Canvas/RawImage
    private float _currentTimer = 0f;
    private FPSInput _input;
    private bool _isCompleted = false;
    [SerializeField] private AudioClip matrixSFX;

    void Awake() => _input = FindFirstObjectByType<FPSInput>();

    void Update()
    {
        if (_isCompleted || _input == null) return;

        if (_input.IsInteracting)
        {
            _currentTimer += Time.deltaTime;

            // Matematiksel Nan korumasý: hackDuration asla 0 olmamalý
            float progress = Mathf.Clamp01(_currentTimer / hackDuration);
            progressCircle.fillAmount = progress;

            if (_currentTimer >= hackDuration)
            {
                _isCompleted = true;
                FinishHack();
            }
        }
        else
        {
            _currentTimer = 0f;
            progressCircle.fillAmount = 0f;
        }
    }
    void FinishHack()
    {
        Debug.Log("Hacking Bitti!");

        // Bar Canvas'ýný kapat, Kayan Ekraný aç
        if (progressCircle != null) progressCircle.transform.parent.gameObject.SetActive(false);

        if (scrollingScreen != null)
        {
            scrollingScreen.SetActive(true);
        }

        StartCoroutine(PlaySFXMatrix());    
    }

    IEnumerator PlaySFXMatrix()
    {

        yield return new WaitForSeconds(2f);
        SoundManager.Instance.AudioSource.loop = true;
        SoundManager.Instance.PlayAudioClip(matrixSFX);
       
    }
}