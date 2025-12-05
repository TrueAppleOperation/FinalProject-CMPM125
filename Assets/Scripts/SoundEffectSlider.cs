using UnityEngine;
using UnityEngine.UI;

public class SoundEffectSlider : MonoBehaviour
{
    public Slider sfxSlider;

    private void Start()
    {
        float savedVolume = PlayerPrefs.GetFloat("SFXVolume", 1f);
        sfxSlider.value = savedVolume;
        if (SoundFXManager.instance != null)
        {
            SoundFXManager.instance.SetVolume(savedVolume);
        }

        sfxSlider.onValueChanged.AddListener(OnVolumeChange);
    }

    private void OnVolumeChange(float value)
    {
        PlayerPrefs.SetFloat("SFXVolume", value);
        PlayerPrefs.Save();

        if (SoundFXManager.instance != null)
        {
            SoundFXManager.instance.SetVolume(value);
        }
    }
}