using UnityEngine;

namespace Assets._Data._Scripts.Camera
{
    public class CameraBounding : MonoBehaviour
    {

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.CompareTag("Player"))
            {
                CameraManager.Instance.ChangeBoundingShape(this.GetComponent<Collider2D>());
            }
        }
        private void OnTriggerExit2D(Collider2D collision)
        {
            if (collision.CompareTag("Player"))
            {
                CameraManager.Instance.ChangeBoundingShape(CameraManager.Instance.lastCollider);
            }
        }
    }
}