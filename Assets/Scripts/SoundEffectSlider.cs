using UnityEngine;
using UnityEngine.UI;
public class SoundEffectSlider : MonoBehaviour
{
    public AudioSource sfxSource;
    public Slider sfxSlider;

    private void Start()
    {
        float savedVolume = PlayerPrefs.GetFloat("SFXVolume", 1f);

        sfxSlider.value = savedVolume;
        sfxSource.volume = savedVolume;
        sfxSlider.onValueChanged.AddListener(OnVolumeChange);
    }
    private void OnVolumeChange(float value)
    {
        sfxSource.volume = value;
        PlayerPrefs.SetFloat("SFXVolume", value);
    }
}
