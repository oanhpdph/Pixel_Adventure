using Assets._Data._Scripts.Level;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Assets._Data._Scripts.Save
{
    public class ResetData : MonoBehaviour
    {
        private void Start()
        {
            ResetGameData();
        }
        private void ResetGameData()
        {
            int sceneCount = SceneManager.sceneCountInBuildSettings;
            PlayerPrefs.SetInt("characterSelect", 0);
            PlayerPrefs.Save();
            List<SaveData> saveData = new();
            for (int j = 0; j < sceneCount; j++)
            {
                string name = "Level " + (j + 1);

                for (int i = 0; i < sceneCount; i++)
                {
                    string scenePath = SceneUtility.GetScenePathByBuildIndex(i);
                    string sceneFileName = System.IO.Path.GetFileNameWithoutExtension(scenePath);

                    if (sceneFileName == name)
                    {
                        SaveData data = new();
                        data.unlock = false;
                        data.star = 0;
                        data.level = j + 1;
                        if (j == 0)
                        {
                            data.unlock = true;
                        }
                        saveData.Add(data);
                    }
                }
            }
            SaveGame save = new();
            save.Save(saveData);
        }
    }
}