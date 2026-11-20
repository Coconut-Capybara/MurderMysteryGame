using UnityEngine;

public class ItemUseSoundController : MonoBehaviour
{
    [SerializeField] private AudioSource mainAudioSource;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    public void PlayItemSound(AudioClip clip)
    {
        mainAudioSource.clip = clip;
        mainAudioSource.Play();
    }

    public void PlayInteractSound(AudioClip clip)
    {
        mainAudioSource.clip = clip;
        mainAudioSource.Play();
    }
}
