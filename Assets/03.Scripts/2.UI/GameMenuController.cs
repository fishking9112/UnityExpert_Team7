using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using static UnityEditorInternal.VersionControl.ListControl;

/// <summary>
/// GameMenuCanvers 컨트롤러 입니다.
/// </summary>

public class GameMenuController : MonoBehaviour
{
    //CrossHair
    public CrossHair crossHair;
    [SerializeField] Button reStartBtn;
    [SerializeField] Button toTitleBtn;
    //Sound

    private void Awake()
    {
        crossHair = GetComponentInChildren<CrossHair>();
    }

    private void Start()
    {
        reStartBtn.onClick.AddListener(OnClickRestart);
        toTitleBtn.onClick.AddListener(OnClickToTitle);
    }
    void OnClickRestart()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
    void OnClickToTitle()
    {
        Time.timeScale = 1f;
        GameManager.Instance.UpdateLastStage();
        SceneManager.LoadScene("TitleScene");
    }
}
