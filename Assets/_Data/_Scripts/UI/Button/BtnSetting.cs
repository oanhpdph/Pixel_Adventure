using UnityEngine;


public class BtnSetting : MonoBehaviour
{
    public void Setting()
    {
        GameManager.Instance.stackGameState.Push(GameState.Setting);
        GameManager.Instance.CurrentState = GameState.Setting;
    }
}
