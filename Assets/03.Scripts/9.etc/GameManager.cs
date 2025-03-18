using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public Player player;

    public int curStage;
    public int lastStage;



    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }

        lastStage = PlayerPrefs.GetInt("lastStage");
    }

    public void StageClear()
    {
        curStage += 1;
        PlayerPrefs.SetInt("lastStage", curStage);
        PlayerPrefs.Save();
        SceneManager.LoadScene($"Stage{curStage}");
    }

    public void StartFirstStage()
    {
        curStage = 1;
        PlayerPrefs.SetInt("lastStage", curStage);
        PlayerPrefs.Save();
        SceneManager.LoadScene($"Stage{curStage}");
    }
}
