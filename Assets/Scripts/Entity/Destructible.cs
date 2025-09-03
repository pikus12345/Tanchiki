using UnityEngine;

namespace Tanchiki.Entity
{
    public class Destructible : MonoBehaviour, IDamageable
    {
        [SerializeField] Sprite deathSprite;
        public void TakeDamage(float damage)
        {
            Die();
        }
        public void Heal(float amount)
        {
            Die();
        }
        public void Die()
        {
            GetComponent<SpriteRenderer>().sprite = deathSprite;
            Destroy(GetComponent<Collider2D>());
        }
        private void OnCollisionEnter2D(Collision2D collision)
        {
            Die();
        }
    }
}