using System.IO;
using UnityEngine;

public class SaveController : ISaveGame
{
    public T Load<T>(string fileName)
    {
        string path = Path.Combine(Application.persistentDataPath, fileName);

        if (File.Exists(path))
        {
            string jsonData = System.IO.File.ReadAllText(path);
            Debug.Log(jsonData);
            return JsonUtility.FromJson<T>(jsonData);
        }
        else
        {
            Debug.Log("File not found!");
        }
        return default;
    }

    public void Save<T>(object data, string fileName)
    {
        string jsonData = JsonUtility.ToJson(data);
        string path = Path.Combine(Application.persistentDataPath, fileName);
        System.IO.File.WriteAllText(path, jsonData);
    }
}
