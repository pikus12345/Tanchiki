using UnityEngine;
using Tanchiki.Entity;
using UnityEngine.InputSystem;

namespace Tanchiki.PlayerControl
{
    public class PlayerShooting : Shooting
    {
        [SerializeField] private InputActionAsset inputAsset;
        private InputAction shootAction;
        private void OnEnable()
        {
            inputAsset.FindActionMap("Player").Enable();
        }
        private void OnDisable()
        {
            inputAsset.FindActionMap("Player").Disable();
        }
        private void Start()
        {
            shootAction = inputAsset.FindAction("Shoot");
        }
        private void Update()
        {
            UpdateShootingTimer();
            if (shootAction.IsPressed()){
                Shoot();
            }
        }
    }
}