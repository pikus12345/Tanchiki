using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Tanchiki.Entity {
    public class Healthbar : MonoBehaviour
    {
        [SerializeField] private Slider healthBar;
        [SerializeField] private Slider shieldBar;
        [SerializeField] private Health health;
        [SerializeField] private TMP_Text text;
        [SerializeField] private bool hideWhenFull;

        private void LateUpdate()
        {
            transform.rotation = Quaternion.identity;
        }
        private void Start()
        {
            HealthUpdate();
        }
        private void OnEnable()
        {
            health.onHeal?.AddListener(HealthUpdate);
            health.onTakeShield?.AddListener(HealthUpdate);
            health.onTakeDamage?.AddListener(HealthUpdate);
            health.onDeath?.AddListener(Death);
        }
        private void OnDisable()
        {
            health.onHeal?.RemoveListener(HealthUpdate);
            health.onTakeShield.RemoveListener(HealthUpdate);
            health.onTakeDamage?.RemoveListener(HealthUpdate);
            health.onDeath?.RemoveListener(Death);
        }
        private void HealthUpdate()
        {
            float h = health.currentHealth; // current health
            float mh = health.maxHealth; // max health
            float s = health.currentShield; // current shield
            float ms = health.maxShield; // max shield
            if (h >= mh & hideWhenFull | s >= ms)
            {
                healthBar.gameObject.SetActive(false);
            }
            else 
            {
                healthBar.gameObject.SetActive(true);
            }
            shieldBar.value = s / ms;
            healthBar.value = h / mh;
            text.text = $"{h}/{mh}";
        }
        private void Death()
        {
            Destroy(gameObject);
        }
    }
}
