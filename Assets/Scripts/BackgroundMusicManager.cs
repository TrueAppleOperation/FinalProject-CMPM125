using UnityEngine;
using UnityEngine.Audio;


public class BackgroundMusicManager : MonoBehaviour
{
    public static BackgroundMusicManager instance;

    [Header("Audio")]
    public AudioSource musicSource;
    public AudioClip musicClip;    [Range(0f, 1f)] public float volume = 1f;

    [Header("Mixer")]
    public AudioMixerGroup musicMixerGroup;


    private void Awake()
    {
        //using singlton to keep across game
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
            return;
        }


        if (musicSource == null)
        {
            musicSource = gameObject.AddComponent<AudioSource>();
            musicSource.loop = true;
            musicSource.playOnAwake = false;
        }

        musicSource.clip = musicClip;
        musicSource.volume = volume;
        musicSource.Play();
    }
 }
