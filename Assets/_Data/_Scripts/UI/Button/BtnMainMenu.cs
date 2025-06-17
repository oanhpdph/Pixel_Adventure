using UnityEngine;

public class BtnMainMenu : MonoBehaviour
{
    public GameObject panelSetting;
    private void Start()
    {
        panelSetting = GameObject.Find("PanelGameSetting");

        if (panelSetting != null)
        {
            panelSetting.SetActive(false);
        }
    }
    public void LoadMenuArea()
    {
        SceneController.Instance.LoadScene(SceneFlags.LEVEL_SCREEN);
    }

    public void Exit()
    {
        Application.Quit();
    }

    public void Setting()
    {
        panelSetting.SetActive(true);
    }
    public void CloseSetting()
    {
        panelSetting.SetActive(false);
    }


}
