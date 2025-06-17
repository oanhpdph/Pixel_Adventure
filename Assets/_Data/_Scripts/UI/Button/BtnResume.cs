using UnityEngine;


public class BtnResume : MonoBehaviour
{

    public void Resume()
    {
        GameManager.Instance.CurrentState = GameState.Play;
    }

}
