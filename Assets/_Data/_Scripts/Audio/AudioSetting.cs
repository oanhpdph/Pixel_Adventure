using UnityEngine;
using UnityEngine.UI;

public class AudioSetting : MonoBehaviour
{
    public float volumeMusic;
    public float volumeSFX;

    private Slider _sliderMusic;
    private Slider _sliderSFX;

    private void Start()
    {
        if (AudioManager.Instance != null)
        {
            _sliderMusic = transform.Find("Music").GetComponentInChildren<Slider>();
            _sliderMusic.onValueChanged.AddListener(AudioManager.Instance.ChangeVolumeMusic);
            //_sliderMusic.value = PlayerPrefs.GetFloat("musicVolume");
            _sliderMusic.value = AudioManager.Instance.LoadVolumeMusic();

            _sliderSFX = transform.Find("SFX").GetComponentInChildren<Slider>();
            _sliderSFX.onValueChanged.AddListener(AudioManager.Instance.ChangeVolumeSFX);
            _sliderSFX.value = AudioManager.Instance.LoadVolumeSFX();
        }
    }

    private void OnDestroy()
    {
        if (_sliderMusic != null)
        {
            _sliderMusic.onValueChanged.RemoveListener(AudioManager.Instance.ChangeVolumeMusic);
        }
        if (_sliderSFX != null)
        {
            _sliderSFX.onValueChanged.RemoveListener(AudioManager.Instance.ChangeVolumeSFX);
        }
    }
}
