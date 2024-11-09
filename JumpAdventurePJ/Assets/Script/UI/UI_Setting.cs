using TMPro;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class UI_Setting : MonoBehaviour
{
    [SerializeField] private AudioMixer audioMixer;
    [SerializeField] private float mixerMultiplier = 25;

    [Header("SFX Settings")]
    [SerializeField] public Slider sfxSlider;
    [SerializeField] private TextMeshProUGUI sfxSliderText;
    [SerializeField] public string sfxParameter;

    [Header("SFX Settings")]
    [SerializeField] public Slider bgmSlider;
    [SerializeField] private TextMeshProUGUI bgmSliderText;
    [SerializeField] public string bgmParameter;

     private void OnDisable()
    {
        PlayerPrefs.SetFloat(sfxParameter, sfxSlider.value);
        PlayerPrefs.SetFloat(bgmParameter, bgmSlider.value);
    }

    private void OnEnable()
    {
        sfxSlider.value = PlayerPrefs.GetFloat(sfxParameter, 0.4f);
        bgmSlider.value = PlayerPrefs.GetFloat(bgmParameter, 0.4f);
    }
    
    // SFX 슬라이더 값 조정
    public void SFXSliderValue(float value)
    {
        sfxSliderText.text = Mathf.RoundToInt(value * 100) + "%";
        float newValue = Mathf.Log10(value) * mixerMultiplier;
        audioMixer.SetFloat(sfxParameter, newValue);
    }

    // BGM 슬라이더 값 조정
    public void BGMSliderValue(float value)
    {
        bgmSliderText.text = Mathf.RoundToInt(value * 100) + "%";
        float newValue = Mathf.Log10(value) * mixerMultiplier;
        audioMixer.SetFloat(bgmParameter, newValue);
    }

}