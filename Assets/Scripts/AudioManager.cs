using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    [Header("---------Audio Source----------")]
    [SerializeField] private AudioSource musicSource;
    [SerializeField] private AudioSource SFXSource;

    [Header("---------Audio Clip----------")]
    public AudioClip background;
    public AudioClip Play;
    public AudioClip HornSound;
    public AudioClip formationSFX;
    public AudioClip deformationSFX;
    public AudioClip ManHurt;

    [Header("---------Settings----------")]
    public bool usePreviousBGM = false;

    public static AudioManager instance;
    private static AudioClip lastBGM;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject); // Only first AudioManager will persist
        }
        else
        {
            // If another AudioManager already exists:
            if (!usePreviousBGM)
            {
                instance.PlayNewBackground(background); // Play new BGM
            }

            Destroy(gameObject); // Remove the extra AudioManager
            return;
        }
    }

    private void Start()
    {
        if (!usePreviousBGM)
        {
            PlayNewBackground(background);
        }
        else if (lastBGM != null && musicSource.clip != lastBGM)
        {
            musicSource.clip = lastBGM;
            musicSource.loop = true;
            musicSource.Play();
        }
    }

    private void PlayNewBackground(AudioClip newClip)
    {
        if (musicSource != null && newClip != null)
        {
            musicSource.Stop();
            musicSource.clip = newClip;
            musicSource.loop = true;
            musicSource.Play();
            lastBGM = newClip;
        }
    }

    public void PlaySFX(AudioClip clip)
    {
        if (clip != null && SFXSource != null)
        {
            SFXSource.PlayOneShot(clip);
        }
    }
}
