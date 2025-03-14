using Assets._Data._Scripts.Save;
using UnityEngine;

public class BtnMainMenu : MonoBehaviour
{
    public GameData _gameData;
    public GameObject _panelSetting;
    private void Start()
    {
        _panelSetting = GameObject.Find("PanelGameSetting");

        if (_panelSetting != null)
        {
            _panelSetting.SetActive(false);
        }
    }
    public void LoadMenuArea()
    {
        SaveGame saveGame = new();
        _gameData.saveData = saveGame.Load();
        SceneController.Instance.LoadScene("Menu Area");
    }

    public void Exit()
    {
        Application.Quit();
    }

    public void Setting()
    {
        _panelSetting.SetActive(true);
    }
    public void CloseSetting()
    {
        _panelSetting.SetActive(false);
    }

    public void Charater()
    {

    }
}
