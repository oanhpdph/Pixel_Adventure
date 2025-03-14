using Assets._Data._Scripts.Audio;
using Assets._Data._Scripts.Common;
using System.Collections;
using UnityEngine;

public class PlayerHealth : Health
{
    [SerializeField] private GameObject _panelGameOver;

    public Vector2 respawnPos;
    public Rigidbody2D playerRigidBody;
    public float CurrentHealth { get; private set; }

    private Animator playerAnimator;
    public bool isDie = false;

    public override void Awake()
    {
        base.Awake();
        Init();
    }
    private void Init()
    {
        CurrentHealth = base.startingHealth;
    }
    private void Start()
    {
        playerAnimator = GetComponent<Animator>();
        respawnPos = StartPoint.Instance.transform.position;

    }
    public void PlayerTakeDamage(float _damage)
    {
        isDie = base.TakeDamageHealth(_damage);
        LoadHealth();
        AudioManager.Instance.PlaySFX(AudioAssets.instance.HitSound());

        if (isDie)// alive
        {
            Die();
            return;
        }
        playerAnimator.SetBool("isTakeDamage", true);
        playerAnimator.SetBool("isJumping", false);
        playerAnimator.SetBool("isFalling", false);
        StartCoroutine(Respawn());
    }
    private void Die()
    {
        isDie = true;
        AudioManager.Instance.PlaySFX(AudioAssets.instance.DieSound());

        if (_panelGameOver != null)
        {
            _panelGameOver.SetActive(true);
        }
        Time.timeScale = 0;
    }
    public IEnumerator Respawn()
    {
        PlayerController.Instance.GetComponent<Rigidbody2D>().simulated = false;
        yield return new WaitForSeconds(0.8f);

        transform.localScale = new Vector3(0, 0, 0);
        transform.position = respawnPos;
        transform.localScale = new Vector3(1, 1, 1);

        transform.GetComponent<Rigidbody2D>().velocity = Vector3.zero;
        playerAnimator.SetBool("isAppearing", true);

        yield return new WaitForSeconds(0.8f);
        playerAnimator.SetBool("isAppearing", false);
        playerAnimator.SetBool("isTakeDamage", false);

        PlayerController.Instance.GetComponent<Rigidbody2D>().simulated = true;
    }

    public void LoadHealth()
    {
        CurrentHealth = base.currentHealth;
    }
}
