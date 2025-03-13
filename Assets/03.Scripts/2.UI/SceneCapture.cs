using UnityEngine;
using System.IO;

public class SceneCapture : MonoBehaviour
{
    /// <summary>
    /// 이동할 씬의 화면을 캡쳐할 카메라 
    /// </summary>
    public Camera sceneCamera;

    void Start()
    {
        CaptureSceneScreenshot();
    }

    void CaptureSceneScreenshot()
    {
        int width = Screen.width;
        int height = Screen.height;
        RenderTexture rt = new RenderTexture(width, height, 24);
        sceneCamera.targetTexture = rt;
        sceneCamera.Render();

        Texture2D screenShot = new Texture2D(width, height, TextureFormat.RGB24, false);
        RenderTexture.active = rt;
        screenShot.ReadPixels(new Rect(0, 0, width, height), 0, 0);
        screenShot.Apply();

        byte[] bytes = screenShot.EncodeToPNG();
        string sceneName = UnityEngine.SceneManagement.SceneManager.GetActiveScene().name;
        string filePath = Application.persistentDataPath + "/" + sceneName + ".png";
        File.WriteAllBytes(filePath, bytes);

        Debug.Log("Screenshot saved at: " + filePath);

        sceneCamera.targetTexture = null;
        RenderTexture.active = null;
        Destroy(rt);
    }
}
