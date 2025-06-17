using Assets._Data._Scripts.Level;
using Assets._Data._Scripts.Save;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelController : MonoBehaviour
{
    public static LevelController instance { get; private set; }
    [SerializeField] private GameData gameData;


    public int currentLevel;
    public List<SaveData> saveData;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        SaveGame saveGame = new();
        gameData.saveData = saveGame.Load() == null ? gameData.saveData : gameData.saveData = saveGame.Load();
        saveData = gameData.saveData;
    }

    public void SelectLevel(int level)
    {
        currentLevel = level;
        string levelSelect = "Level " + level;
        SceneManager.LoadScene(levelSelect);
    }

    public void UpdateSaveDataSO(SaveData saveData)
    {
        foreach (SaveData item in gameData.saveData)
        {
            if (item.level == saveData.level)
            {
                item.star = saveData.star;
                item.unlock = saveData.unlock;
            }
        }
        SaveUpdate();
    }
    public void SaveUpdate()
    {
        SaveGame saveGame = new();
        saveGame.Save(gameData.saveData);
    }

    public SaveData GetOneLevel(int level)
    {
        if (level > gameData.saveData.Count)
        {
            return null;
        }
        foreach (SaveData item in gameData.saveData)
        {
            if (item.level == level)
            {
                return item;
            }
        }
        return default;
    }
}
