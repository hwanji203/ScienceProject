using System;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;

public class AudioManager : MonoBehaviour
{
    public AudioMixer mixer;
    public Slider bgmSlider;
    public Slider sfxSlider;

    void Start()
    {
        float bgm = PlayerPrefs.GetFloat(SliderType.BGM.ToString(), 0.75f);
        float sfx = PlayerPrefs.GetFloat(SliderType.SFX.ToString(), 0.75f);

        bgmSlider.value = bgm;
        sfxSlider.value = sfx;

        SetBGMVolume(bgm);
        SetSFXVolume(sfx);

        bgmSlider.onValueChanged.AddListener(SetBGMVolume);
        sfxSlider.onValueChanged.AddListener(SetSFXVolume);
    }

    public void SetBGMVolume(float value)
    {
        mixer.SetFloat("BGMVolume", Mathf.Log10(Mathf.Clamp(value, 0.0001f, 1f)) * 20);
    }

    public void SetSFXVolume(float value)
    {
        mixer.SetFloat("SFXVolume", Mathf.Log10(Mathf.Clamp(value, 0.0001f, 1f)) * 20);
    }
}
