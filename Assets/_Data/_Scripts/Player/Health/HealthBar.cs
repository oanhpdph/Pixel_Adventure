using UnityEngine;
using UnityEngine.UI;
public class HealthBar : MonoBehaviour
{
    [SerializeField] private Image _totalHealthBar;
    [SerializeField] private Image _currentHealthBar;
    [SerializeField] private GameObject player;
    private PlayerHealth _playerHealth;

    private void Start()
    {
        _playerHealth = player.GetComponentInChildren<PlayerHealth>();
        _totalHealthBar.fillAmount = _playerHealth.CurrentHealth / 10;
    }

    private void Update()
    {
        _currentHealthBar.fillAmount = _playerHealth.CurrentHealth / 10;
    }
}