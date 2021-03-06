using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    public bool isFiring;
    public bool isReloading;
    public bool isJumping;
    public bool isRunning;
    public bool isAiming;
    public bool inInventory;
    public bool HasWeapon;

    public InventoryComponent inventory;
    public WeaponHolder weaponHolder;
    public GameUIController uIController;
    public HealthComponent healthComponent;
    public CureComponent cureComponent;
    public GameObject characterOverlap;

    private void Awake()
    {
        if (inventory == null)
        {
            inventory = GetComponent<InventoryComponent>();
        }
        if (weaponHolder == null)
        {
            weaponHolder = GetComponent<WeaponHolder>();
        }
        if (healthComponent == null)
        {
            healthComponent = GetComponent<HealthComponent>();
        }
        if (cureComponent == null)
        {
            cureComponent = GetComponent<CureComponent>();
        }
    }

    public void OnInventory(InputValue value)
    {
        if (inInventory)
        {
            inInventory = false;
        }
        else
        {
            inInventory = true;
        }
        OpenInventory(inInventory);
    }

    private void OpenInventory(bool open)
    {
        if(open)
        {
            uIController.EnableInventoryMenu();
        }
        else
        {
            uIController.EnableGameMenu();
        }
    }
}
