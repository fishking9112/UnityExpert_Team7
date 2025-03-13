using UnityEngine;
using UnityEngine.UI;

public class AudioMixer : MonoBehaviour
{
    public Slider bgmSlider;
    public Slider sfxSlider;

    void Start()
    {
        bgmSlider.value = SoundManager.instance.bgmSource.volume = 1;
        sfxSlider.value = SoundManager.instance.sfxSource.volume = 1;

        bgmSlider.onValueChanged.AddListener(SoundManager.instance.SetBGMVolume);
        sfxSlider.onValueChanged.AddListener(SoundManager.instance.SetSFXVolume);
    }
}
