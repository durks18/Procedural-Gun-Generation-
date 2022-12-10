using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Target : MonoBehaviour
{

    public float maxHealth = 100;
    float currentHealth;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void TakeDamage(float damageToTake)
    {
        currentHealth -= damageToTake;

        StartCoroutine(FlashRed());

        if(currentHealth < 0)
        {
            Destroy(gameObject);
        }
    }

    IEnumerator FlashRed()
    {
        GetComponent<Renderer>().material.color = Color.red;
        yield return new WaitForSeconds(.1f);
        GetComponent<Renderer>().material.color = Color.white;
    }
}
