using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class player_life : MonoBehaviour
{
    private Animator anim;
    private Rigidbody2D rg;
    public int maxHealth = 100;
    public int currentHealth;
    public HealthBar1 healthBar;
    [SerializeField] private AudioSource soundDie;
    
    private void Start()
    {
        anim = GetComponent<Animator>();
        rg = GetComponent<Rigidbody2D>();
        currentHealth = maxHealth;
        healthBar.SetMaxHealth(maxHealth);

    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("fallout"))
        {
            currentHealth = 0;
            healthBar.SetHealth(currentHealth);
            
            Die();
        }
    }
    private void Die()
    {
        soundDie.Play();
        rg.bodyType = RigidbodyType2D.Static;
        anim.SetTrigger("Death");
    }
    private void RestartLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

}
