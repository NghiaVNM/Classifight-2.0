using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Enemytrain : MonoBehaviour
{
    private Animator animator;
    public int maxHealth = 100;
    public int currentHealth;
    public HealthBar1 healthBar;
    [SerializeField] private AudioSource soundHurt;
    [SerializeField] private AudioSource soundDie;

    void Start()
    {
        animator = GetComponent<Animator>();
        currentHealth = maxHealth;
        healthBar.SetMaxHealth(maxHealth);
    }
    public void TakeDamage(int damage)
    {

        currentHealth -= damage;
        healthBar.SetHealth(currentHealth);
        soundHurt.Play();
        animator.SetTrigger("Hurt");
        if (currentHealth <= 0)
        {
            Die();
        }
    }
    void Die()
    {
        soundDie.Play();
        animator.SetBool("IsDeath", true);
        GetComponent<Collider2D>().enabled = false;
        this.enabled = false;
    }
    private void RestartLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
