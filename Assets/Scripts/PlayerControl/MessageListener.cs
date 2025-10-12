using UnityEngine;
using Tanchiki.GameManagers;
using Tanchiki.Navigation;
using Tanchiki.Entity;

namespace Tanchiki.PlayerControl
{
    public class MessageListener : MonoBehaviour
    {
        private MessageManager messageManager;
        private void Start()
        {
            if (GameManager.Instance.messageManager != null)
            {
                messageManager = GameManager.Instance.messageManager;
            }
            else
            {
                Debug.Log("MessageManager is null!");
            }
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.gameObject.CompareTag("MessageMark"))
            {
                messageManager.ShowMessage(collision.gameObject.GetComponent<MessageMark>().messageText);
                Destroy(collision.gameObject);
            }
        }
    }
}