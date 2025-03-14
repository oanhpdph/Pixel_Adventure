using Assets._Scripts.Platforms;
using System.Collections;
using UnityEngine;

public class PlatformFalling : Platforms
{
    private Rigidbody2D rigidBody2DFallingPlatform;

    private Vector2 initPosition;
    private bool isFalled = false;
    private bool isFalling = false;

    private float speed = 5f;
    [SerializeField] private float timeReset = 3f;
    private void Start()
    {
        rigidBody2DFallingPlatform = GetComponent<Rigidbody2D>();
        initPosition = transform.position;
    }

    private void Update()
    {
        if (isFalled)
        {
            transform.position = Vector2.MoveTowards(transform.position, initPosition, speed * Time.deltaTime);
        }
        if (Vector2.Distance(transform.position, initPosition) < 0.2f)
        {
            isFalled = false;
        }
        if (isFalling)
        {
            transform.Translate(speed * 2 * Time.deltaTime * Vector2.down);
        }
    }
    public override void OnTriggerEnter2D(Collider2D collision)
    {
        string collisionName = collision.gameObject.name;
        if (collisionName.CompareTo("player") == 0)
        {
            collision.transform.SetParent(transform);
            StartCoroutine(Falling());
            StartCoroutine(ResetPosition());
        }
    }

    private IEnumerator ResetPosition()
    {
        yield return new WaitForSeconds(timeReset);
        isFalling = false;
        isFalled = true;
    }
    private IEnumerator Falling()
    {
        yield return new WaitForSecondsRealtime(0.1f);
        isFalling = true;
    }

}
