using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FortPart : MonoBehaviour
{
    public int ResourceRequirement = 5;

    public int CurrentInvestedResources = 0;

    public int BaseArmor = 5;
    
    public int Armor = 5;

    public bool IsDone => ResourceRequirement == CurrentInvestedResources;

    public bool IsDestroyed => IsDone && Armor == 0;

    public void TakeDamage(int amount)
    {
        Armor -= amount;
    }
    
    public void AddToPart(int amount)
    {
        CurrentInvestedResources += amount;

        if (!IsDone) return;

        gameObject.SetActive(true);
    }

    public int Fix(int amount)
    {
        var maxToAdd = BaseArmor - Armor;
        Armor += maxToAdd;

        return amount - maxToAdd;
    }
}
