using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public enum SFXSoundType
{
    None,
    SliderValueChange,
    BirdsSwing,
    GetBird,
    Hited
}

public enum BGMSoundType
{
    None,
    MainScene,
    GameScene,
    ClearScene
}
public class SoundManager : MonoSingleton<SoundManager>, IChangeable
{
    public class SoundData <T>
    {
        public T type;     // 사운드 구분 타입
        public AudioClip clip;   // 오디오 파일
    }

    [System.Serializable]
    public class SFXSoundData : SoundData<SFXSoundType> { }

    [System.Serializable]
    public class BGMSoundData : SoundData<BGMSoundType> { }

    [Header("SFX 사운드 데이터 목록")]
    [SerializeField] private List<SFXSoundData> sfxSounds;

    [Header("BGM 사운드 데이터 목록")]
    [SerializeField] private List<BGMSoundData> bgmSounds;

    private Dictionary<SFXSoundType, AudioClip> sfxSoundDict;
    [HideInInspector] public AudioSource sfxAudioSource;

    private Dictionary<BGMSoundType, AudioClip> bgmSoundDict;
    [HideInInspector] public AudioSource bgmAudioSource;
    private IChangeable _changeableImplementation;

    protected override void Awake()
    {
        base.Awake();

        AudioInitialized(out sfxAudioSource, out sfxSoundDict, sfxSounds);
        AudioInitialized(out bgmAudioSource, out bgmSoundDict, bgmSounds);
    }

    private void Start()
    {
        Play(BGMSoundType.MainScene);
    }

    private void AudioInitialized<T1, T2>(out AudioSource audioSource, out Dictionary<T1, AudioClip> dict, List<T2> data) where T2 : SoundData<T1>
    {
        audioSource = gameObject.AddComponent<AudioSource>();
        audioSource.playOnAwake = false;

        dict = new Dictionary<T1, AudioClip>();
        foreach (T2 s in data)
        {
            if (!dict.ContainsKey(s.type))
                dict.Add(s.type, s.clip);
        }
    }

    // 지정된 타입의 사운드 재생
    public void Play(SFXSoundType type)
    {
        if (sfxSoundDict.TryGetValue(type, out var clip))
        {
            sfxAudioSource.PlayOneShot(clip);
        }
    }
    public void Play(BGMSoundType type, float fadeTime = 1f)
    {
        if (bgmSoundDict.TryGetValue(type, out var clip))
        {
            StartCoroutine(FadeBGM(clip, fadeTime));
        }
    }

    private IEnumerator FadeBGM(AudioClip newClip, float fadeTime)
    {
        if (bgmAudioSource.isPlaying)
        {
            for (float t = 0; t < fadeTime; t += Time.deltaTime)
            {
                bgmAudioSource.volume = 1 - (t / fadeTime);
                yield return null;
            }
        }

        bgmAudioSource.clip = newClip;
        bgmAudioSource.Play();

        for (float t = 0; t < fadeTime; t += Time.deltaTime)
        {
            bgmAudioSource.volume = t / fadeTime;
            yield return null;
        }
        bgmAudioSource.volume = 1f;
    }

    public void OnChange()
    {
        bgmAudioSource.volume = PlayerPrefs.GetFloat("BGMVolume", 0.75f);
        sfxAudioSource.volume = PlayerPrefs.GetFloat("SFXVolume", 0.75f);
    }
}

public interface IChangeable
{
    public void OnChange();
}
