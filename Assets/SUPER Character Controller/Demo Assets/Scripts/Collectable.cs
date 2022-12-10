using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using SUPERCharacter;

[RequireComponent(typeof(Collider))]
public class Collectable : MonoBehaviour, ICollectable
{
    public UnityEvent OnCollect;

    public virtual void Collect(){
        OnCollect.Invoke();
    }

    public void DestroySelf(){
        Destroy(gameObject);
    }
}
