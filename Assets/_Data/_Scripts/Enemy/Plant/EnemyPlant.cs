using Assets._Data._Scripts.Enemy;
using UnityEngine;

public class EnemyPlant : Enemy
{
    [SerializeField] private GameObject pointSpawnBullet;
    [SerializeField] private GameObject plantBullet;
    [SerializeField] private float timeDetroyBullet = 2f;
    [SerializeField] private float speedBullet = 5f;
    [SerializeField] private float cooldown = 2f;


    private EnemyHealth health;
    private Vector2 normalizedShoot;
    private float _cooldown;
    private void Start()
    {
        _cooldown = cooldown;
        normalizedShoot = (pointSpawnBullet.transform.position - transform.position).normalized;
        health = GetComponent<EnemyHealth>();
    }

    public override void Update()
    {
        base.Update();
        NextState();
    }
    public override void Patrol()
    {
    }
    public override void Chase()
    {
        base.Chase();
    }

    public override void Attack()
    {
        _cooldown -= Time.deltaTime;
        if (_cooldown < 0)
        {
            GetComponent<Animator>().SetTrigger("triggerAttack");
            return;
        }
        GetComponent<Animator>().ResetTrigger("triggerAttack");

    }
    public override void TakeDamage()
    {
        GetComponent<Animator>().SetTrigger("triggerHit");
    }

    private void NextState()
    {
        if (health.isTakeDamage)
        {
            Debug.Log("takeDamage");
            base.currentState = EnemyState.TakeDamage;
            health.isTakeDamage = false;
            return;
        }
        if (base.currentState != EnemyState.Attack)
        {
            base.currentState = EnemyState.Attack;
        }
    }
    private GameObject SpawnBullet()
    {
        return Instantiate(plantBullet, pointSpawnBullet.transform.position, Quaternion.identity, transform.parent);
    }
    public void Fire()
    {
        GameObject bullet = SpawnBullet();
        bullet.GetComponent<EnemyBullet>().DestroyBullet(timeDetroyBullet);

        Vector2 directionShoot = new(Mathf.Sign(normalizedShoot.x), 0);
        bullet.GetComponent<Rigidbody2D>().AddForce(directionShoot * speedBullet, ForceMode2D.Impulse);
        _cooldown = cooldown;
    }
}
