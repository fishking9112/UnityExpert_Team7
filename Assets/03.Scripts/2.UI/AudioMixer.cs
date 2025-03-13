using UnityEngine;
using UnityEngine.UI;

public class AudioMixer : MonoBehaviour
{
    /// <summary>
    /// BGM, SFX 볼륨 조절 슬라이더
    /// </summary>
    /// 
    public Slider bgmSlider;
    public Slider sfxSlider;

    void Start()
    {
        bgmSlider.value = SoundManager.instance.bgmSource.volume;
        sfxSlider.value = SoundManager.instance.sfxSource.volume;

        bgmSlider.onValueChanged.AddListener(SoundManager.instance.SetBGMVolume);
        sfxSlider.onValueChanged.AddListener(SoundManager.instance.SetSFXVolume);
    }
}
