using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Palmmedia.ReportGenerator.Core;

public class WeaponGenerator : MonoBehaviour
{

    public List<GameObject> bodyParts;
    public List<GameObject> barrelParts;
    public List<GameObject> StockParts;
    public List<GameObject> SightParts;
    public List<GameObject> MagazineParts;
    public List<GameObject> GripParts;

    [SerializeField] TMP_InputField seedInputField;
    [SerializeField] TextMeshProUGUI placeHolderSeedText;
    [SerializeField] TextMeshProUGUI seedName;

    GameObject currentWeapon = null;
    GameObject prevWeapon;

    void Start()
    {  
        GenerateWeapon();
        ShowCursor();
    }

    int currentSeed;

     public void GenerateWeapon()
    {
        //when a weapon is generated it also sets a random seed number 
        SetRandomSeed();

        seedName.text = seedInputField.text;

        //if the player generates a new gun the previous one is destroyed
        if (prevWeapon != null)
        {
            Destroy(prevWeapon);
        }

        // spawn weapon chasis 
        // spawn parts at chasis sockets
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

    void SetRandomSeed()
    {
        if (seedInputField.text != "")
        {
            try //only numbers
            {
                currentSeed = System.Int32.Parse(seedInputField.text);
            }
            catch //if contains Letters
            {
                currentSeed = seedInputField.text.GetHashCode();
            }
        }
        else
            currentSeed = Random.seed;


        Random.InitState(currentSeed);
        placeHolderSeedText.text = currentSeed.ToString();
    }
    //this method when activated copies the guns seed so it can be recalled
    public void CopySeedToClipboard()
    {
        GUIUtility.systemCopyBuffer = currentSeed.ToString();
    }
    //when this method is called the previously copied seed is deleted
    public void ClearSeed()
    {
        seedInputField.text = "Seed string";
    }
    // on the start of the game the cursor is enabled
    void ShowCursor()
    {
        if (!Cursor.visible || Cursor.lockState == CursorLockMode.Locked)
        {
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
        }
    }
}
