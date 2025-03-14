using UnityEngine;

public class Damage : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] private float _damage;
    public float DamageDeal => _damage;

}
