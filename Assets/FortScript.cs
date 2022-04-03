using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class FortScript : MonoBehaviour
{
    public GameObject Bounds;
    
    public int BaseArmor = 20;

    public int CurrentArmor = 20;
    
    public bool IsAlive => Armor > 0;
    
    public int Armor => Parts.Where(x => x.IsDone).Sum(x => x.Armor) + CurrentArmor;

    public IEnumerable<FortPart> DamagedParts => Parts.Where(x => x.IsDone && x.Armor != x.BaseArmor);
    
    public IEnumerable<FortPart> DoneParts => Parts.Where(x => x.IsDone);
    
    public FortPart[] Parts;

    public int CurrentPartIndex = 0;

    // private void Update()
    // {
    //     if (Input.GetMouseButtonDown(1))
    //     {
    //         TakeDamage(1);
    //     }
    //     
    //     if (Input.GetKeyDown(KeyCode.Q))
    //     {
    //         BuildCurrentPart(1);
    //     }
    // }
    
    public void TakeDamage(int amount)
    {
        if (DoneParts.Any())
        {
            var workingPart = DoneParts.FirstOrDefault(x => !x.IsDestroyed);

            if (workingPart != null)
            {
                workingPart.TakeDamage(amount);
            }
            else
            {
                CurrentArmor -= amount;
            }
        }
        else
        {
            CurrentArmor -= amount;
        }

        CurrentArmor = Math.Max(CurrentArmor, 0);
    }
    
    private void Start()
    {
        Parts = GetComponentsInChildren<FortPart>(true);

        foreach (var fortPart in Parts)
        {
            fortPart.gameObject.SetActive(false);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Player")) return;
        
        var player = other.GetComponent<PlayerScript>();

        if (player.Resource != null)
        {
            DepositResource(player.Resource);

            player.DepositSource.Play();
            
            Destroy(player.Resource.gameObject, 0.1f);
        }
    }

    public void DepositResource(ResourceComponent resource)
    {
        BuildCurrentPart(resource.Value);
    }

    public void BuildCurrentPart(int amount)
    {
        if (DamagedParts.Any())
        {
            amount = DamagedParts.First().Fix(amount);
            BuildCurrentPart(amount);
            return;
        }

        if (BaseArmor != CurrentArmor)
        {
            var max = BaseArmor - CurrentArmor;

            if (max > amount)
            {
                CurrentArmor += amount;
                return;
            }
            
            CurrentArmor = BaseArmor;
            amount -= max;
            BuildCurrentPart(amount);
            return;
        }
        
        var maxToAdd = CurrentPart.ResourceRequirement - CurrentPart.CurrentInvestedResources;
        
        if (maxToAdd > amount)
        {
            CurrentPart.AddToPart(amount);
            return;
        }

        var leftovers = amount - maxToAdd;

        CurrentPart.AddToPart(maxToAdd);

        if (!GetNextPart()) return;
        
        BuildCurrentPart(leftovers);
    }

    public bool GetNextPart()
    {
        if (CurrentPartIndex == Parts.Length - 1) return false;

        CurrentPartIndex++;

        return true;
    }

    public FortPart CurrentPart => Parts[CurrentPartIndex];

}
