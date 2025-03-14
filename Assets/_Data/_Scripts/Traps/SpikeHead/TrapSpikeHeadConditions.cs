using System.Collections;
using UnityEngine;
public class TrapSpikeHeadConditions : TrapSpikeHead
{
    [SerializeField] private int[] conditions;// 0 right, 1 left, 2 up, 3 down
    [SerializeField] private LayerMask playerLayer;
    [SerializeField] private int range;
    private bool isAttack = false;
    private bool isReturn = false;

    private Vector3 initPosition;

    private void Awake()
    {
        initPosition = transform.position;
    }
    public override void Update()
    {
        Conditions();
        if (isAttack)
        {
            base.Moving();
        }
        Return();
    }
    private void Conditions()
    {
        if (!isAttack)
        {
            for (int i = 0; i < conditions.Length; i++)
            {
                RaycastHit2D raycastHit2D = Physics2D.Raycast(transform.position, base.direction[conditions[i]], range, playerLayer);
                if (raycastHit2D.collider != null)
                {
                    isAttack = true;
                    base.directionTarget = base.direction[conditions[i]];
                }
            }
        }
    }
    public override IEnumerator Stop()
    {
        base.speedMultiplier = 0;
        base.EffectCollider();
        transform.Translate(0.2f * -base.directionTarget);
        yield return new WaitForSeconds(5f);
        //return to init position
        base.speedMultiplier = 1;
        isAttack = false;
        if (Vector3.Distance(transform.position, initPosition) > 0.2f)
        {
            isReturn = true;
        }
    }
    private void Return()
    {
        if (isReturn)
        {
            float step = base.speed * Time.deltaTime;
            transform.position = Vector3.MoveTowards(transform.position, initPosition, step);
            if (Vector3.Distance(transform.position, initPosition) < 0.2f)
            {
                isReturn = false;
            }
        }
    }
}
