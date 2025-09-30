using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [SerializeField] private GameObject settingWindow;
    [SerializeField] private Slider sfxSlider;
    [SerializeField] private Slider bgmSlider;

    private float tempBgmVolume;
    private float tempSfxVolume;

    private void Start()
    {
        float savedBgm = PlayerPrefs.GetFloat("Volume", 1.0f);
        float savedSfx = PlayerPrefs.GetFloat("Volume", 1.0f);
        
        bgmSlider.value = savedBgm;
        sfxSlider.value = savedSfx;
        
        bgmSlider.onValueChanged.AddListener(value =>
        {
            tempBgmVolume = value;
            SoundManager.Instance.bgmAudioSource.volume = tempBgmVolume;
        });
        
        sfxSlider.onValueChanged.AddListener(value =>
        {
            tempSfxVolume = value;
            SoundManager.Instance.sfxAudioSource.volume = tempSfxVolume;
        });
        
        tempBgmVolume = savedBgm;
        tempSfxVolume = savedSfx;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (!settingWindow.activeSelf)
                Open();
            else
                Close();
        }
    }

    public void Save()
    {
        PlayerPrefs.SetFloat("BGMVolume", tempBgmVolume);
        PlayerPrefs.SetFloat("SFXVolume", tempSfxVolume);
        PlayerPrefs.Save();
        Close();
    }

    public void Exit()
    {
        Application.Quit();
    }

    public void Open()
    {
        settingWindow.SetActive(true);
        Time.timeScale = 0;
    }
    
    public void Close()
    {
        settingWindow.SetActive(false);
        Time.timeScale = 1;
    }

    public void BGMVolume()
    {
        SoundManager.Instance.bgmAudioSource.volume = bgmSlider.value;
        SoundManager.Instance.OnChange();
    }

    public void SFXVolume()
    {
        SoundManager.Instance.sfxAudioSource.volume = sfxSlider.value;
        SoundManager.Instance.OnChange();
    }

    public void GameStart()
    {
        SceneManager.LoadScene(1);
        SoundManager.Instance.Play(BGMSoundType.GameScene);
    }
}
