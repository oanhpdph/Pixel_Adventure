using UnityEngine;

namespace Assets._Scripts.Player
{
    public class PlayerGroundCheck : MonoBehaviour
    {
        [SerializeField] private ParticleSystem fallParticle;
        private Animator playerAnimator;
        private PlayerController playerController;

        private bool isPlayEffect = false;

        private void Start()
        {
            playerController = PlayerController.Instance;
            playerAnimator = GetComponent<Animator>();
        }
        private void Update()
        {
            CheckGround();
        }

        private void CheckGround()
        {
            bool onGround = playerController.onGround;
            if (onGround && isPlayEffect)
            {
                playerAnimator.SetBool("onGround", true);
                playerAnimator.SetBool("isJumping", false);
                playerAnimator.ResetTrigger("triggerDoubleJumping");
                GetComponent<Rigidbody2D>().velocity = Vector3.zero;
                fallParticle.Play();
                isPlayEffect = false;
            }
            else if (!onGround)
            {
                playerAnimator.SetBool("onGround", false);
                playerController.onGround = false;
                isPlayEffect = true;
            }
        }
    }
}