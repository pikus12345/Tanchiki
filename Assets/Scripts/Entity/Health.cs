using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace Tanchiki.Entity
{
    public class HealthBar : MonoBehaviour
    {
        [Header("Health Settings")]
        [SerializeField] private float maxHealth = 100f;
        [SerializeField] private float currentHealth;

        [Header("Events")]
        public UnityEvent onTakeDamage; // ������� ��� ��������� �����
        public UnityEvent onHeal;       // ������� ��� �������
        public UnityEvent onDeath;      // ������� ��� ������

        [Header("Prefabs")]
        public GameObject damageIndicator;

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
        private void Die()
        {
            onDeath?.Invoke();
            Destroy(gameObject); // ��� ������ ������ (��������, ���������� ����������)
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
                Destroy(collision.collider.gameObject);
            }

        }
    }
}