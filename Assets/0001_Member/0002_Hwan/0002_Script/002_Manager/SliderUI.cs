using TMPro;
using UnityEngine;
using UnityEngine.UI;
public enum SliderType
{
    SFX,
    BGM
}

public class SliderUI : MonoBehaviour
{
    [SerializeField] private SliderType myValueType;
    private float maxValue;

    private IChangeable changeable;

    private Slider slider;

    private SoundManager soundManager;
    private SaveManager saveManager;

    private void Awake()
    {
        slider = GetComponent<Slider>();
        soundManager = SoundManager.Instance;
        saveManager = SaveManager.Instance;

        Initialized();

    }


    private void ChangeValue(float sliderValue)
    {
        saveManager.SaveValue(myValueType.ToString(), sliderValue / maxValue);
        changeable.OnChange();
    }

    private void Initialized()
    {
        maxValue = slider.maxValue;
        slider.value = PlayerPrefs.GetFloat(myValueType.ToString()) * maxValue;

        slider.onValueChanged.AddListener((sliderValue) =>
        {
            soundManager.Play(SFXSoundType.SliderValueChange);
            ChangeValue(sliderValue);
        });

        switch (myValueType)
        {
            case SliderType.SFX:
                changeable = soundManager.GetComponent<IChangeable>();
                break;
            case SliderType.BGM:
                changeable = soundManager.GetComponent<IChangeable>();
                break;
        }
    }
}
