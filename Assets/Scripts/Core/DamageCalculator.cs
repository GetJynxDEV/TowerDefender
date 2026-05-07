using UnityEngine;

public static class DamageCalculator 
{
    //Fire => Earth
    //Earth => Air
    //Air => Lightning
    //Lightning => Water
    //Water => Fire
    public static int ElementalDamage(Element defenderElement, Element attackerElement, float attackerDamage)
    {
        float finalDamageValue = attackerDamage;

        if (defenderElement == Element.None || attackerElement == Element.None)
            return Mathf.RoundToInt(finalDamageValue);

        if (defenderElement == attackerElement)
        {
            finalDamageValue *= 0.5f; // Same element, reduced damage
        }
        else if ((attackerElement == Element.Fire && defenderElement == Element.Earth) ||
                 (attackerElement == Element.Earth && defenderElement == Element.Air) ||
                 (attackerElement == Element.Air && defenderElement == Element.Lightning) ||
                 (attackerElement == Element.Lightning && defenderElement == Element.Water) ||
                 (attackerElement == Element.Water && defenderElement == Element.Fire))
        {
            finalDamageValue *= 1.5f; // Strong against, increased damage
        }
        else
        {
            finalDamageValue *= 0.75f; // Weak against, reduced damage
        }

        return Mathf.RoundToInt(finalDamageValue);
    }

    public static bool IsEffective(Element defenderElement, Element attackerElement)
    {
        if (defenderElement == Element.None || attackerElement == Element.None)
            return false;
        return (attackerElement == Element.Fire && defenderElement == Element.Earth) ||
               (attackerElement == Element.Earth && defenderElement == Element.Air) ||
               (attackerElement == Element.Air && defenderElement == Element.Lightning) ||
               (attackerElement == Element.Lightning && defenderElement == Element.Water) ||
               (attackerElement == Element.Water && defenderElement == Element.Fire);
    }
}