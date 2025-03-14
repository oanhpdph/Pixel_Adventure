using UnityEngine;

namespace Assets._Data._Scripts.Enemy
{
    public enum EnemyState
    {
        Patrol,
        Attack,
        Chase,
        TakeDamage
    }
    public class Enemy : MonoBehaviour
    {
        public EnemyState currentState = EnemyState.Patrol;

        public virtual void Update()
        {
            State();
        }
        public void State()
        {
            switch (currentState)
            {
                case EnemyState.Patrol:
                    Patrol();
                    break;
                case EnemyState.Attack:
                    Attack();
                    break;
                case EnemyState.Chase:
                    Chase();
                    break;
                case EnemyState.TakeDamage:
                    TakeDamage();
                    break;

                default:
                    break;

            }
        }

        public virtual void Patrol()
        {

        }
        public virtual void Attack()
        {

        }
        public virtual void Chase()
        {

        }
        public virtual void TakeDamage()
        {

        }
        public void Flip(Transform enemy, Vector3 targetPosition)
        {
            enemy.localScale = new Vector3(Mathf.Sign(enemy.position.x - targetPosition.x), 1, 1);

        }
    }
}