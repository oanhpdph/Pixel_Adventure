using Assets._Data._Scripts.Enemy;
using Assets._Data._Scripts.Enemy.Bee;
using System.Collections;
using UnityEngine;
public class EnemyRino : Enemy
{
    [SerializeField] private AreaAttack areaAttack;
    [SerializeField] private float speed;
    [SerializeField] private float timeDelayPatrol = 3f;

    [SerializeField] private GameObject checkWall;
    [SerializeField] private LayerMask wallLayer;

    private Animator rinoAnimator;
    private Rigidbody2D rinoRigidBody;
    private GhostEffect ghostEffect;
    private EnemyHealth enemyHealth;



    private Vector2 direction = Vector2.zero;
    private float timeDelay = 3f;
    private bool isAttack = false;
    private bool isPatrol = false;
    private Coroutine delayPatrol;
    private Vector2 initPosition;
    private void Start()
    {
        rinoAnimator = GetComponent<Animator>();
        rinoRigidBody = GetComponent<Rigidbody2D>();
        ghostEffect = GetComponent<GhostEffect>();
        enemyHealth = GetComponent<EnemyHealth>();
        initPosition = transform.position;
    }
    public override void Update()
    {
        base.Update();
        NextState();
    }

    public override void Patrol()
    {
        transform.position = Vector2.MoveTowards(transform.position, initPosition, speed / 3 * Time.deltaTime);
        if (Vector2.Distance(transform.position, initPosition) < 0.2f)
        {
            rinoAnimator.SetBool("isRunning", false);
            isPatrol = true;
        }
    }
    public override void Attack()
    {
        Runing();
    }
    public override void Chase()
    {

    }
    public override void TakeDamage()
    {
    }

    private void Runing()
    {
        Vector2 newPosition = rinoRigidBody.position + speed * Time.deltaTime * direction;
        rinoRigidBody.MovePosition(newPosition);
        rinoAnimator.SetBool("isRunning", true);
        ghostEffect.makeGhost = true;

    }
    private void NextState()
    {
        if (enemyHealth.currentHealth > 0)
        {
            NextStateAttack();
        }
        NextStateTakeDamage();

    }
    private void CalculateDirection()
    {
        if (PlayerController.Instance.GetPosition().x > transform.position.x)
        {
            direction = transform.right;
        }
        else
        {
            direction = -transform.right;
        }
    }
    private bool HitWall()
    {
        return Physics2D.OverlapBox(checkWall.transform.position, new Vector2(0.5f, 0.01f), 0, wallLayer);
    }

    private IEnumerator DelayPatrol()
    {
        yield return new WaitForSeconds(timeDelayPatrol);
        base.currentState = EnemyState.Patrol;

        Vector2 localScale = transform.localScale;
        localScale.x = direction.x;
        transform.localScale = localScale;
    }
    private void NextStateAttack()
    {
        if (!isAttack)
        {
            timeDelay -= Time.deltaTime;
            if (areaAttack.playerObject != null && timeDelay < 0)
            {
                base.currentState = EnemyState.Attack;

                timeDelay = 3f;
                isAttack = true;
                isPatrol = false;
                CalculateDirection();
                if (delayPatrol != null)
                {
                    StopCoroutine(delayPatrol);
                }
                base.Flip(transform, PlayerController.Instance.GetPosition());
                return;
            }
            NextStatePatrol();
        }
    }
    private void NextStatePatrol()
    {
        if (areaAttack.playerObject == null && !isPatrol)
        {
            GetComponent<GhostEffect>().makeGhost = false;

            isPatrol = true;
            delayPatrol = StartCoroutine(DelayPatrol());
        }
    }
    private void NextStateTakeDamage()
    {

        if (HitWall() && isAttack)
        {
            isAttack = false;
            base.currentState = EnemyState.TakeDamage;
            rinoAnimator.SetBool("isHitWall", true);
            rinoAnimator.SetBool("isRunning", false);
            return;
        }
        else
        {
            ghostEffect.makeGhost = false;
            rinoAnimator.SetBool("isHitWall", false);
        }

        if (enemyHealth.isTakeDamage)
        {
            isAttack = false;

            enemyHealth.isTakeDamage = false;
            rinoAnimator.SetTrigger("triggerHit");
            rinoAnimator.SetBool("isRunning", false);

            base.currentState = EnemyState.TakeDamage;
        }
    }
}
