using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using SUPERCharacter;

[RequireComponent(typeof(Collider))]
public class CollectableGem : MonoBehaviour, ICollectable
{
    public UnityEvent OnCollect;
    Vector3 startPos;
    private void Start()
    {
        startPos =transform.position;
    }
    private void Update()
    {
        transform.eulerAngles += Vector3.up*Time.deltaTime*30;
        transform.position = startPos + Vector3.up * Mathf.Sin(Mathf.PI*Time.time)*0.1f;
    }
    public void Collect(){
        OnCollect.Invoke();
        CollectableCounter.instance.AddToCount();
        DestroySelf();

    }

    public void DestroySelf(){
        Destroy(gameObject);
    }
}
