using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class HackingSystem : MonoBehaviour
{
    [SerializeField] private Image progressCircle;
    [SerializeField] private float hackDuration = 2.0f;
    [SerializeField] private GameObject scrollingScreen; 
    private float _currentTimer = 0f;
    private FPSInput _input;
    private bool _isCompleted = false;
    [SerializeField] private AudioClip matrixSFX;
    [SerializeField] private GameObject winTabPanel;
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
            ScrollScreenActivate(true);  
        }
        else
        {
            _currentTimer = 0f;
            progressCircle.fillAmount = 0f;
            ScrollScreenActivate(false);
        }
    }

    void ScrollScreenActivate(bool isActive)
    {
        if (scrollingScreen != null)
        {
            scrollingScreen.SetActive(isActive);
        }

    }
    void FinishHack()
    {
          
        if (progressCircle != null) progressCircle.transform.parent.gameObject.SetActive(false);

        GameManager.Instance.CompleteLevel(winTabPanel, matrixSFX);
    }  
}