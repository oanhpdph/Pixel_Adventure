using UnityEngine;

public class StartPoint : MonoBehaviour
{
    private static StartPoint instance { get; set; }
    public static StartPoint Instance => instance;

    private Animator animator;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }
    private void Start()
    {
        animator = GetComponent<Animator>();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            animator.SetBool("IsActive", true);
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            animator.SetBool("IsActive", false);
        }
    }

}
