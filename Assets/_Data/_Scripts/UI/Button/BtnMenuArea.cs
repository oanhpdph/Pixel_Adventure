using Assets._Data._Scripts.Save;
using UnityEngine;

public class BtnMenuArea : MonoBehaviour
{
    public void SelectArea(GameObject area)
    {
        string areaName = area.name + " Level";
        SceneController.Instance.LoadScene(areaName);
        SaveGame saveGame = new();
        saveGame.Load();
    }
}


