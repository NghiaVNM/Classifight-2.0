using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using Unity.Netcode;
using UnityEngine;

public class PlayerNetwork : NetworkBehaviour
{
    public float currentHealth;
    private Animator animator;
    public float moveSpeed = 4f;
    private Rigidbody2D rb;
    private SpriteRenderer sprite;
    private MovementState state;
    private enum MovementState { idle, running, jumping, attacking, back, idleback, attackingback, jumpingback }
    private bool checkJump = false;
    public Transform attackPoint;
    public LayerMask enemyLayers;
    public float attackRange = 0.5f;
    public int attackDamage = 20;
    public float attackRate = 2f;
    float nextAttackTime = 0f;
    public GameObject hitBox;

    private void Start()
    {
        currentHealth = 100;
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        sprite = GetComponent<SpriteRenderer>();
        hitBox = GameObject.FindGameObjectWithTag("attackpoint");
    }

    private void Update()
    {
        if (!IsOwner) return;

        if (currentHealth <= 0)
        {
            Die();
            return;
        }

        float dirX = Input.GetAxisRaw("Horizontal");
        rb.velocity = new Vector2(dirX * moveSpeed, rb.velocity.y);

        checkJump = Input.GetKeyDown(KeyCode.W);
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

        if (Time.time >= nextAttackTime)
        {
            if (Input.GetKeyDown(KeyCode.J))
            {
                ServerAttackServerRpc();
                nextAttackTime = Time.time + 1f / attackRate;
            }
        }
    }
    void Attack()
    {
        Collider2D[] hitColliders = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, enemyLayers);

        foreach (Collider2D hitCollider in hitColliders)
        {
            if (hitCollider.TryGetComponent<PlayerNetwork>(out PlayerNetwork playerNetwork))
            {
                if (playerNetwork.IsOwner)
                {
                    playerNetwork.TakeDamage(attackDamage);
                    playerNetwork.RpcTakeDamageClientRpc(attackDamage);
                }
            }
        }
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        animator.SetTrigger("Hurt");

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    async void Die()
    {
        Debug.Log("Player Died");
        animator.SetTrigger("Hurt");
        animator.SetBool("isDead", true);
        await Task.Delay(500);
        GetComponent<Collider2D>().enabled = false;
        this.enabled = false;
    }

    [ServerRpc]
    private void ServerAttackServerRpc()
    {
        Attack();
        RpcAttackClientRpc();
    }

    [ClientRpc]
    private void RpcAttackClientRpc()
    {
        Attack();
    }

    [ClientRpc]
    private void RpcTakeDamageClientRpc(int damage)
    {
        TakeDamage(damage);
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
    void OnDrawGizmosSelected()
    {
        if (attackPoint == null)
        { return; }
        Gizmos.DrawWireSphere(attackPoint.position, attackRange);
    }
}
