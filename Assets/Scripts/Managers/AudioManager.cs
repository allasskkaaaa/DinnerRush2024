using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance;
    [SerializeField] private AudioSource musicSource;
    [SerializeField] private AudioSource SFXSource;

    [SerializeField] private Slider volumeSlider;
    [SerializeField] private float volumeValue;

    private void Start()
    {
        instance = this;
        setVolume();

    }
    public void playOneShot(AudioClip clip)
    {
        SFXSource.pitch = Random.Range((float)0.8, 1);
        SFXSource.PlayOneShot(clip);
    }

    public void setVolume()
    {
        if (volumeSlider != null)
        {
            volumeValue = volumeSlider.value;
        }
        
        if (musicSource != null) musicSource.volume = volumeValue;
        if (SFXSource != null) SFXSource.volume = volumeValue;
    }

}