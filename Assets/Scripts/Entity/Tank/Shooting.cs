using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using static UnityEngine.UI.Image;

namespace Tanchiki.Entity
{
    public class Shooting : MonoBehaviour
    {
        [Header("Settings")]
        [SerializeField] internal float shootingDelay;
        [SerializeField] private float shootDistance;
        [SerializeField] private LayerMask shootingLayerMask;
        [SerializeField] private float damage;
        internal bool canShoot = true;
        internal AudioSource audioSource;

        [Header("Links & Prefabs")]
        [SerializeField] private Transform firePoint;
        [SerializeField] private GameObject fireEffect;
        [SerializeField] private GameObject hitEffect;
        [SerializeField] private AudioClip shootSound;
        
        internal virtual void Start()
        {
            audioSource = GetComponent<AudioSource>();
        }
        internal virtual IEnumerator reloadRoutine()
        {
            canShoot = false;
            yield return new WaitForSeconds(shootingDelay);
            canShoot = true;
        }
        public void Shoot()
        {
            if (!canShoot) { return; }
            StartCoroutine(reloadRoutine());
            audioSource.PlayOneShot(shootSound);

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