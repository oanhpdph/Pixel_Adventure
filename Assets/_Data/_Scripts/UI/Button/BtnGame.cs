using UnityEngine;

namespace Assets._Data._Scripts.UI.Button
{
    public class BtnGame : MonoBehaviour
    {
        private GameObject _panelSetting;

        private GameObject _panelPause;


        private void Start()
        {
            Init();
        }
        public void Init()
        {

            _panelSetting = GameObject.Find("PanelGameSetting");

            if (_panelSetting != null)
            {
                _panelSetting.SetActive(false);
            }
            _panelPause = GameObject.Find("PanelGamePause");

            if (_panelPause != null)
            {

                _panelPause.SetActive(false);
            }

        }
        public void Quit()
        {
            Time.timeScale = 1;
            UnityEngine.SceneManagement.SceneManager.LoadScene("Main Menu");
        }
        public void RestartGame()
        {
            Time.timeScale = 1;
            UnityEngine.SceneManagement.SceneManager.LoadScene(UnityEngine.SceneManagement.SceneManager.GetActiveScene().name);
        }

        public void PauseGame()
        {
            _panelPause.SetActive(true);
            Time.timeScale = 0;
        }

        public void Resume()
        {
            Time.timeScale = 1;
            _panelPause.SetActive(false);
        }
        public void NextLevel()
        {
            string level = "Level " + (LevelController.instance.currentLevel + 1);

            SceneController.Instance.LoadScene(level);
        }
        public void Setting()
        {
            _panelSetting.SetActive(true);
            _panelPause.SetActive(false);
            Time.timeScale = 0;
        }

        public void Close()
        {
            _panelSetting.SetActive(false);
            Time.timeScale = 1;
            PlayerPrefs.Save();
        }
    }
}