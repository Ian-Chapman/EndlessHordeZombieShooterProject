using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum WeaponType
{
    None,
    Pistol,
    MachineGun
}

public enum WeaponFiringPattern
{
    SemiAuto, FullAuto, ThreeShotBurst, FiveShotBurst
}

[System.Serializable]
public struct WeaponStats
{
    public WeaponType weaponType;
    public string weaponName;
    public float damage;
    public int bulletsInClip;
    public int clipSize;
    public float fireStartDelay;
    public float fireRate;
    public WeaponFiringPattern weaponFiringPattern;
    public float fireDistance;
    public bool repeating;
    public LayerMask weaponHitLayers;
    public int totalBullets;
}



public class WeaponComponent : MonoBehaviour
{

    public Transform gripLocation;
    public Transform firingEffectLocation; //barrel of gun

    protected WeaponHolder weaponHolder;
    [SerializeField]
    protected ParticleSystem firingEffect; //smoke and muzzleflash

    [SerializeField]
    public WeaponStats weaponStats;

    public AudioSource akFire;
    public AudioSource reloadSound;

    public bool isFiring = false;
    public bool isReloading = false;
    protected Camera mainCamera;

    private void Awake()
    {
        mainCamera = Camera.main;
        //firingEffect.transform.parent = firingEffectLocation;
        firingEffect.Stop();
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Initialize(WeaponHolder _weaponHolder, WeaponScriptable weaponScriptable)
    {
        weaponHolder = _weaponHolder;

        if(weaponScriptable)
        {
            weaponStats = weaponScriptable.weaponStats;
            
        }

    }


    //decide if weapon is automatic or semi-automatic here
    public virtual void StartFiringWeapon()
    {
        isFiring = true;
        if(weaponStats.repeating)
        {
            InvokeRepeating(nameof(FireWeapon), weaponStats.fireStartDelay, weaponStats.fireRate);

        }
        else
        {
            FireWeapon();
        }
    }

    public virtual void StopFiringWeapon()
    {
        isFiring = false;
        CancelInvoke(nameof(FireWeapon));

        if(firingEffect.isPlaying)
        {
            firingEffect.Stop();
        }
        akFire.Stop();
    }

    protected virtual void FireWeapon()
    {
        print("Firing Weapon!");
        weaponStats.bulletsInClip--;
    }


    //ammo counts and particle effects
    public virtual void StartReloading()
    {
        isReloading = true;
        ReloadWeapon();
        reloadSound.Play();
    }

    public virtual void StopReloading()
    {
        isReloading = false;
    }

    protected virtual void ReloadWeapon()
    {
        //if theres a firing effect, stop it here
        if (firingEffect.isPlaying)
        {
            firingEffect.Stop();
        }

        int bulletsToReload = weaponStats.totalBullets - (weaponStats.clipSize - weaponStats.bulletsInClip);
        
        if (bulletsToReload > 0)
        {
            weaponStats.totalBullets = bulletsToReload;
            weaponStats.bulletsInClip = weaponStats.clipSize;
        }
        else
        {
            weaponStats.bulletsInClip += weaponStats.totalBullets;
            weaponStats.totalBullets = 0;
        }
    }

}
