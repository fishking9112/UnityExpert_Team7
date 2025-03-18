using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SoundMenuController : MonoBehaviour
{
    public Slider masterSlider, bgmSlider, sfxSlider;

    private void Start()
    {
        SoundManager.instance.masterSlider = masterSlider;
        SoundManager.instance.bgmSlider = bgmSlider;
        SoundManager.instance.sfxSlider = sfxSlider;

        SoundManager.instance.LoadVolumeSettings();
    }
}
