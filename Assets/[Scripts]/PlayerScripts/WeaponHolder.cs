using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class WeaponHolder : MonoBehaviour
{
    public GameObject weaponInfoPanel;

    [Header("WeaponToSpawn"), SerializeField]
    GameObject weaponToSpawn;

    public Dictionary<WeaponType, WeaponStats> WeaponAmmoData;

    public PlayerController playerController;
    Animator animator;
    Sprite crosshairImage;

    public WeaponComponent equippedWeapon;
    public WeaponAmmoUI weaponAmmoUI;

    [SerializeField]
    GameObject weaponSocketLocation;
    [SerializeField]
    Transform gripIKSocketLocation;

    bool wasFiring = false;
    bool firingPressed = false;

    GameObject spawnedWeapon;
    public WeaponScriptable startingWeaponScriptable;


    public readonly int HasWeaponHash = Animator.StringToHash("HasWeapon");
    public readonly int isFiringHash = Animator.StringToHash("IsFiring");
    public readonly int isReloadingHash = Animator.StringToHash("IsReloading");

    // Start is called before the first frame update
    void Start()
    {
        playerController = GetComponent<PlayerController>();
        animator = GetComponent<Animator>();
        WeaponAmmoData = new Dictionary<WeaponType, WeaponStats>();
        playerController.inventory.AddItem(startingWeaponScriptable, 1);
        WeaponAmmoData.Add(startingWeaponScriptable.weaponStats.weaponType, startingWeaponScriptable.weaponStats);

        weaponInfoPanel.SetActive(false);

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnAnimatorIK(int layerIndex)
    {
        if(equippedWeapon)
        {
            animator.SetIKPositionWeight(AvatarIKGoal.LeftHand, 1);
            animator.SetIKPosition(AvatarIKGoal.LeftHand, gripIKSocketLocation.transform.position);
        }

    }

    public void OnFire(InputValue value)
    {
        firingPressed = value.isPressed;
        if(!equippedWeapon)
        { return; }

        if (firingPressed)
        {
            StartFiring();
        }
        else
        {
            //print("Stop firing");
            StopFiring();
        }

        
        //call shooting from weapon holder here
    }


    public void StartFiring()
    {
        if (equippedWeapon.weaponStats.bulletsInClip <= 0)
        {
            StartReloading();
            return;
        }
        animator.SetBool(isFiringHash, true);
        playerController.isFiring = true;
        equippedWeapon.StartFiringWeapon();
    }

    public void StopFiring()
    {
        playerController.isFiring = false;
        animator.SetBool(isFiringHash, false);
        equippedWeapon.StopFiringWeapon();
    }


    //input based, caused by a button press
    public void OnReload(InputValue value)
    {
        if(!equippedWeapon)
        { return; }

        playerController.isReloading = value.isPressed;
        //animator.SetBool(isReloadingHash, playerController.isReloading);
        StartReloading();
    }


    //the action of reloading 
    public void StartReloading()
    {
        if (equippedWeapon.isReloading || equippedWeapon.weaponStats.bulletsInClip == equippedWeapon.weaponStats.clipSize) return;
        
        if (playerController.isFiring)
        {
            StopFiring();
        }

        if (equippedWeapon.weaponStats.totalBullets <= 0) 
            return;
        
        animator.SetBool(isReloadingHash, true);
        equippedWeapon.StartReloading();

        WeaponAmmoData[equippedWeapon.weaponStats.weaponType] = equippedWeapon.weaponStats;

        InvokeRepeating(nameof(StopReloading), 0, 0.1f);
    }

    public void StopReloading()
    {
        if (animator.GetBool(isReloadingHash)) return;

        playerController.isReloading = false;
        equippedWeapon.StopReloading();
        animator.SetBool(isReloadingHash, false);
        CancelInvoke(nameof(StopReloading));
    }

    public void EquipWeapon(WeaponScriptable weaponScriptable)
    {
        if(!weaponScriptable)
        {return;}

        spawnedWeapon = Instantiate(weaponScriptable.itemPrefab, weaponSocketLocation.transform.position, weaponSocketLocation.transform.rotation, weaponSocketLocation.transform);

        if (!spawnedWeapon)
        {return;}

        

        equippedWeapon = spawnedWeapon.GetComponent<WeaponComponent>();
        if(!equippedWeapon)
        {return;}

        equippedWeapon.Initialize(this, weaponScriptable);

        if (WeaponAmmoData.ContainsKey(equippedWeapon.weaponStats.weaponType))
        {
            equippedWeapon.weaponStats = WeaponAmmoData[equippedWeapon.weaponStats.weaponType];
        }

        PlayerEvents.InvokeOnWeaponEquipped(equippedWeapon);

        gripIKSocketLocation = equippedWeapon.gripLocation;

        weaponAmmoUI.OnWeaponEquipped(equippedWeapon);

        animator.SetBool(HasWeaponHash, true);
        playerController.HasWeapon = true;
        weaponInfoPanel.SetActive(true);

        // do IK stuff here if other weapons are one handed, etc.
        // set stuff in animator for other weapons
    }

    public void UnEquipWeapon()
    {
        if(!equippedWeapon)
        { return; }
        
        if(WeaponAmmoData.ContainsKey(equippedWeapon.weaponStats.weaponType))
        {
            WeaponAmmoData[equippedWeapon.weaponStats.weaponType] = equippedWeapon.weaponStats;
        }

        animator.SetBool(HasWeaponHash, false);
        playerController.HasWeapon = false;
        weaponInfoPanel.SetActive(false);
        Destroy(equippedWeapon.gameObject);
        equippedWeapon = null;
    }
}
