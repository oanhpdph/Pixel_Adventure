using Assets._Data._Scripts.Enemy;
using System.Collections;
using UnityEngine;

public class EnemyAngryPig : Enemy
{
    [SerializeField] private float distancePatrol = 5f;
    [SerializeField] private float speedNomal = 3f;
    [SerializeField] private float speedRun = 5f;

    private float speedMultiplier = 1f;
    private float speed = 0f;
    private float timePatrol = 8f;
    private float coolDownChase = 4f;
    private float distanceChase = 8f;
    private bool isLeft = true;
    private bool isRuning = false;

    private Vector3 targetPosition;
    private Vector3 enemyInitPosition;
    private Vector3 playerPosition;

    private Animator animatorEnemy;
    private EnemyHealth health;
    private Coroutine waitTime;
    private void Start()
    {
        enemyInitPosition = transform.position;
        animatorEnemy = GetComponent<Animator>();
        health = transform.GetComponentInChildren<EnemyHealth>();

        CalculateTargetPosition();
    }

    public override void Update()
    {
        base.Update();
        playerPosition = PlayerController.Instance.GetPosition();
        NextState();
    }
    /// <summary>
    /// Patrol
    /// </summary>
    public override void Patrol()
    {
        base.Flip(transform, targetPosition);
        isRuning = false;
        if (timePatrol > 0)
        {
            timePatrol -= Time.deltaTime;
            Move();
        }
        else if (timePatrol < 0 && speedMultiplier == 1f)
        {
            waitTime = StartCoroutine(WaitTime(3f));
        }
        CheckDistance();

    }
    private void CalculateTargetPosition()
    {
        if (isLeft)
        {
            targetPosition = enemyInitPosition + new Vector3(distancePatrol, 0, 0);
        }
        else
        {
            targetPosition = enemyInitPosition - new Vector3(distancePatrol, 0, 0);
        }
    }


    private void CheckDistance()
    {
        if (Vector2.Distance(transform.position, targetPosition) < 0.2f)
        {
            isLeft = !isLeft;
            CalculateTargetPosition();
        }
    }
    private IEnumerator WaitTime(float time)
    {
        //isMove = false;
        speedMultiplier = 0f;
        animatorEnemy.SetBool("isMove", false);
        yield return new WaitForSeconds(time);
        speedMultiplier = 1f;
        TimePatrol();
    }
    private void TimePatrol()
    {
        timePatrol = Random.Range(5f, 8f);
        isLeft = Random.Range(0f, 1f) > 0.5f;
        CalculateTargetPosition();

    }
    /// <summary>
    /// chase
    /// </summary>

    public override void Chase()
    {
        CalculatePositionTargetChase();
        speedMultiplier = 1f;
        base.Flip(transform, targetPosition);
        Move();
        isRuning = true;

    }

    private void CalculatePositionTargetChase()
    {
        targetPosition.x = playerPosition.x;
        targetPosition.y = transform.position.y;
        targetPosition.z = transform.position.z;
    }
    /// <summary>
    /// Take dame
    /// </summary>
    public override void TakeDamage()
    {
        animatorEnemy.SetTrigger("hit");
        animatorEnemy.SetBool("isMove", false);
        animatorEnemy.SetBool("isRuning", false);

    }


    /// <summary>
    /// common
    /// </summary>
    private void NextState()
    {
        // current position - init position > distance patrol=> patrol
        // current position - player position < distance Chase => chase
        Vector3 currentPosition = transform.position;
        coolDownChase -= Time.deltaTime;
        if (Vector3.Distance(currentPosition, enemyInitPosition) > distancePatrol ||
            Vector3.Distance(currentPosition, playerPosition) > distanceChase)
        {
            speed = speedNomal;
            base.currentState = EnemyState.Patrol;
            return;
        }
        if (Vector3.Distance(currentPosition, playerPosition) < distanceChase &&
            coolDownChase < 0f &&
            Vector3.Distance(playerPosition, enemyInitPosition) < distancePatrol &&
            health.currentHealth > 0
            )
        {
            ChaseState();
            return;
        }

        if (health.isTakeDamage)
        {
            base.currentState = EnemyState.TakeDamage;
            health.isTakeDamage = false;
            return;
        }
    }
    private void ChaseState()
    {
        speed = speedRun;
        coolDownChase = 4f;
        TimePatrol();
        if (waitTime != null)
        {
            StopCoroutine(waitTime);
        }
        base.currentState = EnemyState.Chase;
    }

    private void Move()
    {
        float step = speed * Time.deltaTime * speedMultiplier;
        animatorEnemy.SetBool("isMove", speedMultiplier != 0);
        transform.position = Vector3.MoveTowards(transform.position, targetPosition, step);

        animatorEnemy.SetBool("isRuning", isRuning);
    }


}
