using UnityEngine;

public class BtnBack : MonoBehaviour
{
    public void GoBack()
    {
        SceneController.Instance.LoadPreviousScene();
    }
}
