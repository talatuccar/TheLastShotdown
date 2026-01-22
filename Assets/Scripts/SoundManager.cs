using UnityEngine;

public class SoundManager : MonoBehaviour
{
    AudioSource audioSource;

    public static SoundManager Instance { get; private set; }
    private void Awake()
    {
        
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        
    }
    void Start()
    {
        audioSource = GetComponent<AudioSource>();

    }

    public void PlayAudioClip(AudioClip audioClip)
    {
        audioSource.clip = audioClip;
        audioSource.Play();

    }

    
}
