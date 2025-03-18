using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// 안쓸거에요 삭제 예정 !!
/// </summary>

public class UIManager : MonoBehaviour
{
    private static UIManager _instance;
    public Image fadeoutImg;
    public static UIManager Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = new GameObject("UIManager").AddComponent<UIManager>();
            }

            return _instance;
        }
    }

    [SerializeField] SoundManager soundManager;

    public CrossHair crossHair;

    private void Awake()
    {
        //싱글톤
        if (_instance == null)
        {
            _instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
}
