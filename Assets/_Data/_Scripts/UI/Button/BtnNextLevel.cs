using UnityEngine;


public class BtnNextLevel : MonoBehaviour
{
    private void OnEnable()
    {
        if (LevelController.Instance.currentLevel + 1 > LevelController.Instance.saveData.Count)
        {
            gameObject.SetActive(false);
        }
    }
    public void NextLevel()
    {
        Time.timeScale = 1.0f;
        GameManager.Instance.CurrentState = GameState.Play;
        LevelController.Instance.currentLevel++;
        string level = "Level " + (LevelController.Instance.currentLevel);
        SceneController.Instance.LoadScene(level);
    }
}
