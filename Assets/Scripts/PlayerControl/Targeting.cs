using UnityEngine;
using UnityEngine.InputSystem;
using YG;

namespace Tanchiki.PlayerControl
{
    public class Targeting : MonoBehaviour
    {
        [SerializeField] private Transform Cursor;

        private Transform playerTurret;

        [SerializeField] private InputActionAsset actionAsset;
        private InputAction actionByPosition;
        private InputAction actionByStick;

        private void Start()
        {
            if (YG2.envir.isMobile) 
            { 
                playerTurret = GameObject.FindGameObjectWithTag("Player").transform.GetChild(0); //башня должна быть первой по индексу
                actionByStick = actionAsset.FindAction("AimByStick");
                actionByStick.Enable();
            }
            else
            {
                actionByPosition = actionAsset.FindAction("AimByMousePosition");
                actionByPosition.Enable();
            }
        }

        private void Update()
        {
            Refresh();
        }
        private void Refresh()
        {
            if (YG2.envir.isMobile)
            {
                Cursor.position = (Vector2)playerTurret.position + actionByStick.ReadValue<Vector2>() * 5;
            }
            else
            {
                Vector2 mousePosition = actionByPosition.ReadValue<Vector2>();
                Cursor.position = Camera.main.ScreenToWorldPoint(mousePosition);
            }
        }
        
    }
}

