using UnityEngine;

public class DamageCalculator : MonoBehaviour
{
    float finalDamageValue;

    public float PhysicalDamage(float defenderArmor, float attackerDamage)
    {
        finalDamageValue = attackerDamage - defenderArmor;

        if (finalDamageValue < 0)
        {
            finalDamageValue = 1;
        }

        return finalDamageValue;
    }
}