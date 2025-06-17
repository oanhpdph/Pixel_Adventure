using UnityEngine;


public class BtnPause : MonoBehaviour
{
    public void PauseGame()
    {
        GameManager.Instance.stackGameState.Push(GameState.Pause);
        GameManager.Instance.CurrentState = GameState.Pause;
    }
}
