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
    public static SaveManager instance;


    private string saveFilePath;

    void Awake()
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
        saveFilePath = Path.Combine(Application.persistentDataPath, "saveData.json");
    }

    public void SaveLastStage()
    {
        SaveData saveData = new SaveData();
        saveData.lastStage = SceneManager.GetActiveScene().name;

        string json = JsonUtility.ToJson(saveData, true);
        File.WriteAllText(saveFilePath, json);
    }

    public void LoadLastStage()
    {
        if (File.Exists(saveFilePath))
        {
            string json = File.ReadAllText(saveFilePath);
            SaveData saveData = JsonUtility.FromJson<SaveData>(json);

            if (!string.IsNullOrEmpty(saveData.lastStage))
            {
                SceneManager.LoadScene(saveData.lastStage);
            }
        }
    }
}