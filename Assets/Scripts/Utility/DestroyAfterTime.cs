using UnityEngine;

namespace Tanchiki.Utility
{
    public class DestroyAfterTime : MonoBehaviour
    {
        [SerializeField] private float timeToDestroy = 3f; // ����� � �������� �� �����������

        void Start()
        {
            // �������� ����� Destroy ����� timeToDestroy ������
            Destroy(gameObject, timeToDestroy);
        }
    }
}