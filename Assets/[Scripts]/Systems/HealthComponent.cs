using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

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
            SceneManager.LoadScene("GameOver");
        }
    }

    public virtual void HealDamage(int value)
    {
        if (currentHealth < maxHealth)
        {
            //heal without going past max HP
            currentHealth = Mathf.Clamp(currentHealth + value, 0, maxHealth);
        }
    }

}
