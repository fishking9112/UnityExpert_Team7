using UnityEngine;
using UnityEngine.SceneManagement;

public class ChangeScene : MonoBehaviour
{
    public static void Change(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }
}