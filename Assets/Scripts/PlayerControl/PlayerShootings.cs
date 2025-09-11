using UnityEngine;
using Tanchiki.Entity;
using UnityEngine.InputSystem;
using System.Collections;

namespace Tanchiki.PlayerControl
{
    public class PlayerShooting : Shooting
    {
        [SerializeField] private InputActionAsset inputAsset;
        [SerializeField] private AudioClip reloadCompletedSound;
        private InputAction shootAction;

        internal override IEnumerator reloadRoutine()
        {
            canShoot = false;
            yield return new WaitForSeconds(shootingDelay / 2);
            audioSource.PlayOneShot(reloadCompletedSound);
            yield return new WaitForSeconds(shootingDelay / 2);
            canShoot = true;
        }
        private void OnEnable()
        {
            inputAsset.FindActionMap("Player").Enable();
        }
        private void OnDisable()
        {
            inputAsset.FindActionMap("Player").Disable();
        }
        internal override void Start()
        {
            base.Start();
            shootAction = inputAsset.FindAction("Shoot");
        }
        private void Update()
        {
            if (shootAction.IsPressed()){
                Shoot();
            }
        }
    }
}