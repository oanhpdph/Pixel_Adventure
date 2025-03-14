using UnityEngine;

public class Healing : MonoBehaviour
{
    [SerializeField]
    private float blood = 1;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            collision.gameObject.GetComponent<PlayerHealth>().AddHealth(blood);
            collision.gameObject.GetComponent<PlayerHealth>().LoadHealth();
            Destroy(this.gameObject);
        }
    }
}