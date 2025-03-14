using UnityEngine;

namespace Assets._Data._Scripts.Audio
{
    public class AudioAssets : MonoBehaviour
    {
        public static AudioAssets instance { get; set; }


        [SerializeField]
        private AudioClip _backGroundSound;
        [SerializeField]
        private AudioClip _collectSound;
        [SerializeField]
        private AudioClip _dieSound;
        [SerializeField]
        private AudioClip _finishSound;
        [SerializeField]
        private AudioClip _jumpSound;
        [SerializeField]
        private AudioClip _movementSound;
        [SerializeField]
        private AudioClip _hitSound;
        [SerializeField]
        private AudioClip _checkpointSound;


        private void Awake()
        {
            instance = this;
        }

        public AudioClip BackGroundSound()
        {
            return _backGroundSound;
        }
        public AudioClip CollectSound()
        {
            return _collectSound;
        }
        public AudioClip DieSound()
        {
            return _dieSound;
        }
        public AudioClip FinishSound()
        {
            return _finishSound;
        }
        public AudioClip JumpSound()
        {
            return _jumpSound;
        }
        public AudioClip MovementSound()
        {
            return _movementSound;
        }
        public AudioClip HitSound()
        {
            return _hitSound;
        }
        public AudioClip CheckpointSound()
        {
            return _checkpointSound;
        }

    }
}