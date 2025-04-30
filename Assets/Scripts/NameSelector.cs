using JetBrains.Annotations;
using UnityEditor.Overlays;
using UnityEngine;
using System.IO;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;


public class NameSelector : MonoBehaviour
{
    public int highScore;
    public TextMeshProUGUI bestScore;
    public static NameSelector Instance;
    public string playerName;
    public string bestPlayer;
    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        UpdateBestScore();
        if (bestPlayer != null)
        {
            bestScore.text = $"Best Score:\n {bestPlayer} - {highScore}";
        }
        else
            bestScore.text = $"Best Score: None";
    }

    public void SaveHighScore()
    {
        SaveData data = new SaveData();
        data.savedHighScore = highScore;
        data.savedPlayerName = playerName;
        bestPlayer = playerName;

        string json = JsonUtility.ToJson(data);

        File.WriteAllText(Application.persistentDataPath + "/savefile.json", json);
    }

    public void UpdateBestScore()
    {
        string path = Application.persistentDataPath + "/savefile.json";
        if (File.Exists(path))
        {
            string json = File.ReadAllText(path);
            SaveData data = JsonUtility.FromJson<SaveData>(json);

            bestPlayer = data.savedPlayerName;
            highScore = data.savedHighScore;

        }
    }

    [System.Serializable]
    class SaveData
    {
        public int savedHighScore;
        public string savedPlayerName;
    }

}
