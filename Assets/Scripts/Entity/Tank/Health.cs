using System;
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
        [SerializeField] internal int maxShield;
        [SerializeField] internal int currentShield;
        [SerializeField] private bool isBoss;
        

        [Header("Events")]
        public UnityEvent onTakeDamage; // Событие при получении урона
        public UnityEvent onHeal;       // Событие при лечении
        public UnityEvent onDeath;      // Событие при смерти
        public UnityEvent onTakeShield;

        public static event Action<GameObject, Health> OnAnyDeath;
        public static event Action<GameObject, Health> OnBossDeath;

        [Header("Prefabs")]
        public GameObject damageIndicator;

        [Header("DeathSettings")]
        [SerializeField] private Color deathColor = new Color(0.4f, 0.4f, 0.4f);
        [SerializeField] GameObject liveObjects;
        [SerializeField] GameObject deathObjects;

        private void Start()
        {
            currentHealth = maxHealth; // Инициализация HP
        }

        // Нанесение урона
        public void TakeDamage(float damage)
        {
            
            
            if (currentShield > 0)
            {
                currentShield -= 1;
                currentShield = Mathf.Clamp(currentShield, 0, maxShield);
            }
            else
            {
                currentHealth -= damage;
                currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth);
            }
            onTakeDamage?.Invoke();
            ShowDamageIndicator(damage, false);

            if (currentHealth <= 0)
            {
                Die();
            }
        }

        // Лечение
        public void Heal(float amount)
        {
            currentHealth += amount;
            currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth);
            ShowDamageIndicator(amount, true);
            onHeal?.Invoke();
        }
        public void TakeShield(int amount)
        {
            currentShield += amount;
            currentShield = Mathf.Clamp(currentShield, 0, maxShield);
            ShowDamageIndicator(amount, true);
            onTakeShield?.Invoke();
        }

        // Смерть объекта
        public void Die()
        {
            onDeath?.Invoke();
            OnAnyDeath?.Invoke(gameObject, this);
            if (isBoss) OnBossDeath?.Invoke(gameObject, this);

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

        // Сброс HP до максимального
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
        // Проверка на полное здоровье
        public bool IsFullHealth()
        {
            return currentHealth >= maxHealth;
        }
        public bool IsFullShield()
        {
            return currentShield >= maxShield;
        }
        private void OnCollisionEnter2D(Collision2D collision)
        {
            if (collision.collider.CompareTag("Healthbox"))
            {
                Healthbox box = collision.collider.GetComponent<Healthbox>();
                if (box.health > 0)
                {
                    if (box.isShield & !IsFullShield())
                    {
                        TakeShield(Mathf.FloorToInt(box.health));
                        return;
                    }
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