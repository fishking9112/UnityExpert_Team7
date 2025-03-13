using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// 저장 및 로드할 데이터
/// </summary>
[System.Serializable]
public class SaveData
{
    public string lastStage;
}

public class SaveManager : MonoBehaviour
{
    private string saveFilePath;

    void Awake()
    {
        saveFilePath = Path.Combine(Application.persistentDataPath, "saveData.json");
    }

    public void SaveLastStage()
    {
        SaveData saveData = new SaveData();
        saveData.lastStage = SceneManager.GetActiveScene().name;

        string json = JsonUtility.ToJson(saveData, true);
        File.WriteAllText(saveFilePath, json);

        Debug.Log("저장됨: " + saveData.lastStage);
    }

    public void LoadLastStage()
    {
        if (File.Exists(saveFilePath))
        {
            // JSON 파일 읽기
            string json = File.ReadAllText(saveFilePath);
            SaveData saveData = JsonUtility.FromJson<SaveData>(json);

            if (!string.IsNullOrEmpty(saveData.lastStage))
            {
                SceneManager.LoadScene(saveData.lastStage);
                Debug.Log("로드됨 " + saveData.lastStage);
            }
            else
            {
                Debug.LogWarning("저장된 스테이지 없음");
            }
        }
        else
        {
            Debug.LogWarning("파일 없음");
        }
    }
}