using UnityEngine;
using UnityEngine.UI;

public class Setting : MonoBehaviour
{
    public GameObject SettingBtn;
    public Button activateButton;
    public bool isActive = false;

    void Start()
    {
        //if (activateButton != null)
        //{
        //    activateButton.onClick.AddListener(ActivateObject);
        //}
    }

    public void PauseButton()
    {
        if (!isActive)
        {
            isActive = true;
            SettingBtn.SetActive(isActive);
        }
        else
        {
            isActive = false;
            SettingBtn.SetActive(isActive);
        }
    }

    void ActivateObject()
    {
        SettingBtn.SetActive(false);
    }
}
