using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEditor;

public class ProgressManager : MonoBehaviour
{
    public static ProgressManager Instance;
    public InputField playerNameInput;
    public string pName;
    public Text bestScoreText;
    public int bestScore;
    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(Instance);
            //return;
        }
        else
        {
            Instance = this;
        }
        DontDestroyOnLoad(gameObject);
        LoadData();
        bestScoreText.text = "Best Score: " + playerNameInput.text + ": " + bestScore;
    }
    public void SaveName()
    {
        pName = playerNameInput.text;
        //Debug.Log("Entext = " + playerName);
    }

    [System.Serializable]
    class SaveDetails
    {
        public string playerName;
        public int bestScore;
    }
    public void SaveData()
    {
        SaveDetails data = new SaveDetails
        {
            playerName = pName,
            bestScore = bestScore
        };
        string json = JsonUtility.ToJson(data);
        File.WriteAllText(Application.persistentDataPath + "/savefile1.json", json);
    }
    public void LoadData()
    {
        string path = Application.persistentDataPath + "/savefile1.json";
        if (File.Exists(path))
        {
            string json = File.ReadAllText(path);
            SaveDetails data = JsonUtility.FromJson<SaveDetails>(json);

            playerNameInput.text = data.playerName;
            bestScore = data.bestScore;
        }
    }
    public void StartGame()
    {
        SceneManager.LoadScene(1);  // 1 = main scene
    }
    public void QuitGame()
    {
#if UNITY_EDITOR
        EditorApplication.ExitPlaymode();
#else
        Application.Quit(); // original code to quit Unity player
#endif
    }
}
