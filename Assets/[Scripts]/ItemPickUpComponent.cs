using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemPickUpComponent : MonoBehaviour
{
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

        if (playerInventory) playerInventory.AddItem(ItemInstance, amount);
        if(ItemInstance.itemCategory == ItemCategory.WEAPON)
        {
            other.GetComponentInChildren<WeaponHolder>().equippedWeapon.weaponStats.totalBullets += pickupItem.amountValue;
        }
        Destroy(gameObject);
    }
}
