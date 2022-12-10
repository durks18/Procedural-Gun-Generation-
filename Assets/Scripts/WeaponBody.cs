using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponBody : WeaponPart
{

    public Transform BarrelSocket;
    public Transform SightSocket;
    public Transform MagazineSocket;
    public Transform GripSocket;
    public Transform StockSocket;

    List<WeaponPart> weaponParts = new List<WeaponPart>();
    Dictionary<WeaponStatType, float> weaponStats = new Dictionary<WeaponStatType, float>();

    int rawRarity = 0;

    public RaritySO raritySO;

    public Weapon weapon;
    public Transform muzzle;
    public GameObject muzzleFX;

    public void Initialize(WeaponBarrelPart barrel, WeaponPart scope, WeaponPart stock, WeaponPart magazine, WeaponPart grip)
    {
        weaponParts.Add(this);
        muzzle = barrel.muzzle;
        muzzleFX = barrel.muzzleFX;

        weaponParts.Add(barrel);
        weaponParts.Add(scope);
        weaponParts.Add(stock);
        weaponParts.Add(magazine);
        weaponParts.Add(grip);

        CalculateStats();
        DetermineRarity();

        weapon.Initialize(weaponStats, this);

    }

    void CalculateStats()
    {
        // go through the list of weaponparts
        // go through all statistics per weaponpart
        // save them in collection

        foreach (WeaponPart part in weaponParts)
        {
            
            rawRarity += (int)part.rarityLevel;
        
            foreach (KeyValuePair<WeaponStatType, float> stat in part.stats)
            {
                if (!weaponStats.ContainsKey(stat.Key))
                {
                    weaponStats.Add(stat.Key, stat.Value);
                }
                else
                {
                    weaponStats[stat.Key] += stat.Value;
                }
            }
        }
    }


    void DetermineRarity()
    {
        int averageRarity = rawRarity / weaponParts.Count;
        averageRarity = Mathf.Clamp(averageRarity, 0, 4);
        rarityLevel = (RarityLevel)averageRarity;

        foreach (WeaponPart weaponPart in weaponParts)
        {
            Outline outlineWpnPart = weaponPart.GetComponent<Outline>();
            outlineWpnPart.OutlineColor = raritySO.rarityColors[(int)rarityLevel];
        }
    }
}
