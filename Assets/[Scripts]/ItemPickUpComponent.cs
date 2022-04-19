using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemPickUpComponent : MonoBehaviour
{
    public AudioSource pickupSound;

    [SerializeField]
    ItemScript pickupItem;

    [Tooltip("Manual Override for drop amount. If left at -1, it will use the amount from the scriptable object")]
    [SerializeField]
    int amount = -1;

    [SerializeField] MeshRenderer propMeshRenderer;
    [SerializeField] MeshFilter propMeshFilter;

    ItemScript ItemInstance;


    // Start is called before the first frame update
    void Start()
    {
        InstantiateItem();
    }

    private void InstantiateItem()
    {
        ItemInstance = Instantiate(pickupItem);
        if (amount > 0)
        {
            ItemInstance.SetAmount(amount);
        }
        else
        {
            ItemInstance.SetAmount(pickupItem.amountValue);
        }   
        ApplyMesh();
    }

    private void ApplyMesh()
    {
        if(propMeshFilter)
        {
            propMeshFilter.mesh = pickupItem.itemPrefab.GetComponentInChildren<MeshFilter>().sharedMesh;
        }

        if (propMeshRenderer)
        {
            propMeshRenderer.materials = pickupItem.itemPrefab.GetComponentInChildren<MeshRenderer>().sharedMaterials;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Player")) return;

        //add to inventory here
        //get reference to player inventory
        InventoryComponent playerInventory = other.GetComponent<InventoryComponent>();
        WeaponHolder weaponHolder = other.GetComponent<WeaponHolder>();

        if (playerInventory)
        {
            pickupSound.Play();
            playerInventory.AddItem(ItemInstance, amount);
        }

        if(ItemInstance.itemCategory == ItemCategory.WEAPON)
        {
            WeaponComponent tempWeaponData = ItemInstance.itemPrefab.GetComponent<WeaponComponent>();
            if (weaponHolder.WeaponAmmoData.ContainsKey(tempWeaponData.weaponStats.weaponType))
            {
                WeaponStats tempWeaponStats = weaponHolder.WeaponAmmoData[tempWeaponData.weaponStats.weaponType];
                tempWeaponStats.totalBullets += ItemInstance.amountValue;

                other.GetComponentInChildren<WeaponHolder>().WeaponAmmoData[tempWeaponData.weaponStats.weaponType] = tempWeaponStats;

                if (weaponHolder.equippedWeapon != null)
                {
                    weaponHolder.equippedWeapon.weaponStats = weaponHolder.WeaponAmmoData[tempWeaponStats.weaponType]; 
                }
            }
            //if its a new weapon, add a new key to weapon ammo data 
        }



        Destroy(gameObject);
    }
}