using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;


public class GameStateManager : MonoBehaviour
{
    public PanelManager panelManager;

    private void Start()
    {
        panelManager = GetComponent<PanelManager>();
    }
    private void OnEnable()
    {
        GameManager.Instance.OnStateChange += ChangeState;

    }
    private void OnDisable()
    {
        GameManager.Instance.OnStateChange -= ChangeState;

    }
    public void ChangeState(GameState gameState)
    {
        Debug.Log("change state " + GameManager.Instance.CurrentState);
        switch (GameManager.Instance.CurrentState)
        {
            case GameState.Pause:
                StartCoroutine(ShowPausePanel());
                break;
            case GameState.GameFinish:
                StartCoroutine(ShowFinishPanel());
                break;
            case GameState.Setting:
                StartCoroutine(ShowSettingPanel());
                break;
            case GameState.GameOver:
                StartCoroutine(ShowGameOverPanel());
                break;
            case GameState.Menu:
                break;
            case GameState.Play:
                HidePanel();
                break;
        }
    }
    private IEnumerator ShowPausePanel()
    {
        yield return null; // Đợi 1 frame

        foreach (GameObject item in panelManager.listPanel)
        {
            if (item.name.Equals(PanelFlags.PAUSE_PANEL))
            {
                item.SetActive(true);
                LayoutRebuilder.ForceRebuildLayoutImmediate(item.GetComponent<RectTransform>());
            }
            else
            {
                item.SetActive(false);
            }
        }

        Time.timeScale = 0;
        EventSystem.current.SetSelectedGameObject(null);
    }
    private IEnumerator ShowFinishPanel()
    {
        yield return null; // Đợi 1 frame

        foreach (GameObject item in panelManager.listPanel)
        {
            if (item.name.Equals(PanelFlags.FINISH_PANEL))
            {
                item.SetActive(true);
                LayoutRebuilder.ForceRebuildLayoutImmediate(item.GetComponent<RectTransform>());
            }
            else
            {
                item.SetActive(false);
            }
        }

        Time.timeScale = 0;
        EventSystem.current.SetSelectedGameObject(null);
    }
    private void HidePanel()
    {
        Time.timeScale = 1.0f;
        foreach (GameObject item in panelManager.listPanel)
        {
            item.SetActive(false);
        }

        Debug.Log(Time.timeScale);
    }
    private IEnumerator ShowSettingPanel()
    {
        yield return null; // Đợi 1 frame

        foreach (GameObject item in panelManager.listPanel)
        {
            if (item.name.Equals(PanelFlags.SETTING_PANEL))
            {
                item.SetActive(true);
                LayoutRebuilder.ForceRebuildLayoutImmediate(item.GetComponent<RectTransform>());
            }
            else
            {
                item.SetActive(false);
            }
        }

        Time.timeScale = 0;
        EventSystem.current.SetSelectedGameObject(null);
    }
    private IEnumerator ShowGameOverPanel()
    {
        yield return null; // Đợi 1 frame

        foreach (GameObject item in panelManager.listPanel)
        {
            if (item.name.Equals(PanelFlags.GAMEOVER_PANEL))
            {
                item.SetActive(true);
                LayoutRebuilder.ForceRebuildLayoutImmediate(item.GetComponent<RectTransform>());
            }
            else
            {
                item.SetActive(false);
            }
        }

        Time.timeScale = 0;
        EventSystem.current.SetSelectedGameObject(null);
    }

}
