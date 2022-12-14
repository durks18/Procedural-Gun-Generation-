using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponPart : MonoBehaviour
{
    //Weapon stats 
    public enum WeaponStatType
    {
        Damage,
        Accuracy,
        AmmoPerClip,
        ReloadSpeed,
        FireRate,

    }
    //weapon rarities 
    public enum RarityLevel
    {
        Common,
        Uncommon,
        Rare,
        Epic,
        Legendary,
    }

    [System.Serializable]

    //minimum and maximum stat value
    public class WeaponStatPair
    {
        public WeaponStatType stat;

        public float minStatValue;
        public float maxStatValue;

    }

    public List<WeaponStatPair> rawStats;
    public Dictionary<WeaponStatType, float> stats = new Dictionary<WeaponStatType, float>();

    public RarityLevel rarityLevel;
 
    private void Awake()
    {
        foreach (WeaponStatPair statPair in rawStats)
        {

            float chosenValue = Random.Range(statPair.minStatValue, statPair.maxStatValue);
            stats.Add(statPair.stat, chosenValue);

        }
    }

}
