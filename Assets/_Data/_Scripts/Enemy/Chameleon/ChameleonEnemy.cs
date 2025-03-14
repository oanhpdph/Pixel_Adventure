using Assets._Data._Scripts.Enemy;
using UnityEngine;

public class ChameleonEnemy : Enemy
{
    [SerializeField] private LayerMask playerLayer;
    [SerializeField] private float range = 3f;
    [SerializeField] private float distancePatrol = 3f;
    [SerializeField] private float chameleonSpeed = 5f;
    [SerializeField] private bool isRight = false;
    [SerializeField] private float cooldownAttack = 2f;

    private Vector2[] arrDirection = new Vector2[2];
    private Vector2 targetDirection;
    private Vector2 targetPosition;
    private Vector2 initPosition;
    private Animator animatorChemeleon;
    private EnemyHealth enemyHealth;
    private float _cooldownAttack;

    private void Start()
    {
        animatorChemeleon = GetComponent<Animator>();
        enemyHealth = GetComponent<EnemyHealth>();
        initPosition = transform.position;
        AddDirection();
        CalculateTargetPosition();
        _cooldownAttack = cooldownAttack;
    }
    public override void Update()
    {
        base.Update();
        NextState();
    }

    public override void Attack()
    {

        if (_cooldownAttack < 0)
        {
            animatorChemeleon.SetBool("isAttack", true);
            _cooldownAttack = cooldownAttack;
            return;
        }
        animatorChemeleon.SetBool("isAttack", false);

    }
    public override void Patrol()
    {
        Moving();

        animatorChemeleon.SetBool("isAttack", false);
    }
    public override void TakeDamage()
    {
        animatorChemeleon.SetTrigger("triggerHit");
    }

    private void NextState()
    {
        _cooldownAttack -= Time.deltaTime;
        if (CheckTarget())
        {
            base.currentState = EnemyState.Attack;
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
        Vector2 direction = new(-transform.localScale.x, 0);
        RaycastHit2D raycastHit2D = Physics2D.Raycast(transform.position, direction, range, playerLayer);
        if (raycastHit2D.collider != null)
        {
            targetDirection = direction;
            return true;
        }
        return false;

    }
    private void AddDirection()
    {
        arrDirection[0] = transform.right;
        arrDirection[1] = -transform.right;
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
        transform.position = Vector2.MoveTowards(transform.position, targetPosition, chameleonSpeed * Time.deltaTime);
        animatorChemeleon.SetBool("isRunning", true);
        if (Vector2.Distance(transform.position, targetPosition) < 0.2f)
        {
            isRight = !isRight;
            CalculateTargetPosition();
        }
    }
}
