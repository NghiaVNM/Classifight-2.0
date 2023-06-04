using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fighter : MonoBehaviour
{
    public enum PlayerType {
        HUMAN, AI
    }

    public static float maxHealth = 100f;

    public float life = maxHealth;
    public string fighterName;
    public Fighter opponent;

    public PlayerType player;
    public FighterState currentState = FighterState.Idel;

    protected Animator animator;
    private Rigidbody2D myBody;

    void Start()
    {
        myBody = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    public void UpdateHumanInput() 
    {
        // fix
        if (Input.GetAxis("Horizontal") > 0.1 || Input.GetAxis("Horizontal") < -0.1)
        {
            animator.SetBool("Walk", true);
        }
        else
        {
            animator.SetBool("Walk", false);
        }

        if (Input.GetKeyDown(KeyCode.W))
        {
            animator.SetTrigger("Jump");
        }

        if (Input.GetKeyDown(KeyCode.J))
        {
            animator.SetTrigger("Attack");
        }
    }
  
    void Update()
    {
        animator.SetFloat("health", lifePercent);

        if (player == PlayerType.HUMAN)
        {
            UpdateHumanInput();
        }

        if (opponent != null) 
        {
            animator.SetFloat("opponent_health", opponent.lifePercent);
        }
        else
        {
            animator.SetFloat("opponent_health", 1);
        }
    }

    public float lifePercent
    {
        get 
        {
            return life / maxHealth;
        }   
    }

    public Rigidbody2D body 
    {
        get
        {
            return this.myBody;
        }
    }
}

