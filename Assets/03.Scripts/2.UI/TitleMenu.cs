using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class TitleMenu : MonoBehaviour
{


    [SerializeField] Button startBtn;
    [SerializeField] Button continueBtn;
    [SerializeField] Button endBtn;
    //[SerializeField] Button settingBtn;
    //[SerializeField] Button closeSetting;

    [SerializeField] Slider masterVolumSlider;
    [SerializeField] Slider bgmVolumSlider;
    [SerializeField] Slider sfxVolumSlider;

    [SerializeField] GameObject soundMenu;
    bool canContinue;

    private void Start()
    {
        SoundManager.instance.masterSlider = masterVolumSlider;
        SoundManager.instance.bgmSlider = bgmVolumSlider;
        SoundManager.instance.sfxSlider = sfxVolumSlider;
        SoundManager.instance.LoadVolumeSettings();

        IsAbleContinuBtn(GameManager.Instance.lastStage > 1);

        startBtn.onClick.AddListener(onClickStart);
        continueBtn.onClick.AddListener(onClickContinue);
        //settingBtn.onClick.AddListener(OnClickSettingBtn);
        //closeSetting.onClick.AddListener(OnClickCloseSetting);
    }
    void IsAbleContinuBtn(bool value)
    {
        ColorBlock colors = continueBtn.colors;
        colors.normalColor = value? Color.white : Color.black;
        continueBtn.colors = colors;
        canContinue = value;
    }
    public void onClickStart()
    {
        GameManager.Instance.StartFirstStage();
    }
    public void onClickContinue()
    {
        if (canContinue)
        {
            SceneManager.LoadScene($"Stage{GameManager.Instance.lastStage}");
        }
    }

    public void onClickEndBtn()
    {

    }
    public void OnClickSettingBtn()
    {
        soundMenu.SetActive(true);
    }
    //public void OnClickCloseSetting()
    //{
    //    soundMenu.SetActive(false);
    //}
}
