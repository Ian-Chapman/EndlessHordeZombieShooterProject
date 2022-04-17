using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Cure", menuName = "Items/Cure", order = 3)]
public class CureConsumableScriptable : ConsumableScriptable
{
    public override void UseItem(PlayerController playerController)
    {
        if (playerController.healthComponent.CurrentHealth >= playerController.healthComponent.MaxHealth)
        { return; }

        playerController.healthComponent.HealDamage(effect);

        base.UseItem(playerController);
    }



}
