using Assets._Data._Scripts.Audio;
using UnityEngine;

public class PlayerWallCheck : MonoBehaviour
{
    [Header("Player Info")]
    private Animator playerAnimator;
    private Rigidbody2D playerRigidbody;

    [Header("Orther")]
    [SerializeField] private float wallSlidingSpeed = 2f;

    [Header("Particle")]
    [SerializeField] private ParticleSystem wallJumpParticle;

    private float timeCoolSound = 0.4f;
    private PlayerController playerController;
    private float dustFormationPeriod = 0f;
    private void Start()
    {
        playerController = PlayerController.Instance;

        playerAnimator = GetComponent<Animator>();
        playerRigidbody = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        OnWall();
    }

    private void OnWall()
    {
        if (playerController.onWall && !playerController.DistanceWithGround(new Vector2(0.1f, 2)) && Input.GetAxis("Horizontal") != 0)
        {
            PlayeSound();
            WallSlide();
            PlayEffect();
            playerAnimator.SetBool("onWall", true);
            playerAnimator.SetBool("isJumping", false);
        }
        else
        {
            playerAnimator.SetBool("onWall", false);
        }
    }

    private void PlayeSound()
    {
        if (timeCoolSound < 0f)
        {
            AudioManager.Instance.PlaySFX(AudioAssets.instance.MovementSound());
            timeCoolSound = 0.4f;
        }
        timeCoolSound -= Time.deltaTime;
    }
    private void PlayEffect()
    {
        dustFormationPeriod -= Time.deltaTime;
        if (dustFormationPeriod < 0)
        {
            wallJumpParticle.Play();
            dustFormationPeriod = 0.1f;
        }
    }
    private void WallSlide()
    {
        float velocityY = Mathf.Clamp(playerRigidbody.velocity.y, wallSlidingSpeed, float.MaxValue);
        playerRigidbody.velocity = new Vector2(0, -velocityY);
    }

}
