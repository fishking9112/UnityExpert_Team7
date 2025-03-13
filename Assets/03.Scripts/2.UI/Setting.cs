using UnityEngine;

public class Setting : MonoBehaviour
{
    /// <summary>
    /// Setting 버튼을 눌렀을 때 SoundManager UI 활성화
    /// </summary>
    public GameObject SettingBtn;

    public void PauseButton()
    {
        SettingBtn.SetActive(true);
    }

    public void ResumeButton()
    {
        SettingBtn.SetActive(false);
    }
}
