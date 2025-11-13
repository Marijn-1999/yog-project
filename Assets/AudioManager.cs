using UnityEngine;

public class AudioManager : MonoBehaviour
{
    [Header("----- Audio Source -----")]
    [SerializeField] AudioSource musicSource;
    [SerializeField] AudioSource SFXSource;

    [Header("----- Audio Clips -----")]
    public AudioClip backgroundSound1;
    public AudioClip backgroundSound2;
    public AudioClip backgroundMusic;

    private void Start()
    {
        musicSource.clip = backgroundMusic;
        musicSource.Play();
        musicSource.clip = backgroundSound1;
        musicSource.Play();
        musicSource.clip = backgroundSound2;
        musicSource.Play();
    }
}
