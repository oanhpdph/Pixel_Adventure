using System.Collections;
using UnityEngine;

public class TrapFire : MonoBehaviour
{
    [SerializeField] private float _timeDelay;
    [SerializeField] private float _timeActive;


    private Animator _fireAnimator;
    private bool _isActive = false;

    private void Start()
    {
        Init();
    }

    private void Init()
    {
        _fireAnimator = GetComponent<Animator>();

    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            if (_isActive)
            {
                return;
            }
            StartCoroutine(Delay());
        }
    }

    private IEnumerator Delay()
    {
        _isActive = true;
        _fireAnimator.SetTrigger("Hit");
        yield return new WaitForSeconds(_timeDelay);
        StartCoroutine(FireOn());
    }

    private IEnumerator FireOn()
    {
        FlameIsActive(true);
        yield return new WaitForSeconds(_timeActive);
        FlameIsActive(false);
        _isActive = false;

    }

    private void FlameIsActive(bool isActive)
    {
        _fireAnimator.SetBool("On", isActive);
        transform.Find("Flame").GetComponent<Collider2D>().enabled = isActive;
    }
}
