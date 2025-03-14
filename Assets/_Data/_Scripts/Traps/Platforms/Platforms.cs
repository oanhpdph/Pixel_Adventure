using UnityEngine;

namespace Assets._Scripts.Platforms
{
    public class Platforms : MonoBehaviour
    {
        public virtual void OnTriggerEnter2D(Collider2D collision)
        {
            string collisionTag = collision.tag;
            if (collisionTag.CompareTo("player") == 0)
            {
                collision.transform.SetParent(this.transform);
            }
        }
        private void OnTriggerExit2D(Collider2D collision)
        {
            string collisionTag = collision.tag;
            if (collisionTag.CompareTo("player") == 0)
            {
                collision.transform.SetParent(null);
            }
        }
    }
}