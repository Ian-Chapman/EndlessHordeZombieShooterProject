using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Weapon", menuName = "Items/Weapon", order = 2)]
public class WeaponScriptable : EquipableScriptable
{
    Animator playerAnimator;

    public WeaponStats weaponStats;

    public void Start()
    {
       
    }

    public override void UseItem(PlayerController playerController)
    {
        if (Equipped)
        {
            playerController.weaponHolder.UnEquipWeapon();
            //unequip from inventory
            //and controller
            //playerController.weaponHolder.UnEquipItem();
        }
        else
        {
            playerController.weaponHolder.EquipWeapon(this);
            PlayerEvents.InvokeOnWeaponEquipped(itemPrefab.GetComponent<WeaponComponent>());
            //invoke on weapon equipped from player events here for inventory
            //equip weapon from weapon holder on player controller
        }

        base.UseItem(playerController);
    }
}
