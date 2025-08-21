using Unity.VisualScripting;
using UnityEngine;
using static UnityEngine.UI.Image;

namespace Tanchiki.Entity
{
    public class Shooting : MonoBehaviour
    {
        [Header("Settings")]
        [SerializeField] private float shootingDelay;
        [SerializeField] private float shootDistance;
        [SerializeField] private LayerMask shootingLayerMask;
        [SerializeField] private float damage;
        private float shootingTimer;

        [Header("Links & Prefabs")]
        [SerializeField] private Transform firePoint;
        [SerializeField] private GameObject fireEffect;
        [SerializeField] private GameObject hitEffect;
        
        private void Update()
        {
            UpdateShootingTimer();
        }
        protected void UpdateShootingTimer()
        {
            shootingTimer += Time.deltaTime;
        }
        public void Shoot()
        {
            if (shootingTimer < shootingDelay) { return; }
            shootingTimer = 0;

            if (fireEffect != null)
            {
                GameObject fireEffectClone = Instantiate(fireEffect, firePoint.position, firePoint.rotation, null);
                Destroy(fireEffectClone, 3f);
            }
            

            RaycastHit2D hit = Physics2D.Raycast(firePoint.position, firePoint.right, shootDistance, shootingLayerMask);

            if (hit)
            {
                if (hitEffect != null)
                {
                    GameObject hitEffectClone = Instantiate(hitEffect, hit.point, Quaternion.identity);
                    Destroy(hitEffectClone, 3f);
                }
                Debug.DrawLine(firePoint.position, hit.point, Color.azure, 2f);

                if (hit.collider.GetComponent<IDamageable>() != null)
                {
                    hit.collider.GetComponent<IDamageable>().TakeDamage(damage);
                }
                
            }
        }
    }
}