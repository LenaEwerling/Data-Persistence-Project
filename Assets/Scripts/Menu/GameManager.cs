using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public string Name;
    public int HighScore;
    public string CurrentPlayerName;

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);
        LoadStats();
    }

    [System.Serializable]
    class SaveData
    {
        public string Name;
        public int HighScore;
    }

    public void SaveScore(int score)
    {
        if (score > HighScore)
        {
            HighScore = score;
            Name = CurrentPlayerName;
        }
    }

    public void SaveStats()
    {
        SaveData data = new SaveData();
        data.Name = Name;
        data.HighScore = HighScore;

        string json = JsonUtility.ToJson(data);

        File.WriteAllText(Application.persistentDataPath + "/savefile.json", json);
    }

    public void LoadStats()
    {
        string path = Application.persistentDataPath + "/savefile.json";
        if (File.Exists(path))
        {
            string json = File.ReadAllText(path);
            SaveData data = JsonUtility.FromJson<SaveData>(json);

            Name = data.Name;
            HighScore = data.HighScore;
        }
        else 
        {
            Name = "Nobody";
            HighScore = 0;
        }
    }
}
