using TMPro;
using UnityEngine;

namespace Tanchiki.Navigation
{
    public class MessageManager : MonoBehaviour
    {
        [SerializeField] private GameObject messagePrefab;

        public void ShowMessage(string text)
        {
            var msg = Instantiate(messagePrefab, transform);
            msg.transform.GetComponentInChildren<TMP_Text>().text = text;
        }
    }

}
