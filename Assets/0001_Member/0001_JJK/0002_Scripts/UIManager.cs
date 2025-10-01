using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [SerializeField] private GameObject settingWindow;

    private float tempBgmVolume;
    private float tempSfxVolume;


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

    public void GameStart()
    {
        SceneManager.LoadScene(1);
        SoundManager.Instance.Play(BGMSoundType.GameScene);
    }
}
