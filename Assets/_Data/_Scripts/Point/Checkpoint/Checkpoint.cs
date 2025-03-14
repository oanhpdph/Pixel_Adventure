using Assets._Data._Scripts.Audio;
using UnityEngine;

public class Checkpoint : MonoBehaviour
{
    private Animator _animator;
    private void Start()
    {
        _animator = GetComponent<Animator>();
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        string collisionTag = collision.tag;
        if (collisionTag.CompareTo("player") == 0)
        {
            _animator.SetTrigger("checking");
            _animator.SetBool("checked", true);

            this.GetComponent<Collider2D>().enabled = false;
            AudioManager.Instance.PlaySFX(AudioAssets.instance.CheckpointSound());
            SaveRespawnPos();
        }
    }

    private void SaveRespawnPos()
    {
        PlayerController.Instance.GetComponent<PlayerHealth>().respawnPos = transform.position;
    }
}
