using UnityEngine;

public class Setting : MonoBehaviour
{
    /// <summary>
    /// Setting 버튼을 눌렀을 때 SoundManager UI 활성화
    /// </summary>
    public GameObject SettingBtn;
    bool isSetting = false;


    public void OnClickSetting()
    {
        isSetting = !isSetting;
        SettingBtn.SetActive(isSetting);
    }
}
