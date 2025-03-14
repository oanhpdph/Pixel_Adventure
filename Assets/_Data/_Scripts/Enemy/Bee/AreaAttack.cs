using UnityEngine;

namespace Assets._Data._Scripts.Enemy.Bee
{
    public class AreaAttack : MonoBehaviour
    {
        public GameObject playerObject;
        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.CompareTag("Player"))
            {
                playerObject = collision.gameObject;
            }
        }

        private void OnTriggerExit2D(Collider2D collision)
        {
            if (collision.CompareTag("Player"))
            {
                playerObject = null;
            }
        }
    }
}