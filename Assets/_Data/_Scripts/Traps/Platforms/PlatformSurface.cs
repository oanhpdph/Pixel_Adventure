using UnityEngine;

public class PlatformSurface : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            Rigidbody2D rigidbody2D = collision.GetComponent<Rigidbody2D>();
            rigidbody2D.velocity = Vector3.zero;
            rigidbody2D.AddForce(Vector2.up * 10, ForceMode2D.Impulse);
        }
    }

}
