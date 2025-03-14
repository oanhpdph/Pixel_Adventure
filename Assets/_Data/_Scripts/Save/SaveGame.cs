using Assets._Data._Scripts.Level;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEngine;

namespace Assets._Data._Scripts.Save
{
    public class SaveGame
    {
        public List<SaveData> _listData;
        private SaveDataWrapper _saveDataWrapper;
        private static string file = "DataLevel.json";
        public void Save(List<SaveData> data)
        {
            _saveDataWrapper = new() { levelData = data.ToArray() };
            string dataJson = JsonUtility.ToJson(_saveDataWrapper);
            string path = Application.persistentDataPath + "/" + file;
            Debug.Log(path);
            //C:/Users/FPTSHOP/AppData/LocalLow/DefaultCompany/Pixel Adventure 1/DataLevel.json
            System.IO.File.WriteAllText(path, dataJson);
        }

        public List<SaveData> Load()
        {
            string path = Application.persistentDataPath + "/" + file;
            Debug.Log(path);
            if (File.Exists(path))
            {
                string dataJson = System.IO.File.ReadAllText(path);
                _saveDataWrapper = JsonUtility.FromJson<SaveDataWrapper>(dataJson);
                return _saveDataWrapper.levelData.ToList();
            }
            else
            {
                Debug.Log("File not found!");
            }
            return null;
        }
    }

    [System.Serializable]
    public class SaveDataWrapper
    {
        public SaveData[] levelData;
    }
}