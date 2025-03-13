using UnityEngine;
using System.IO;

public class SceneCapture : MonoBehaviour
{
    public Camera sceneCamera; // 캡처할 카메라

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

        // 저장 경로 설정
        byte[] bytes = screenShot.EncodeToPNG();
        string sceneName = UnityEngine.SceneManagement.SceneManager.GetActiveScene().name;
        string filePath = Application.persistentDataPath + "/" + sceneName + ".png";
        File.WriteAllBytes(filePath, bytes);

        Debug.Log("Screenshot saved at: " + filePath);

        // 메모리 정리
        sceneCamera.targetTexture = null;
        RenderTexture.active = null;
        Destroy(rt);
    }
}
