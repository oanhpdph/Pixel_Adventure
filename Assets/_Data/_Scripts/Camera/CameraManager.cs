using Cinemachine;
using UnityEngine;

public class CameraManager : MonoBehaviour
{
    public GameObject player;
    private CinemachineVirtualCamera cinemachine;
    private void Awake()
    {
        cinemachine = GetComponent<CinemachineVirtualCamera>();

    }
    private void Start()
    {
        cinemachine.Follow = player.transform.GetChild(0);
    }

}
