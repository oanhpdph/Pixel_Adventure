using Assets._Data._Scripts.Audio;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance;
    [Header("--------Audio Source----------")]
    [SerializeField] private AudioSource musicSource;
    [SerializeField] private AudioSource SFXSource;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            if (gameObject.transform.parent != null)
            {
                DontDestroyOnLoad(gameObject.transform.parent.gameObject);
            }
            else
            {
                DontDestroyOnLoad(gameObject);
            }
        }
        else
        {
            Destroy(this.gameObject);
        }

    }
    // Start is called before the first frame update
    void Start()
    {
        musicSource.clip = AudioAssets.instance.BackGroundSound();
        musicSource.Play();
        ChangeVolumeMusic(PlayerPrefs.GetFloat("musicVolume", 1));
        ChangeVolumeSFX(PlayerPrefs.GetFloat("SFXVolume", 1));

    }

    public void PlaySFX(AudioClip audioClip)
    {
        if (audioClip != null)
        {
            SFXSource.PlayOneShot(audioClip);
        }
    }
    public void ChangeVolumeMusic(float value)
    {
        musicSource.volume = value;
        PlayerPrefs.SetFloat("musicVolume", value);
    }
    public void ChangeVolumeSFX(float value)
    {
        SFXSource.volume = value;
        PlayerPrefs.SetFloat("SFXVolume", value);
    }

    public float LoadVolumeMusic()
    {
        return PlayerPrefs.GetFloat("musicVolume");
    }
    public float LoadVolumeSFX()
    {
        return PlayerPrefs.GetFloat("SFXVolume");
    }
}
