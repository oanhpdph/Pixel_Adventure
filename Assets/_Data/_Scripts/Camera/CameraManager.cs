using Cinemachine;
using UnityEngine;

public class CameraManager : MonoBehaviour
{
    private static CameraManager instance { get; set; }
    public static CameraManager Instance => instance;
    public Collider2D lastCollider;
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }
    public void ChangeBoundingShape(Collider2D collider2D)
    {
        if (lastCollider == null || lastCollider != collider2D)
        {
            lastCollider = collider2D;
        }

        GetComponent<CinemachineConfiner2D>().m_BoundingShape2D = collider2D;
    }
}
