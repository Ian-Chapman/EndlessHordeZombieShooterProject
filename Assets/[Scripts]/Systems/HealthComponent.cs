using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthComponent : MonoBehaviour, IDamageable
{

    [SerializeField]
    public float currentHealth;
    public float CurrentHealth => currentHealth;

    [SerializeField]
    public float maxHealth;
    public float MaxHealth => maxHealth;
    // Start is called before the first frame update
    protected virtual void Start()
    {
        currentHealth = MaxHealth;
        //countdownTimerComponent = GameObject.FindGameObjectsWithTag("Zombie").
    }

    public virtual void Destroy()
    {
        Destroy(gameObject);
    }

    public virtual void TakeDamage(float damage)
    {
        currentHealth -= damage;
        if (currentHealth <= 0)
        {
            Destroy();
        }
    }


}
