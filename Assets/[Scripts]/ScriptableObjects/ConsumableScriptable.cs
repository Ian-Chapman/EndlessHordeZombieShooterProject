using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu (fileName = "Consumable", menuName = "Items/Consumable", order = 1)]
public class ConsumableScriptable : ItemScript
{
    public int effect = 0;

    public override void UseItem(PlayerController playerController)
    {
        //check to see if player is at max health, return
        //Heal player with potion/first aid kit

        SetAmount(amountValue - 1);
        //if item is used, remove it. If item value is below 0, remove it from the inventory
        if (amountValue <= 0)
        {
            DeleteItem(playerController);
        }
    }
}

