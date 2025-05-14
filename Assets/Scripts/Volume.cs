using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class Volume : MonoBehaviour
{
    [SerializeField] private AudioMixer myMixer;
    [SerializeField] private Slider MusicSlider;
    [SerializeField] private Slider SFXSlider;


    private void Start()
    {
        if (PlayerPrefs.HasKey("MusicVolume"))
        {
            LoadVolume();
        }
        else
        {
            SetMusicVolume();
            SetSFXVolume();
        }
    }

    public void SetMusicVolume()
    {
        float volume = MusicSlider.value;

        if (volume == 0)
        {
            myMixer.SetFloat("music", -80f); // Mute
        }
        else
        {
            myMixer.SetFloat("music", Mathf.Log10(volume) * 20);
        }

        PlayerPrefs.SetFloat("MusicVolume", volume);
    }


    public void SetSFXVolume()
    {
        float volume = SFXSlider.value;

        if (volume == 0)
        {
            myMixer.SetFloat("SFX", -80f); // Mute
        }
        else
        {
            myMixer.SetFloat("SFX", Mathf.Log10(volume) * 20);
        }

        PlayerPrefs.SetFloat("SFXVolume", volume);
    }


    private void LoadVolume()       // back to game, the sound volume maintain before leaving game at last time
    {
        MusicSlider.value = PlayerPrefs.GetFloat("MusicVolume");
        SFXSlider.value = PlayerPrefs.GetFloat("SFXVolume");

        SetMusicVolume();
        SetSFXVolume();

    }


}