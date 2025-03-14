using Assets._Data._Scripts.Enemy;
using Assets._Data._Scripts.Enemy.Bee;
using UnityEngine;
public class EnemyBee : Enemy
{
    [SerializeField] private GameObject pointSpawnBullet;
    [SerializeField] private GameObject bulletObject;
    [SerializeField] private GameObject shootingArea;
    [SerializeField] private AreaAttack areaAttackScript;

    [Header("Bullet")]
    [SerializeField] private float shootCooldown;
    [SerializeField] private float bulletSpeed = 3f;
    [SerializeField] private float timeDestroy = 3f;


    private float runTimeCooldown;
    private float angle;
    private GameObject playerObject;
    private Animator animatorBee;
    private EnemyHealth enemyHealth;
    private void Start()
    {
        enemyHealth = GetComponent<EnemyHealth>();
        animatorBee = GetComponent<Animator>();
        areaAttackScript = shootingArea.GetComponent<AreaAttack>();
        runTimeCooldown = shootCooldown;
    }
    public override void Update()
    {
        base.Update();
        NextState();
    }

    public override void Patrol()
    {
        transform.rotation = Quaternion.Euler(0, 0, 0);

    }
    public override void Attack()
    {
        animatorBee.SetTrigger("triggerAttack");

    }

    public override void Chase()
    {
        Vector3 distancePlayer = pointSpawnBullet.transform.position - playerObject.transform.position;
        angle = Vector2.SignedAngle(Vector2.up, distancePlayer);

        transform.rotation = Quaternion.Euler(0, 0, angle);
    }

    public override void TakeDamage()
    {
        animatorBee.SetTrigger("hit");
    }

    public void Fire()
    {
        GameObject bullet = SpawnBullet();

        bullet.GetComponent<EnemyBullet>().DestroyBullet(timeDestroy);

        Vector2 direction = (playerObject.transform.position - pointSpawnBullet.transform.position).normalized;
        bullet.GetComponent<Rigidbody2D>().AddForce(direction * bulletSpeed, ForceMode2D.Impulse);
    }
    private void NextState()
    {
        playerObject = areaAttackScript.playerObject;
        if (enemyHealth.isTakeDamage)
        {
            base.currentState = EnemyState.TakeDamage;
            enemyHealth.isTakeDamage = false;
            return;
        }
        if (enemyHealth.currentHealth > 0)
        {
            if (playerObject == null)
            {
                base.currentState = EnemyState.Patrol;
                return;
            }
            else
            {
                base.currentState = EnemyState.Chase;
                runTimeCooldown -= Time.deltaTime;
                if (runTimeCooldown > 0)
                {
                    return;
                }
            }
            if (runTimeCooldown < 0)
            {
                base.currentState = EnemyState.Attack;
                runTimeCooldown = shootCooldown;
                return;
            }

        }
    }

    private GameObject SpawnBullet()
    {
        return Instantiate(bulletObject, pointSpawnBullet.transform.position, transform.rotation, transform.parent);
    }
}
