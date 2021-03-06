using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AK47Component : WeaponComponent
{

    Vector3 hitLocation;

    protected override void FireWeapon()
    {
        
        //player cannot run and shoot at same time
        if (weaponStats.bulletsInClip > 0 && !isReloading && !weaponHolder.playerController.isRunning)
        {
            base.FireWeapon();
            if(firingEffect)
            {
                firingEffect.Play();
                //sounds
                
                //Debug.Log("")
            }
            akFire.Play();

            Ray screenRay = mainCamera.ScreenPointToRay(new Vector3(Screen.width / 2, Screen.height / 2, 0));
            if (Physics.Raycast(screenRay, out RaycastHit hit, weaponStats.fireDistance, weaponStats.weaponHitLayers))
            {
                hitLocation = hit.point;
                //this might be a good spot to add a bullet decal/bullet hole

                //zombies take damage on raycast hit
                DealDamage(hit);

                Vector3 hitDirection = hit.point - mainCamera.transform.position;
                Debug.DrawRay(mainCamera.transform.position, hitDirection.normalized * weaponStats.fireDistance, Color.red, 1);

            }
            print("Bullet count: " + weaponStats.bulletsInClip);
            
        }
        else if (weaponStats.bulletsInClip <=0)
        {
            weaponHolder.StartReloading();
            reloadSound.Play();
        }
    }

    void DealDamage(RaycastHit hitInfo)
    {
        IDamageable damageable = hitInfo.collider.GetComponent<IDamageable>();
        damageable?.TakeDamage(weaponStats.damage);
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(hitLocation, 0.2f);
    }
}
