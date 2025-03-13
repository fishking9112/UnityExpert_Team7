using UnityEngine;
using UnityEngine.UI;
using System.IO;

/// <summary>
/// 다음 씬의 미리보기 이미지를 표시
/// </summary>
/// 
public class SceneDisplay : MonoBehaviour
{
    public Image previewImage;
    public string nextSceneName;

    void Start()
    {
        LoadScenePreview(nextSceneName);
    }

    void LoadScenePreview(string sceneName)
    {
        string filePath = Application.persistentDataPath + "/" + sceneName + ".png";
        if (File.Exists(filePath))
        {
            byte[] imageBytes = File.ReadAllBytes(filePath);
            Texture2D texture = new Texture2D(2, 2);
            texture.LoadImage(imageBytes);
            
            // Texture2D를 Sprite로 변환 후 UI Image에 적용
            Sprite sprite = Sprite.Create(texture, new Rect(0, 0, texture.width, texture.height), new Vector2(0.5f, 0.5f));
            previewImage.sprite = sprite;
        }
        else
        {
            Debug.LogWarning("No preview image found for scene: " + sceneName);
        }
    }
}
