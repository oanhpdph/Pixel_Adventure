using UnityEngine;
using UnityEngine.EventSystems;


public class ClosePanel : MonoBehaviour
{
    private void Update()
    {
        if (Input.GetMouseButtonDown(0) || Input.GetMouseButtonDown(1))
        {
            if (!EventSystem.current.IsPointerOverGameObject())
            {
                GameManager.Instance.stackGameState.Pop();
                GameManager.Instance.CurrentState = GameManager.Instance.GetTopStack();
            }
        }
    }
}
