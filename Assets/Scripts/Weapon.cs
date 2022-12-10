using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    public float shootRange = 1;

    float fireRate;
    float reloadSpeed;
    float accuracy;
    float damage;
    int ammoInClip;

    public float accuracyOffset = 0f;

    float nextFire;
    int currentAmmo;
    bool isReloading = false;

    Transform muzzle;
    ParticleSystem muzzleFlashFX;
    LineRenderer tracerFX;
    public GameObject impactFX;

    private void Start()
    {
        tracerFX = GetComponent<LineRenderer>();

        Reload();
    }

    public void Initialize(Dictionary<WeaponPart.WeaponStatType, float> weaponStats, WeaponBody body)
    {
        SetWeaponStats(weaponStats);

        muzzle = body.muzzle;
        muzzleFlashFX = body.muzzleFX.GetComponent<ParticleSystem>();
    }

    void SetWeaponStats(Dictionary<WeaponPart.WeaponStatType, float> weaponStats)
    {
        damage = weaponStats[WeaponPart.WeaponStatType.Damage];
        reloadSpeed = weaponStats[WeaponPart.WeaponStatType.ReloadSpeed];
        accuracy = weaponStats[WeaponPart.WeaponStatType.Accuracy] / 100;
        ammoInClip = (int)weaponStats[WeaponPart.WeaponStatType.AmmoPerClip];
        fireRate = weaponStats[WeaponPart.WeaponStatType.FireRate];

    }

    public void DoFire()
    {
        if (currentAmmo > 0 && Time.time > nextFire)
        {
            Shoot();
            nextFire = Time.time + fireRate;
        }
    }

    public void DoReload()
    {
        if(!isReloading)
        {
            isReloading = true;
            Invoke("Reload", reloadSpeed);
        }
    }

    void Shoot()
    {

        muzzleFlashFX.Emit(1);
        tracerFX.SetPosition(0, muzzle.position);
        StartCoroutine(FlashTracer());

        currentAmmo--;

        Vector3 offset = Random.insideUnitSphere * ((1 - accuracy) * accuracyOffset); 

        RaycastHit hit;
        Ray ray = new Ray(Camera.main.transform.position, Camera.main.transform.forward);

        Debug.DrawRay(ray.origin, ray.direction * 120f, Color.red, 5);

        if(Physics.Raycast(ray, out hit, shootRange))
        {
            tracerFX.SetPosition(1, hit.point);

            Target target = hit.transform.GetComponent<Target>();
            if(target != null)
            {
                target.TakeDamage(damage);
            }

            GameObject insImpact = Instantiate(impactFX, hit.point, Quaternion.LookRotation(hit.normal));
            Destroy(insImpact, 1f);
        }

        else
        {
            tracerFX.SetPosition(1, ray.origin + (ray.direction * shootRange));
        }
    }

    void Reload()
    {
        currentAmmo = ammoInClip;
        isReloading = false;
    }

    IEnumerator FlashTracer()
    {
        tracerFX.enabled = true;
        yield return new WaitForSeconds(.1f);
        tracerFX.enabled = false;
    }
}
