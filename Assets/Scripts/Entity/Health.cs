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

        private void Start()
        {
            currentHealth = maxHealth; // ������������� HP
        }

        // ��������� �����
        public void TakeDamage(float damage)
        {
            currentHealth -= damage;
            currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth);

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
                    TakeDamage(box.health);
                }
                Destroy(collision.collider.gameObject);
            }

        }
    }
}