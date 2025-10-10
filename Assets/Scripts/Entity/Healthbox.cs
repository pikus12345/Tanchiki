using UnityEngine;

namespace Tanchiki.Entity
{
    
    public class Healthbox : Destructible
    {
        [SerializeField]
        [Range(-100f, 100f)]
        internal float health;
        [SerializeField]
        internal bool isShield;
    }
}
