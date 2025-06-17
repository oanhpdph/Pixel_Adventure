using UnityEngine;

public class BtnQuit : MonoBehaviour
{

    public void Quit()
    {
        Time.timeScale = 1;
        UnityEngine.SceneManagement.SceneManager.LoadScene(SceneFlags.MAIN_SCREEN);
    }

}
