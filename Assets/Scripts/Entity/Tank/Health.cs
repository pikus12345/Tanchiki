using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace Tanchiki.Entity
{
    public interface IDamageable
    {
        void TakeDamage(float damage);
        void Heal(float amount);
        void Die();
    }
    public class Health : MonoBehaviour, IDamageable
    {
        [Header("Health Settings")]
        [SerializeField] internal float maxHealth;
        [SerializeField] internal float currentHealth;

        [Header("Events")]
        public UnityEvent onTakeDamage; // ������� ��� ��������� �����
        public UnityEvent onHeal;       // ������� ��� �������
        public UnityEvent onDeath;      // ������� ��� ������

        public static UnityEvent onAnyDeath;

        [Header("Prefabs")]
        public GameObject damageIndicator;

        [Header("DeathSettings")]
        [SerializeField] private Color deathColor = new Color(0.4f, 0.4f, 0.4f);
        [SerializeField] GameObject liveObjects;
        [SerializeField] GameObject deathObjects;

        private void Start()
        {
            currentHealth = maxHealth; // ������������� HP
        }

        // ��������� �����
        public void TakeDamage(float damage)
        {
            currentHealth -= damage;
            currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth);
            ShowDamageIndicator(damage, false);
            onTakeDamage?.Invoke(); // ������ �������

            if (currentHealth <= 0)
            {
                Die();
            }
        }

        // �������
        public void Heal(float amount)
        {
            currentHealth += amount;
            currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth);
            ShowDamageIndicator(amount, true);
            onHeal?.Invoke();
        }

        // ������ �������
        public void Die()
        {
            onDeath?.Invoke();
            onAnyDeath?.Invoke();

            liveObjects.SetActive(false);
            deathObjects.SetActive(true);
            SpriteRenderer[] sprs = transform.GetComponentsInChildren<SpriteRenderer>();
            foreach (SpriteRenderer sprite in sprs)
            {
                sprite.color = deathColor;
            }
            GetComponent<TankControl>().enabled = false;
            if (GetComponent<AudioSource>() != null)
            {
                if (GetComponent<DistanceBasedAudio>() != null)
                {
                    Destroy(GetComponent<DistanceBasedAudio>());
                }
                Destroy(GetComponent<AudioSource>());
            }

            if (gameObject.CompareTag("Player"))
            {
                
            }
        }

        // ����� HP �� �������������
        public void ResetHealth()
        {
            currentHealth = maxHealth;
        }

        private void ShowDamageIndicator(float value, bool isHeal)
        {
            DamageIndicator dmgInd = Instantiate(damageIndicator, transform.position, Quaternion.identity, null).GetComponent<DamageIndicator>();
            dmgInd.value = value;
            dmgInd.isHeal = isHeal;
        }
        // �������� �� ������ ��������
        public bool IsFullHealth()
        {
            return currentHealth >= maxHealth;
        }
        private void OnCollisionEnter2D(Collision2D collision)
        {
            if (collision.collider.CompareTag("Healthbox"))
            {
                Healthbox box = collision.collider.GetComponent<Healthbox>();
                if (box.health > 0)
                {
                    if (!IsFullHealth())
                    {
                        Heal(box.health);
                    }
                    else
                    {
                        return;
                    }
                }
                else
                {
                    TakeDamage(-box.health);
                }
            }

        }
    }
}