using Assets._Data._Scripts.Enemy;
using UnityEngine;

public class EnemyTruck : Enemy
{
    [Header("Info")]
    [SerializeField] private LayerMask playerLayer;
    [SerializeField] private float speedEnemy = 5f;
    [SerializeField] private float range = 5f;
    [SerializeField] private float distancePatrol = 5f;
    [SerializeField] private bool isRight = false;

    [Header("Bullet")]
    [SerializeField] private GameObject gun;
    [SerializeField] private GameObject truckBullet;
    [SerializeField] private float timeDelay = 1f;
    [SerializeField] private float timeDestroyBullet = 1f;
    [SerializeField] private float speedBullet = 10f;



    private Vector2[] arrDirection = new Vector2[2];
    private float _timeDelay;
    private Vector2 targetDirection;
    private Vector2 initPosition;
    private Vector2 targetPosition;


    private EnemyHealth enemyHealth;
    private Animator animatorTruck;

    private void Start()
    {
        _timeDelay = timeDelay;
        AddDirection();
        enemyHealth = GetComponent<EnemyHealth>();
        animatorTruck = GetComponent<Animator>();
        initPosition = transform.position;
        CalculateTargetPosition();
    }
    public override void Update()
    {
        base.Update();
        NextState();
    }
    public override void Attack()
    {

        if (_timeDelay <= 0f)
        {
            animatorTruck.SetTrigger("triggerAttack");
            animatorTruck.SetBool("isRunning", false);
            return;
        }
        animatorTruck.ResetTrigger("triggerAttack");

    }
    public override void Chase()
    {
        base.Chase();
    }
    public override void Patrol()
    {
        Moving();
    }

    public override void TakeDamage()
    {
        animatorTruck.SetBool("isRunning", false);

        animatorTruck.SetTrigger("triggerHit");

    }

    private void NextState()
    {
        _timeDelay -= Time.deltaTime;
        if (CheckTarget())
        {
            base.currentState = EnemyState.Attack;
            Vector2 localScale = new Vector3(-targetDirection.x, 1, 1);
            transform.localScale = localScale;
            isRight = (localScale.x == -1);
            CalculateTargetPosition();
        }
        else
        {
            base.currentState = EnemyState.Patrol;
        }
        if (enemyHealth.isTakeDamage)
        {
            enemyHealth.isTakeDamage = false;
            base.currentState = EnemyState.TakeDamage;

        }

    }

    private bool CheckTarget()
    {
        foreach (Vector2 direction in arrDirection)
        {
            RaycastHit2D raycastHit2D = Physics2D.Raycast(transform.position, direction, range, playerLayer);
            if (raycastHit2D.collider != null)
            {
                targetDirection = direction;
                return true;
            }
        }
        return false;
    }
    private void AddDirection()
    {
        arrDirection[0] = transform.right;
        arrDirection[1] = -transform.right;
    }

    private GameObject SpawnBullet()
    {
        return Instantiate(truckBullet, gun.transform.position, Quaternion.identity, transform.parent);
    }

    private void CalculateTargetPosition()
    {
        if (isRight)
        {
            targetPosition = initPosition + new Vector2(distancePatrol, 0);
        }
        else
        {
            targetPosition = initPosition + new Vector2(-distancePatrol, 0);

        }
        base.Flip(transform, targetPosition);

    }
    private void Moving()
    {
        transform.position = Vector2.MoveTowards(transform.position, targetPosition, speedEnemy * Time.deltaTime);
        animatorTruck.SetBool("isRunning", true);
        if (Vector2.Distance(transform.position, targetPosition) < 0.2f)
        {
            isRight = !isRight;
            CalculateTargetPosition();
        }
    }
    public void Fire()
    {
        _timeDelay = timeDelay;
        GameObject bullet = SpawnBullet();
        bullet.GetComponent<EnemyBullet>().DestroyBullet(timeDestroyBullet);
        bullet.transform.localScale = transform.localScale;
        bullet.GetComponent<Rigidbody2D>().AddForce(targetDirection * speedBullet, ForceMode2D.Impulse);
    }
}
