using UnityEngine;


public class BtnRestartGame : MonoBehaviour
{
    public void RestartGame()
    {
        GameManager.Instance.CurrentState = GameState.Play;
        UnityEngine.SceneManagement.SceneManager.LoadScene(UnityEngine.SceneManagement.SceneManager.GetActiveScene().name);
    }
}