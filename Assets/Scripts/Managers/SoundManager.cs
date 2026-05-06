using UnityEngine;

public class SoundManager : MonoBehaviour
{
    private AudioSource audioSource;

    public AudioSource AudioSource => audioSource;

    public AudioClip lootTakenSfx;
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
