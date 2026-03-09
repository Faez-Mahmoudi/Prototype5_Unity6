using UnityEngine;
using System.IO;

public class MainManager : MonoBehaviour
{
    public static MainManager Instance{get; private set;}
    public int bestEasy;
    public int bestMedium;
    public int bestHard;
    public float musicVolume;
    public bool isGameActive;

    public AudioSource my_audio;

    // Save our data
    [System.Serializable]
    class SaveData
    {
        public int b_easy;
        public int b_medium;
        public int b_hard;
        public float m_volume;
    }

    void Awake()
    {
       if (Instance != null)
       {
            Destroy(gameObject);
            return;
       }

       Instance = this;
       DontDestroyOnLoad(gameObject);
       my_audio = GetComponent<AudioSource>(); 
       isGameActive = true;
       LoadScore();
       my_audio.volume = musicVolume;
    }

    public void SaveScore()
    {
        SaveData data = new SaveData();
        data.b_easy = bestEasy;
        data.b_medium = bestMedium;
        data.b_hard = bestHard;
        data.m_volume = musicVolume;

        string json  = JsonUtility.ToJson(data);
        File.WriteAllText(Application.persistentDataPath + "/savefile.json", json);
    }

    public void LoadScore()
    {
        string path = Application.persistentDataPath + "/savefile.json";

        if (File.Exists(path))
        {
            string json = File.ReadAllText(path);
            SaveData data = JsonUtility.FromJson<SaveData>(json);

            bestEasy = data.b_easy;
            bestMedium = data.b_medium;
            bestHard = data.b_hard;
            musicVolume = data.m_volume;
        }
        else
        {
            bestEasy = 0;
            bestMedium = 0;
            bestHard = 0;
            musicVolume = 1.0f;
        }
    }
}
