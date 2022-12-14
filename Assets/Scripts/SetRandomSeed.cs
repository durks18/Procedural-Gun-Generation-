using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SetRandomSeed : MonoBehaviour
{
    [SerializeField]
    public string stringSeed = "seed string";
    public bool useStringSeed;
    public int seed;
    public bool randomizeSeed;

    void Awake()
    {
        if (useStringSeed)
        {
            seed = stringSeed.GetHashCode();
        }

        if (randomizeSeed)
        {
            seed = Random.Range(0, 44444);
        }

        Random.InitState(seed);
    }
}
