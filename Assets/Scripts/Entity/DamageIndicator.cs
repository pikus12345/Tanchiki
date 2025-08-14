using TMPro;
using UnityEngine;

public class DamageIndicator : MonoBehaviour
{
    public float value;
    public bool isHeal;
    [Header("Settings")]
    [SerializeField] private TMP_Text text;
    [SerializeField] private Color Damagecolor;
    [SerializeField] private Color HealColor;

    private void Start()
    {
        Initalize();
    }
    private void Initalize()
    {
        text.color = isHeal ? HealColor : Damagecolor;
        text.text = isHeal ? $"+{value}" : $"-{value}";
    }
}
