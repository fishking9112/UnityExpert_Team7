using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// UI Manager 에서 다른 Prefab에 연결 될 UI들을 알고있다가 , 전달 해준다.
/// </summary>

public class UIManager : MonoBehaviour
{
    private static UIManager _instance;
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
