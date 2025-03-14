using Assets._Data._Scripts.Common;
using UnityEngine;

public class EnemyHealth : Health
{
    public delegate void OnEnemyDeath();
    public event OnEnemyDeath EnemyDeathEvent;

    private Animator enemyAnimator;
    private Rigidbody2D enemyRigidBody2D;

    private AddForce addForce;

    public bool isTakeDamage = false;
    public float forceKnockBack = 25f;
    private void Start()
    {
        enemyAnimator = transform.GetComponent<Animator>();
        enemyRigidBody2D = transform.GetComponent<Rigidbody2D>();
        addForce = new AddForce();
    }

    public void EnemyTakeDamage(float damage)
    {
        isTakeDamage = true;
        bool isDie = base.TakeDamageHealth(damage);
        if (isDie)
        {
            Die();
            enemyRigidBody2D.bodyType = RigidbodyType2D.Dynamic;
            enemyRigidBody2D.gravityScale = 5;
            addForce.Force(enemyRigidBody2D, Vector2.up * 15);
        }
    }

    private void TimeDelayDestroy()
    {
        Destroy(this.gameObject, enemyAnimator.GetCurrentAnimatorStateInfo(0).length + 1f);
    }
    private void Die()
    {
        transform.GetComponent<Collider2D>().enabled = false;
        transform.rotation = Quaternion.Euler(0, 0, Random.Range(-40, 40));
        EnemyDeathEvent?.Invoke();
        TimeDelayDestroy();
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player") && this.enabled == true)
        {
            Vector2 direction = (collision.transform.position - transform.position).normalized;
            if (direction.y > 0.7f)
            {
                EnemyTakeDamage(collision.gameObject.GetComponent<Damage>().DamageDeal);
            }
            else
            {
                collision.gameObject.GetComponent<PlayerHealth>().PlayerTakeDamage(GetComponent<Damage>().DamageDeal);
            }
            collision.gameObject.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
            addForce.Force(collision.gameObject.GetComponent<Rigidbody2D>(), direction * forceKnockBack);
        }
    }

}
