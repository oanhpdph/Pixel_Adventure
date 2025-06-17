using UnityEngine;


public class BtnNextLevel : MonoBehaviour
{
    private void OnEnable()
    {
        if (LevelController.instance.currentLevel + 1 > LevelController.instance.saveData.Count)
        {
            gameObject.SetActive(false);
        }
    }
    public void NextLevel()
    {
        Time.timeScale = 1.0f;
        GameManager.Instance.CurrentState = GameState.Play;
        LevelController.instance.currentLevel++;
        string level = "Level " + (LevelController.instance.currentLevel);
        SceneController.Instance.LoadScene(level);
    }
}
