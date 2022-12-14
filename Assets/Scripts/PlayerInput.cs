using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInput : MonoBehaviour
{

    public Transform weaponSocket;


    Weapon focusedWeapon;
    Weapon equippedWeapon;

    public LayerMask weaponMask;

    // on pressing the E key a ray is fired from the player
    // if the ray hits a weapon within range the player will pick up that weapon
    void Update()
    {

        Debug.DrawRay(Camera.main.transform.position, Camera.main.transform.forward * 10, Color.red);
        RaycastHit hit;

        if(Physics.Raycast(Camera.main.transform.position, Camera.main.transform.forward, out hit, 10, weaponMask))
        {
            Weapon weapon = hit.transform.GetComponent<Weapon>();
            focusedWeapon = weapon;

        }

        else
        {
            if(focusedWeapon!= null)
            {
                focusedWeapon = null;
            }
        }

        if(focusedWeapon != null && Input.GetKeyDown(KeyCode.E))
        {
            EquipWeapon(focusedWeapon);
        }
        //if player has a gun in there hand and the click the mouse button the gun will fire
        if (equippedWeapon != null && Input.GetMouseButton(0))
        {
            equippedWeapon.DoFire();
        }
        //if the player hits the R key the gun will reload and the magazine count will reset
        if (equippedWeapon != null && Input.GetKeyDown(KeyCode.R))
        {
            equippedWeapon.DoReload();
        }
    }

    void EquipWeapon(Weapon weaponToEquip)
    {
        // if the player already has a weapon equipped 
        // when they try pick up another it will drop there current weapon
        if(equippedWeapon != null)
        {
            DropWeapon();
        }

        equippedWeapon = weaponToEquip;
        equippedWeapon.transform.parent = weaponSocket;

        equippedWeapon.GetComponent<Rigidbody>().isKinematic = true;
        equippedWeapon.GetComponent<Collider>().enabled = false;

        Collider[] childColliders = equippedWeapon.transform.GetComponentsInChildren<Collider>();
        for (int i = 0; i < childColliders.Length; i++)
        {
            childColliders[i].enabled = false;
        }

        StartCoroutine(MoveWeaponToSocket());

    }
    // when a weapon is picked up it moves it into the players hands.
    IEnumerator MoveWeaponToSocket()
    {

        float moveTimer = 0;

        Vector3 startPos = equippedWeapon.transform.localPosition;
        Vector3 endPos = Vector3.zero;

        Quaternion startRot = equippedWeapon.transform.localRotation;
        Quaternion endRot = Quaternion.identity;

        while(moveTimer < 1)
        {
            moveTimer += .1f;

            equippedWeapon.transform.localPosition = Vector3.Lerp(startPos, endPos, moveTimer);
            equippedWeapon.transform.localRotation = Quaternion.Lerp(startRot, endRot, moveTimer);

            yield return new WaitForSeconds(.01f);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.layer == LayerMask.NameToLayer("Weapon"))
        {
            focusedWeapon = other.GetComponent<Weapon>();
        }
    }

    void DropWeapon()
    {

        equippedWeapon.GetComponent<Collider>().enabled = true;


        Collider[] childColliders = equippedWeapon.transform.GetComponentsInChildren<Collider>();
        for (int i = 0; i < childColliders.Length; i++)
        {
            childColliders[i].enabled = true;
        }

        // when the player drops the weapon
        // the weapon flys forward rather than dropping straight to the floor
        Rigidbody rb = equippedWeapon.GetComponent<Rigidbody>();
        rb.isKinematic = false;
        rb.AddExplosionForce(3, weaponSocket.position, 1, 1, ForceMode.Impulse);

        equippedWeapon.transform.parent = null;
        equippedWeapon = null;

    }
}
