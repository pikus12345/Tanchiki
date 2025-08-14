using UnityEngine;

namespace Tanchiki.Utility
{
    public class DestroyAfterTime : MonoBehaviour
    {
        [SerializeField] private float timeToDestroy = 3f; // Время в секундах до уничтожения

        void Start()
        {
            // Вызываем метод Destroy через timeToDestroy секунд
            Destroy(gameObject, timeToDestroy);
        }
    }
}