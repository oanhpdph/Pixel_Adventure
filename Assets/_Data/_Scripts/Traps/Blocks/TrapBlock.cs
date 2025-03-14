using Assets._Data._Scripts.Common;
using UnityEngine;

public class TrapBlock : Health
{
    [SerializeField] private GameObject[] spawnObject;
    [SerializeField] private float forceKnockback = 5f;

    private Vector2 directionKnockBack;
    private Destruction destruction;
    private AddForce addForce;

    private void Start()
    {
        destruction = GetComponent<Destruction>();
        addForce = new AddForce();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        directionKnockBack = (collision.transform.position - transform.position).normalized;
        if (directionKnockBack.y > 0.5f
            && directionKnockBack.x < 0.5f
            && directionKnockBack.x > -0.5f)
        {
            if (collision.CompareTag("Player"))
            {
                addForce.Force(collision.gameObject.GetComponent<Rigidbody2D>(), forceKnockback * directionKnockBack);
                BlockTakeDamamge(collision.GetComponent<Damage>().DamageDeal);
            }
        }
    }

    private void BlockTakeDamamge(float damage)
    {
        bool isBroken = base.TakeDamageHealth(damage);
        if (isBroken)
        {
            int index = Random.Range(0, spawnObject.Length - 1);
            Debug.Log(spawnObject[index]);
            GameObject _instantiateObjectHealth = destruction.InstantiateObject(spawnObject[index], transform.position, transform.parent);
            Vector3 position = _instantiateObjectHealth.transform.position;
            _instantiateObjectHealth.transform.position = position;
            destruction.ObjectBroken();
        }
        else
        {
            GetComponent<Animator>().SetTrigger("HitTopTrigger");
        }
    }

}
