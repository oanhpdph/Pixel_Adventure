using UnityEngine;

namespace Assets._Data._Scripts.Common
{
    public class Health : MonoBehaviour
    {

        public float startingHealth = 1;

        public float currentHealth;

        public virtual void Awake()
        {
            currentHealth = startingHealth;

        }

        public bool TakeDamageHealth(float damage)
        {
            currentHealth = Mathf.Clamp(currentHealth - damage, 0, startingHealth);
            if (currentHealth > 0)
            {
                return false;// alive
            }
            else
            {
                return true;// die
            }
        }
        public void AddHealth(float _health)
        {
            currentHealth = Mathf.Clamp(currentHealth + _health, 0, startingHealth);
        }

    }
}