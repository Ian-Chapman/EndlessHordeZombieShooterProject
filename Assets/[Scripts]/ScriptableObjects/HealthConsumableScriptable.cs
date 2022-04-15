using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "FirstAidKit", menuName = "Items/FirstAidKit", order = 1)]
public class HealthConsumableScriptable : ConsumableScriptable
{
    public override void UseItem(PlayerController playerController)
    {
        if (playerController.healthComponent.CurrentHealth >= playerController.healthComponent.MaxHealth)
        { return; }

        playerController.healthComponent.HealDamage(effect);

        base.UseItem(playerController);
    }


}
