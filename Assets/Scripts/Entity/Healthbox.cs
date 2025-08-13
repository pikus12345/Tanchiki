using UnityEngine;

namespace Tanchiki.Entity
{
    public class Healthbox : MonoBehaviour
    {
        [SerializeField]
        [Range(-100f, 100f)]
        internal float health;
    }
}
