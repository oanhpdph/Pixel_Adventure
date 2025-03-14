
using Assets._Data._Scripts.Enemy;
using System.Collections;
using UnityEngine;
public class EnemyTurtle : Enemy
{
    [SerializeField] private float timeSpikeIn = 5f;
    [SerializeField] private float timeSpikeOut = 5f;
    private float _timeSpikeOut;
    private float _timeSpikeIn;
    private bool isSpikeIn = false;
    private bool flag = true;
    private Coroutine spikeOutCoroutine;
    private Animator animatorTurtle;
    [SerializeField] private EnemyHealth enemyHealth;

    private void Start()
    {
        _timeSpikeOut = timeSpikeOut;
        _timeSpikeIn = timeSpikeIn;
        animatorTurtle = GetComponent<Animator>();

    }
    public override void Update()
    {
        base.Update();
        NextState();
    }
    public override void Attack()
    {
        animatorTurtle.SetBool("isSpikeOut", false);
    }
    public override void Chase()
    {
        base.Chase();
    }

    public override void Patrol()
    {
        animatorTurtle.SetBool("isSpikeIn", false);
        animatorTurtle.SetBool("isHit", false);

    }
    public override void TakeDamage()
    {
        animatorTurtle.SetBool("isHit", true);
        base.currentState = EnemyState.Patrol;
    }

    private void NextState()
    {
        if (!isSpikeIn && flag)
        {
            flag = false;
            enemyHealth.enabled = false;
            StartCoroutine(SpikeIn());
        }

        if (isSpikeIn && !flag)
        {
            flag = true;
            enemyHealth.enabled = true;
            spikeOutCoroutine = StartCoroutine(SpikeOut());
        }
        if (enemyHealth.isTakeDamage)
        {
            base.currentState = EnemyState.TakeDamage;
            enemyHealth.isTakeDamage = false;
        }


    }

    private IEnumerator SpikeIn()
    {
        yield return new WaitForSeconds(_timeSpikeOut);

        isSpikeIn = true;
        this.gameObject.tag = "Enemy";
        animatorTurtle.SetBool("isSpikeIn", true);
        base.currentState = EnemyState.Patrol;
    }

    private IEnumerator SpikeOut()
    {
        yield return new WaitForSeconds(_timeSpikeIn);
        isSpikeIn = false;
        this.gameObject.tag = "Obstacle";
        animatorTurtle.SetBool("isSpikeOut", true);
        base.currentState = EnemyState.Attack;
    }

    private IEnumerator IsHit()
    {
        yield return new WaitForSeconds(animatorTurtle.GetCurrentAnimatorStateInfo(0).length);
        animatorTurtle.SetBool("isHit", false);

    }
}
