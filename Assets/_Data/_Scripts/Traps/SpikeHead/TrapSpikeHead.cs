using System.Collections;
using UnityEngine;

public class TrapSpikeHead : MonoBehaviour
{
    [Header("Particle")]
    [SerializeField] private ParticleSystem topParticle;
    [SerializeField] private ParticleSystem rightParticle;
    [SerializeField] private ParticleSystem bottomParticle;
    [SerializeField] private ParticleSystem leftParticle;

    [SerializeField] private int[] ways;
    [SerializeField] protected float speed = 5f;
    private int currentIndex = 0;
    protected int speedMultiplier = 1;
    private float speedReal = 0;
    protected Vector3[] direction = new Vector3[4];
    protected Vector2 directionTarget;

    private Animator spikeHeadAnimator;
    private void Start()
    {
        spikeHeadAnimator = GetComponent<Animator>();
        StartCoroutine(DelayInit());
    }
    private IEnumerator DelayInit()
    {
        speedReal = 0f;
        yield return new WaitForSeconds(0.5f);
        speedReal = speed;
        CalculateDirection();
        DirectionTarget();
    }
    public virtual void Update()
    {
        Moving();
    }

    public void Moving()
    {
        transform.Translate(Time.deltaTime * speedReal * speedMultiplier * directionTarget);
    }

    protected void EffectCollider()
    {
        if (directionTarget.y == -1)
        {
            bottomParticle.Play();
            spikeHeadAnimator.SetTrigger("BottomHitTrigger");
            return;
        }
        if (directionTarget.y == 1)
        {
            topParticle.Play();
            spikeHeadAnimator.SetTrigger("TopHitTrigger");
            return;
        }
        if (directionTarget.x == -1)
        {
            leftParticle.Play();
            spikeHeadAnimator.SetTrigger("LeftHitTrigger");
            return;
        }
        if (directionTarget.x == 1)
        {
            rightParticle.Play();
            spikeHeadAnimator.SetTrigger("RightHitTrigger");
            return;
        }
    }

    private void DirectionTarget()
    {
        int index = ways[currentIndex];
        directionTarget = direction[index];
        currentIndex++;
        if (currentIndex == ways.Length)
        {
            currentIndex = 0;
        }
    }

    private void CalculateDirection()
    {
        direction[0] = transform.right;// 0 is right
        direction[1] = -transform.right;//1 is left
        direction[2] = transform.up;// 2 is up
        direction[3] = -transform.up;//3 is down
    }
    public virtual IEnumerator Stop()
    {
        speedMultiplier = 0;
        EffectCollider();
        transform.Translate(0.2f * -directionTarget);
        yield return new WaitForSeconds(2f);
        speedMultiplier = 1;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            StartCoroutine(Stop());
            DirectionTarget();
        }
    }

}
