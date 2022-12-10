using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "RarityColors", menuName = "WeaponGenerator/RarityColors", order = 1)]

public class RaritySO : ScriptableObject
{
    public List<Color> rarityColors;
}
