using Assets._Data._Scripts.Level;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelController : MonoBehaviour
{
    public static LevelController Instance { get; private set; }
    public int currentLevel;
    public List<LevelData> saveData;
    public int totalStar = 0;
    public GameData gameData;
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
        ISaveGame save = new SaveController();
        SaveDataWrapper saveDataWrapper = save.Load<SaveDataWrapper>("DataLevel.json");
        if (saveDataWrapper != null)
        {
            saveData = saveDataWrapper.levelData.ToList();
        }
        else
        {
            saveData = gameData.saveData;
        }
        totalStar = saveData.Sum(data => data.star);

    }

    public void SelectLevel(int level)
    {
        currentLevel = level;
        string levelSelect = "Level " + level;
        SceneManager.LoadScene(levelSelect);
    }

    public void UpdateSaveData(LevelData data)
    {
        foreach (LevelData item in saveData)
        {
            if (item.level == data.level)
            {
                item.star = data.star;
                item.unlock = data.unlock;
            }
        }
        totalStar = saveData.Sum(data => data.star);
        SaveUpdate();
    }
    public void SaveUpdate()
    {
        SaveDataWrapper saveDataWrapper = new() { levelData = saveData.ToArray() };
        ISaveGame saveGame = new SaveController();
        saveGame.Save<SaveDataWrapper>(saveDataWrapper, "DataLevel.json");
    }

    public LevelData GetOneLevel(int level)
    {
        if (level > saveData.Count)
        {
            return null;
        }
        foreach (LevelData item in saveData)
        {
            if (item.level == level)
            {
                return item;
            }
        }
        return default;
    }
}
