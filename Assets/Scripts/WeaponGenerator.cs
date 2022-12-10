using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponGenerator : MonoBehaviour
{

    public List<GameObject> bodyParts;
    public List<GameObject> barrelParts;
    public List<GameObject> StockParts;
    public List<GameObject> SightParts;
    public List<GameObject> MagazineParts;
    public List<GameObject> GripParts;


    GameObject prevWeapon;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            GenerateWeapon();
        }
    }

    void GenerateWeapon()
    {

       // if (prevWeapon != null)
       // {
       //     Destroy(prevWeapon);
       // }

        GameObject randomChasis = GetRandomPart(bodyParts);
        GameObject insBody = Instantiate(randomChasis, Vector3.zero, Quaternion.identity);
        WeaponBody wpnBody = insBody.GetComponent<WeaponBody>();


        WeaponPart barrel = SpawnWeaponPart(barrelParts, wpnBody.BarrelSocket);
        WeaponPart sight = SpawnWeaponPart(SightParts, wpnBody.SightSocket);
        WeaponPart stock = SpawnWeaponPart(StockParts, wpnBody.StockSocket);
        WeaponPart magazine = SpawnWeaponPart(MagazineParts, wpnBody.MagazineSocket);
        WeaponPart grip = SpawnWeaponPart(GripParts, wpnBody.GripSocket);

        wpnBody.Initialize((WeaponBarrelPart)barrel, sight, stock, magazine, grip);

        prevWeapon = insBody;

    }

    WeaponPart SpawnWeaponPart(List<GameObject> parts, Transform socket)
    {
        GameObject randomPart = GetRandomPart(parts);
        GameObject inspart = Instantiate(randomPart, socket.transform.position, socket.transform.rotation);
        inspart.transform.parent = socket;

        return inspart.GetComponent<WeaponPart>();
    }

    GameObject GetRandomPart(List<GameObject> partList)
    {
        int randomNumber = Random.Range(0, partList.Count);
        return partList[randomNumber];

    }
}
