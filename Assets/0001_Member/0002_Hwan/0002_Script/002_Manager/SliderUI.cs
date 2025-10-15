using TMPro;
using UnityEngine;
using UnityEngine.UI;
public enum SliderType
{
    SFX,
    BGM,
    Speeeed
}

public class SliderUI : MonoBehaviour
{
    [SerializeField] private SliderType myValueType;
    [SerializeField] private PlayerMovement pm;
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


    }

    private void Start()
    {
        Initialized();
        GameManager.Instance.iniCount++;
        if (GameManager.Instance.iniCount == 3) GameObject.Find("Menu").SetActive(false);
    }


    private void ChangeValue(float sliderValue)
    {
        saveManager.SaveValue(myValueType.ToString(), sliderValue / maxValue);
        changeable.OnChange();
    }

    private void Initialized()
    {
        switch (myValueType)
        {
            case SliderType.SFX:
                changeable = soundManager.GetComponent<IChangeable>();
                break;
            case SliderType.BGM:
                changeable = soundManager.GetComponent<IChangeable>();
                break;
            case SliderType.Speeeed:
                changeable = pm;
                break;
        }

        maxValue = slider.maxValue;
        slider.value = PlayerPrefs.GetFloat(myValueType.ToString(), NewValue()) * maxValue;

        slider.onValueChanged.AddListener((sliderValue) =>
        {
            soundManager.Play(SFXSoundType.SliderValueChange);
            ChangeValue(sliderValue);
        });
    }

    private float NewValue()
    {
        if (!PlayerPrefs.HasKey(myValueType.ToString()))
        {
            ChangeValue(0.5f * maxValue);
        }
        return 0.5f;
    }
}
