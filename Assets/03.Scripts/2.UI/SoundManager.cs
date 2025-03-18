using UnityEngine;
using UnityEngine.UI;

public class SoundManager : MonoBehaviour
{
    public static SoundManager instance;

    public AudioSource bgmSource;
    public AudioSource[] sfxSources;

    public Slider masterSlider, bgmSlider, sfxSlider;

    private float masterVolume = 1f;
    private float bgmVolume = 1f;
    private float sfxVolume = 1f;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }

    }

    private void Start()
    {
        if(masterSlider != null && bgmSlider !=null && sfxSlider != null)
        {
            LoadVolumeSettings();
        }

        
    }

    public void SetMasterVolume(float volume)
    {
        masterVolume = volume;
        UpdateVolume();
        PlayerPrefs.SetFloat("MasterVolume", volume);
    }

    public void SetBGMVolume(float volume)
    {
        bgmVolume = volume;
        UpdateVolume();
        PlayerPrefs.SetFloat("BGMVolume", volume);
    }

    public void SetSFXVolume(float volume)
    {
        sfxVolume = volume;
        UpdateVolume();
        PlayerPrefs.SetFloat("SFXVolume", volume);
    }

    public void LoadVolumeSettings()
    {
        masterVolume = PlayerPrefs.GetFloat("MasterVolume", 1f);
        bgmVolume = PlayerPrefs.GetFloat("BGMVolume", 1f);
        sfxVolume = PlayerPrefs.GetFloat("SFXVolume", 1f);

        masterSlider.value = masterVolume;
        bgmSlider.value = bgmVolume;
        sfxSlider.value = sfxVolume;

        UpdateVolume();

        masterSlider.onValueChanged.AddListener(SetMasterVolume);
        bgmSlider.onValueChanged.AddListener(SetBGMVolume);
        sfxSlider.onValueChanged.AddListener(SetSFXVolume);
    }

    private void UpdateVolume()
    {
        if (bgmSource != null)
            bgmSource.volume = masterVolume * bgmVolume;

        foreach (AudioSource sfx in sfxSources)
        {
            if (sfx != null)
                sfx.volume = masterVolume * sfxVolume;
        }
    }

    public void PlaySFX(AudioClip clip, float volume = 1f)
    {
        if (clip != null && sfxSources.Length > 0)
        {
            sfxSources[0].PlayOneShot(clip, volume * sfxVolume * masterVolume);
        }
    }
}
