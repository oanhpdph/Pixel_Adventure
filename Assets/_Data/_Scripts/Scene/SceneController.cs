using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneController : MonoBehaviour
{
    private static string _previousScene;
    private static SceneController instance { get; set; }
    public static SceneController Instance => instance;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        DontDestroyOnLoad(instance);
    }
    public void LoadScene(string nameScene)
    {
        _previousScene = SceneManager.GetActiveScene().name;
        SceneManager.LoadScene(nameScene);
    }

    public void LoadPreviousScene()
    {
        if (!string.IsNullOrEmpty(_previousScene))
        {
            SceneManager.LoadScene(_previousScene);
        }
    }
}
