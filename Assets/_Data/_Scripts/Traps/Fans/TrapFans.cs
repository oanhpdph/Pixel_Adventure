using Assets._Data._Scripts.Common;
using UnityEngine;

public class TrapFans : MonoBehaviour
{

    private PlayerController playerController;
    private bool _isBlow = false;
    private Vector2 _directionBlow = Vector2.zero;
    private AddForce _addForce;
    [SerializeField] private GameObject _directionObject;
    [SerializeField] private float _force;
    [SerializeField] private float _allowableError;

    private void Start()
    {
        playerController = PlayerController.Instance;
        _addForce = new AddForce();
    }
    private void FixedUpdate()
    {
        if (_isBlow)
        {
            float _forceBlow = Random.Range(_force, _force + _allowableError);
            Rigidbody2D rb = playerController.GetComponent<Rigidbody2D>();
            if (!playerController.onGround)
            {
                _addForce.Force(rb, _directionBlow * _forceBlow);
            }
            //if (rb.velocity.y > 0)
            //{
            //    playerController.GetComponent<Animator>().SetBool("isJumping", true);
            //}
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {

            _isBlow = false;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            _isBlow = true;
            _directionBlow = (_directionObject.transform.position - transform.position).normalized;
        }
    }

}
