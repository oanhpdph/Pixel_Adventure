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
        }
        DontDestroyOnLoad(gameObject);
        saveData = gameData.saveData;
    }

    public void SelectLevel(int level)
    {
        currentLevel = level;
        string levelSelect = "Level " + level;
        SceneManager.LoadScene(levelSelect);
    }

    public void UpdateSaveDataScriptableObject(SaveData saveData)
    {
        foreach (SaveData item in gameData.saveData)
        {
            if (item.level == saveData.level)
            {
                item.star = saveData.star;
                item.unlock = saveData.unlock;
            }
        }
    }
    public void SaveUpdate()
    {
        SaveGame saveGame = new();
        saveGame.Save(gameData.saveData);
    }

    public SaveData GetOneLevel(int level)
    {
        foreach (SaveData item in gameData.saveData)
        {
            if (item.level == level)
            {
                return item;
            }
        }
        return null;
    }
}
