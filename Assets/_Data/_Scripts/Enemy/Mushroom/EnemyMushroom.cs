

using Assets._Data._Scripts.Enemy;

using UnityEngine;
public class EnemyMushroom : Enemy
{

    [SerializeField] private float mushroomSpeed;
    [SerializeField] private float distancePatrol = 5f;
    [SerializeField] private bool isRight = false;

    private EnemyHealth enemyHealth;

    private Animator animator;
    private float speedMultiplier = 1f;
    private Vector3 initPosition = Vector2.zero;
    private Vector3 targetPosition = Vector2.zero;


    private void Start()
    {
        animator = GetComponent<Animator>();
        initPosition = transform.position;
        targetPosition = initPosition;
        enemyHealth = GetComponent<EnemyHealth>();
        CalculateTargetPosition();
    }

    public override void Update()
    {
        base.Update();
        NextState();
    }

    public override void Attack()
    {
        base.Attack();
    }

    public override void Chase()
    {
        base.Chase();
    }


    public override void Patrol()
    {

        Moving();
        CheckTargetPosition();
    }

    private void CalculateTargetPosition()
    {
        if (isRight)
        {
            targetPosition = new Vector2(initPosition.x + distancePatrol, initPosition.y);
        }
        else
        {
            targetPosition = new Vector2(initPosition.x - distancePatrol, initPosition.y);
        }
    }
    private void CheckTargetPosition()
    {
        if (Vector2.Distance(transform.position, targetPosition) < 0.2f)
        {
            isRight = !isRight;
            CalculateTargetPosition();
            base.Flip(transform, targetPosition);
        }
    }
    private void Moving()
    {
        float step = mushroomSpeed * Time.deltaTime * speedMultiplier;

        transform.position = Vector2.MoveTowards(transform.position, targetPosition, step);
        animator.SetBool("isMoving", true);
    }
    public override void TakeDamage()
    {
        animator.SetTrigger("triggerHit");
    }

    private void NextState()
    {

        if (enemyHealth.isTakeDamage)
        {
            base.currentState = EnemyState.TakeDamage;
            enemyHealth.isTakeDamage = false;
            return;
        }

        if (enemyHealth.currentHealth > 0)
        {
            base.currentState = EnemyState.Patrol;
            return;
        }
    }
}
