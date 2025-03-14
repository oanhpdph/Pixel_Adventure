using System.Collections;
using UnityEngine;

public class PlayerWallJump : MonoBehaviour
{
    private float _wallJumpingCoolDown = 0.3f;
    private float _wallJumpingDuration = 0.3f;
    private float _wallJumpingDirection = 1;
    private bool _isWallJumping = false;

    private Rigidbody2D playerRigidbody;
    private Animator playerAnimator;

    [Header("Force")]
    [SerializeField] private Vector3 _wallJumpingForce = new(8f, 5f, 0f);


    private PlayerController playerController;

    private void Start()
    {
        playerController = PlayerController.Instance;
        playerRigidbody = GetComponent<Rigidbody2D>();
        playerAnimator = GetComponent<Animator>();
    }
    private void Update()
    {
        CanWallJumping();
    }

    private void FixedUpdate()
    {
        WallJumping();

    }
    private void AddVelocity()
    {
        playerRigidbody.velocity = new Vector3(_wallJumpingForce.x * _wallJumpingDirection, _wallJumpingForce.y, 0);
        playerAnimator.SetBool("isJumping", true);

    }
    private void Flip()
    {
        Vector3 localScale = transform.localScale;
        localScale.x *= -1f;
        transform.localScale = localScale;
    }
    private IEnumerator StopWallJumping()
    {
        yield return new WaitForSeconds(_wallJumpingDuration);
        _isWallJumping = false;

    }
    private void JumpingDirection()
    {
        _wallJumpingDirection = -transform.localScale.x;
    }
    private void WallJumping()
    {
        if (_isWallJumping)
        {
            AddVelocity();
            if (Mathf.Sign(transform.localScale.x) != Mathf.Sign(_wallJumpingDirection) &&
                _wallJumpingCoolDown > 0.3f &&
                _wallJumpingCoolDown < 0.4f)
            {
                _wallJumpingDirection = transform.localScale.x;
            }
        }
    }
    private void CanWallJumping()
    {
        if (playerAnimator.GetBool("onWall") &&
            !playerController.onGround &&
            Input.GetKeyDown(KeyCode.UpArrow) &&
            _wallJumpingCoolDown < 0f)
        {
            _isWallJumping = true;
            JumpingDirection();
            if (transform.localScale.x != _wallJumpingDirection)
            {
                Flip();
            }
            _wallJumpingCoolDown = 0.5f;
            StopCoroutine(StopWallJumping());
            StartCoroutine(StopWallJumping());
        }
        _wallJumpingCoolDown -= Time.deltaTime;
    }
}