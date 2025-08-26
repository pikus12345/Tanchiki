using UnityEngine;

namespace Tanchiki.Entity
{
    public class Healthbox : MonoBehaviour, IDamageable
    {
        [SerializeField]
        [Range(-100f, 100f)]
        internal float health;

        public void Die()
        {
            Destroy(gameObject);
        }

        public void Heal(float amount)
        {
            return;
        }

        public void TakeDamage(float damage)
        {
            Die();
        }
    }
}
