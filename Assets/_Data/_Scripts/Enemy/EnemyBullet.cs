using Assets._Data._Scripts.Common;
using UnityEngine;

namespace Assets._Data._Scripts.Enemy
{
    public class EnemyBullet : MonoBehaviour
    {
        private Destruction destruction;
        private void Start()
        {
            destruction = GetComponent<Destruction>();
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            destruction.ObjectBroken();
        }

        public void DestroyBullet(float time)
        {
            if (this != null)
            {
                Destroy(gameObject, time);
            }
        }
    }
}