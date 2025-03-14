using Assets._Data._Scripts.Enemy;
using UnityEngine;

public class BlueBird : Enemy
{

    [SerializeField] private float distancePatrol = 5f;
    [SerializeField] private float speed = 5f;

    private EnemyHealth enemyHealth;
    private Animator animator;

    private Vector2 initPosition;
    private Vector2 targetPosition;

    private bool isRight = false;
    private void Start()
    {
        initPosition = transform.position;
        animator = GetComponent<Animator>();
        enemyHealth = GetComponent<EnemyHealth>();
        CalculateTargetPosition();
    }
    public override void Update()
    {
        base.Update();
        NextState();
    }
    public override void Patrol()
    {
        Moving();
        animator.SetBool("isHit", false);
    }
    public override void TakeDamage()
    {
        animator.SetBool("isHit", true);
    }
    private void NextState()
    {
        base.currentState = EnemyState.Patrol;

        if (enemyHealth.isTakeDamage)
        {
            enemyHealth.isTakeDamage = false;
            base.currentState = EnemyState.TakeDamage;
        }
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
        transform.position = Vector2.MoveTowards(transform.position, targetPosition, speed * Time.deltaTime);
        if (Vector2.Distance(transform.position, targetPosition) < 0.2f)
        {
            isRight = !isRight;
            CalculateTargetPosition();
        }
    }
}
