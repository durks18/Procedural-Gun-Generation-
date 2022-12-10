using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class MovingPlatform : MonoBehaviour
{
    public Vector3 StartPosition = new Vector3(0,-5,0), EndPosition = new Vector3(0,5,0);
    public float Speed = 0.75f, inteval = 3;
    float TimeOfReachedPoint;
    bool wasAtStart = true;
    Rigidbody rb;
    Vector3 velRef;
    private void Start()
    {
        StartPosition+= transform.position;
        EndPosition+=transform.position;
        rb = GetComponent<Rigidbody>();
        rb.useGravity =false;
        rb.freezeRotation = true;
        rb.isKinematic = true;
    }

    private void FixedUpdate(){
        if(Time.time> TimeOfReachedPoint+inteval){
            rb.MovePosition(Vector3.SmoothDamp(rb.position,wasAtStart? EndPosition:StartPosition, ref velRef, Time.fixedDeltaTime,Speed));
            if(rb.position == StartPosition && !wasAtStart){
                wasAtStart = true;
                TimeOfReachedPoint = Time.time;
            }else if (rb.position == EndPosition && wasAtStart){
                wasAtStart = false;
                TimeOfReachedPoint = Time.time;
            }
        }
    }

    private void OnDrawGizmos()
    {   
        
        Gizmos.color = Color.cyan;
        Gizmos.DrawWireSphere(transform.position+StartPosition,.5f);    
        //Gizmos.DrawWireSphere(transform.position+StartPosition,.5f);    
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position+EndPosition,.5f);    
    }
}
