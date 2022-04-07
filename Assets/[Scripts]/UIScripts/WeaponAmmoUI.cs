using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class WeaponAmmoUI : MonoBehaviour
{
    [SerializeField]
    TextMeshProUGUI weaponNameText;
    [SerializeField]
    TextMeshProUGUI currentBulletCountText;
    [SerializeField]
    TextMeshProUGUI totalBulletCountText;

    [SerializeField] 
    WeaponComponent weaponComponent;
    /// <summary>
    /// Set up events for on weapon equipped to handle the weapon components we've grabbed
    /// </summary>

    private void OnEnable()
    {
        PlayerEvents.OnWeaponEquipped += OnWeaponEquipped;
    }

    private void OnDisable()
    {
        PlayerEvents.OnWeaponEquipped -= OnWeaponEquipped;
    }

    public void OnWeaponEquipped(WeaponComponent _weaponComponent)
    {
        weaponComponent = _weaponComponent;
    }

    // Update is called once per frame
    void Update()
    {
        if (!weaponComponent)
        {
            return;
        }

        weaponNameText.text = weaponComponent.weaponStats.weaponName;
        currentBulletCountText.text = weaponComponent.weaponStats.bulletsInClip.ToString();
        totalBulletCountText.text = weaponComponent.weaponStats.totalBullets.ToString();
    }
}
