using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneController : MonoBehaviour
{
    public static SceneController instance;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void LoadTitleScene()
    {

    }




    //private const string LastSceneKey = "LastPlayedScene";

    //public void SaveCurrentScene()
    //{
    //    string currentScene = SceneManager.GetActiveScene().name;
    //    PlayerPrefs.SetString(LastSceneKey, currentScene);
    //    PlayerPrefs.Save();
    //}

    //public void LoadLastScene()
    //{
    //    if (PlayerPrefs.HasKey(LastSceneKey))
    //    {
    //        string lastScene = PlayerPrefs.GetString(LastSceneKey);
    //        SceneManager.LoadScene(lastScene);
    //    }
    //}

    //private void OnApplicationQuit()
    //{
    //    SaveCurrentScene();
    //}
    //private void OnApplicationPause(bool pause)
    //{
    //    if (pause)
    //    {
    //        SaveCurrentScene();
    //    }
    //}
}
