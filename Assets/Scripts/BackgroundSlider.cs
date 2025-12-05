using UnityEngine;
using UnityEngine.UI;

public class BackgroundSlider : MonoBehaviour
{
    public AudioSource backgroundAudio;
    public Slider volumeSlider;
    private void Start()
    {

        float savedVolume = PlayerPrefs.GetFloat("BackgroundVolume", 1f);

        volumeSlider.value = savedVolume;
        backgroundAudio.volume = savedVolume;

        volumeSlider.onValueChanged.AddListener(OnVolumeChange);
    }
    private void OnVolumeChange(float value)
    {
        backgroundAudio.volume = value;

        PlayerPrefs.SetFloat("BackgroundVolume", value);
    }
}
