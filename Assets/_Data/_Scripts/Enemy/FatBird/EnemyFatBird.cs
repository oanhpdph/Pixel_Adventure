using Assets._Data._Scripts.Enemy;
using System.Collections;
using UnityEngine;

public class EnemyFatBird : Enemy
{
    [SerializeField] private float distance;
    [SerializeField] private float speed;


    [SerializeField] private LayerMask playerLayer;
    [SerializeField] private ParticleSystem fallParticle;

    private Vector2 initPosition;
    private bool isAttack = false;
    private bool isReset = false;
    private Animator animatorFatBird;
    private EnemyHealth enemyHealth;
    private void Start()
    {
        enemyHealth = GetComponent<EnemyHealth>();
        initPosition = transform.position;
        animatorFatBird = GetComponent<Animator>();
    }

    public override void Update()
    {
        base.Update();
        NextState();
        ResetPosition();
    }
    public override void Attack()
    {
        Moving();
    }

    public override void Chase()
    {
        base.Chase();
    }

    public override void Patrol()
    {

    }
    public override void TakeDamage()
    {
        animatorFatBird.SetTrigger("triggerTakeDamage");
    }
    private void NextState()
    {
        if (Condition() && !isReset)
        {
            animatorFatBird.SetBool("isFalling", true);
            isAttack = true;
            base.currentState = EnemyState.Attack;
        }
        if (!isAttack)
            base.currentState = EnemyState.Patrol;

        if (enemyHealth.isTakeDamage)
        {
            base.currentState = EnemyState.TakeDamage;

        }

    }

    private bool Condition()
    {
        RaycastHit2D raycastHit2D = Physics2D.Raycast(transform.position, Vector2.down, distance, playerLayer);
        if (raycastHit2D.collider)
        {
            return true;
        }
        return false;
    }
    private void Moving()
    {
        if (isAttack)
        {
            transform.Translate(speed * Time.deltaTime * Vector2.down);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            if (isAttack == true)
            {
                fallParticle.Play();
            }
            isAttack = false;
            animatorFatBird.SetTrigger("triggerGround");
            animatorFatBird.SetBool("isFalling", false);
            StartCoroutine(TimeDelayReset());
        }
    }

    private IEnumerator TimeDelayReset()
    {
        yield return new WaitForSeconds(3f);
        isReset = true;
    }

    private void ResetPosition()
    {
        if (isReset)
        {
            transform.position = Vector2.MoveTowards(transform.position, initPosition, speed / 2 * Time.deltaTime);
            if (Vector2.Distance(transform.position, initPosition) < 0.2f)
            {
                isReset = false;
            }
        }

    }
}
