using UnityEngine;

public class TrapTrampoline : MonoBehaviour
{
    [SerializeField] private int force = 15;

    private bool isActive = false;
    private GameObject playerObject;


    private void Update()
    {
        if (isActive)
        {
            GetComponent<Animator>().SetTrigger("triggerActive");
            isActive = false;
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            isActive = true;
            playerObject = collision.gameObject;
            Debug.Log(isActive);
        }
    }
    public void AddForce()
    {
        playerObject.GetComponent<Rigidbody2D>().AddForce(Vector2.up * force, ForceMode2D.Impulse);
    }
}
