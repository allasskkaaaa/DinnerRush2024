using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AudioManager : MonoBehaviour
{
    [SerializeField] private AudioSource musicSource;
    [SerializeField] private AudioSource SFXSource;

    [SerializeField] private Slider volumeSlider;

    private void Start()
    {
        setVolume();
    }
    public void playOneShot(AudioClip clip)
    {
        SFXSource.PlayOneShot(clip);
    }

    public void setVolume()
    {
        musicSource.volume = volumeSlider.value;
        SFXSource.volume = volumeSlider.value;
    }

    public void saveSetting()
    {

    }
}
