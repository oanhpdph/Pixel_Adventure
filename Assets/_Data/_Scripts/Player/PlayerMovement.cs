using Assets._Data._Scripts.Audio;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private float horizontalInput;
    private float timeCoolSound = 0.4f;

    [Header("Force")]
    [SerializeField] private float jumpForce = 15;
    [SerializeField] private float doubleJumpForce = 15;
    [SerializeField] private float moveSpeed = 8f;

    [Header("Effect")]
    [SerializeField] private ParticleSystem moveParticle;
    [SerializeField] private ParticleSystem fallParticle;
    [SerializeField] private TrailRenderer trailRenderer;
    [SerializeField] private float dustFormationPeriod;
    private float counter;

    [Header("Player Info")]
    private PlayerController playerController;
    private Animator playerAnimator;
    private Rigidbody2D playerRigidbody;

    private bool canDoubleJump;
    private PlayerHealth Health;

    void Start()
    {
        Init();
    }
    void Update()
    {
        if (Health.isDie)
        {
            return;
        }
        GetHorizontal();
        JumpAction();
        Falling();
        PlayerMove();

        if (!playerAnimator.GetBool("onWall") && !playerAnimator.GetBool("onGround"))
        {
            trailRenderer.enabled = true;
        }
        else
        {
            trailRenderer.enabled = false;
        }
        Navigation();
    }

    private void Init()
    {
        playerController = PlayerController.Instance;
        playerAnimator = GetComponent<Animator>();
        playerRigidbody = GetComponent<Rigidbody2D>();
        Health = GetComponent<PlayerHealth>();
    }
    protected void GetHorizontal()
    {
        horizontalInput = Input.GetAxisRaw("Horizontal");
    }
    protected void PlayerMove()
    {

        AudioPlayer();
        timeCoolSound -= Time.deltaTime;
        if (!playerController.onWall)
        {
            playerRigidbody.velocity = new Vector2(horizontalInput * moveSpeed, playerRigidbody.velocity.y);
        }
        playerAnimator.SetBool("isRuning", horizontalInput != 0);
        MoveParticle();
    }
    private void MoveParticle()
    {
        counter += Time.deltaTime;
        if (playerController.onGround && Mathf.Abs(playerRigidbody.velocity.x) > 3)
        {
            if (counter > Random.Range(0, dustFormationPeriod))
            {
                moveParticle.Play();
                counter = 0;
            }
        }
    }
    private void AudioPlayer()
    {
        if (timeCoolSound < 0f && horizontalInput != 0 && (playerController.onGround))
        {
            AudioManager.Instance.PlaySFX(AudioAssets.instance.MovementSound());
            timeCoolSound = 0.4f;
        }
    }

    protected void Navigation()
    {
        if (horizontalInput > 0)
            transform.localScale = Vector2.one;
        else if (horizontalInput < 0)
            transform.localScale = new Vector2(-1, 1);
    }

    //
    // jump 
    //
    protected void JumpAction()
    {
        if (playerAnimator.GetBool("onWall"))
        {
            canDoubleJump = false;
        }
        if (playerController.onGround)
        {
            canDoubleJump = true;
        }
        if (!playerAnimator.GetBool("isTakeDamage"))
        {
            if (Input.GetKeyDown(KeyCode.UpArrow))
            {
                if (playerController.onGround)
                {
                    SingleJump();
                }
                else if (canDoubleJump)
                {
                    DoubleJump();
                }
            }
        }
    }
    protected void SingleJump()
    {
        AddJumpForce(jumpForce);
        playerAnimator.SetBool("isJumping", true);
        canDoubleJump = true;
    }
    protected void DoubleJump()
    {
        AddJumpForce(doubleJumpForce);
        fallParticle.Play();
        playerAnimator.SetTrigger("triggerDoubleJumping");
        playerAnimator.SetBool("isJumping", false);
        canDoubleJump = false;
    }
    protected void Falling()
    {
        if (playerRigidbody.velocity.y < 0)
        {
            playerAnimator.SetBool("isFalling", true);
        }
        else if (playerRigidbody.velocity.y >= 0)
        {
            playerAnimator.SetBool("isFalling", false);
        }
    }

    protected void AddJumpForce(float force)
    {
        playerRigidbody.velocity = new Vector2(playerRigidbody.velocity.x, 0);
        playerRigidbody.velocity = Vector2.up * force;
    }
}
