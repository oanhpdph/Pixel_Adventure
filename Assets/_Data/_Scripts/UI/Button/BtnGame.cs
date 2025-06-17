using UnityEngine;


public class BtnGame : MonoBehaviour
{
    public PanelManager panelManager;


    public void Close()
    {
        GameManager.Instance.stackGameState.Pop();
        GameManager.Instance.CurrentState = GameManager.Instance.GetTopStack();
        PlayerPrefs.Save();
    }

}