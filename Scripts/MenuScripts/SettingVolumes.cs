using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class SettingVolumes : MonoBehaviour
{
    public AudioMixer SFXAudioMixer;

    public Slider SFXSlider;


    public AudioMixer MusicAudioMixer;

    public Slider MusicSlider;


    public GameObject SettingsPanel;


    public void SetSFXVolume(float volume)
    {
        SFXAudioMixer.SetFloat("SFXVolume", volume);
        PlayerPrefs.SetFloat("SFX", volume);
    }

    public void SetMusicVolume(float volume)
    {
        MusicAudioMixer.SetFloat("MusicVolume", volume);
        PlayerPrefs.SetFloat("Music", volume);
    }

    public void EnableSettingPanel()
    {
        SettingsPanel.SetActive(true);
    }

    public void DisableSettingPanel()
    {
        SettingsPanel.SetActive(false);
    }

    private void OnEnable()
    {
        SFXAudioMixer.SetFloat("SFXVolume", PlayerPrefs.GetFloat("SFX", 0f));
        SFXSlider.value = PlayerPrefs.GetFloat("SFX");

        MusicAudioMixer.SetFloat("MusicVolume", PlayerPrefs.GetFloat("Music", 0f));
        MusicSlider.value = PlayerPrefs.GetFloat("Music");
    }
}
