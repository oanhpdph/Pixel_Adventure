using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private static PlayerController instance { get; set; }
    public static PlayerController Instance => instance;

    [Header("Check Position")]
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private Transform pointCheckGround;
    [SerializeField] private Transform pointCheckWall;


    public bool onGround = false;
    public bool onWall = false;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }

    private void Update()
    {
        OnGround();
        OnWall();
    }

    public Vector3 GetPosition()
    {
        return instance.transform.position;
    }

    private bool OnGround()
    {
        onGround = DistanceWithGround(new Vector2(0.5f, 0.03f));
        return onGround;
    }

    public bool DistanceWithGround(Vector2 size)
    {
        return Physics2D.OverlapBox(pointCheckGround.position, size, 0, groundLayer);
    }
    private bool OnWall()
    {
        onWall = Physics2D.OverlapBox(pointCheckWall.position, new Vector2(0.05f, 1f), 0, groundLayer);
        return onWall;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Obstacle"))
        {
            GetComponent<PlayerHealth>().PlayerTakeDamage(collision.GetComponent<Damage>().DamageDeal);
        }
        else if (collision.CompareTag("Fruit"))
        {
            GetComponentInChildren<PlayerCollect>().CollectFruit(collision);
        }
    }
}