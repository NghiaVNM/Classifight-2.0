using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;

public class Enemy : NetworkBehaviour
{
    public int maxHealth = 100;
    public int currentHealth;
    public HealthBar healthBar;
    public Animator animator;

    void Start()
    {
        currentHealth = maxHealth;    
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        healthBar.SetHealth(currentHealth);
        animator.SetTrigger("Hurt");
        if (currentHealth <= 0) 
        {
            Die();
        }
    }
    void Die()
    {
        Debug.Log("Enemy Died");
        animator.SetBool("IsDead", true);
        GetComponent<Collider2D>().enabled = false;
        this.enabled = false;
    }
    
}
