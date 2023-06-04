using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using Unity.Netcode;
using UnityEngine;

public class PlayerNetwork : NetworkBehaviour
{
    public float currentHeal;
    private Animator animator;
    public float moveSpeed = 4f;
    private Rigidbody2D rb;
    private SpriteRenderer sprite;
    private MovementState state;
    private enum MovementState { idle, running, jumping, attacking, back, idleback, attackingback, jumpingback}
    private bool checkJump = false;
    public Transform attackPoint;
    public LayerMask enemyLayers;
    public float attackRange = 0.5f;
    public int attackDamage = 20;
    public float attackRate = 2f;
    float nextAttackTime = 0f;
    private void Start()
    {
        currentHeal = 100;
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        sprite = GetComponent<SpriteRenderer>();
    }

    private void Update()
    {
        if (!IsOwner) return;
        if (currentHeal <= 0) {
            Die();
            return;
        }
        float dirX = Input.GetAxisRaw("Horizontal");
        rb.velocity = new Vector2(dirX * 7f, rb.velocity.y);
        checkJump = (Input.GetKeyDown(KeyCode.W));
        if (checkJump)
        {
            rb.velocity = new Vector2(rb.velocity.x, 7f);
            state = MovementState.jumping;
        }
        else 
        {
            if (dirX > 0f)
            {
                state = MovementState.running;
                ServerFlipServerRpc(false);
            }
            else if (dirX < 0f)
            {
                state = MovementState.running;
                ServerFlipServerRpc(true);
            }
            else
            {
                state = MovementState.idle;
            }
            if (Input.GetKey(KeyCode.J))
            {
                state = MovementState.attacking;
            } 
        }
        animator.SetInteger("state", (int)state);

        if(Time.time >= nextAttackTime) 
        {
            if (Input.GetKeyDown(KeyCode.J))
            {
                Attack();
                nextAttackTime=Time.time+1f/attackRate;
            }
        }
    }

    void Attack()
    {
        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, enemyLayers);
        foreach(Collider2D enemy in hitEnemies)
        {
            enemy.GetComponent<Enemy>().TakeDamage(attackDamage);
        }
    }

    /*
    public void TakeDamage(int damage)
    {
        if (IsOwner) return;
        currentHealth -= damage;
        animator.SetTrigger("Hurt");
        if (currentHealth <= 0) 
        {
            Die();
        }
    }*/
    async void Die()
    {
        Debug.Log("Enemy Died");
        animator.SetTrigger("Hurt");
        animator.SetBool("isDead", true);
        await Task.Delay(500);
        GetComponent<Collider2D>().enabled = false;
        this.enabled = false;
        
    }
    [ServerRpc]
    private void ServerFlipServerRpc(bool flipX)
    {
        RpcFlipClientRpc(flipX);
    }

    [ClientRpc]
    private void RpcFlipClientRpc(bool flipX)
    {
        sprite.flipX = flipX;
    }
}
