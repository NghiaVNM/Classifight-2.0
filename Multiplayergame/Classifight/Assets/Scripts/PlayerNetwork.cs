using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;

public class PlayerNetwork : NetworkBehaviour
{
    private Animator animator;
    public float moveSpeed = 4f;
    private Rigidbody2D rb;
    private SpriteRenderer sprite;
    private MovementState state;
    private enum MovementState { idle, running, jumping, attacking, back, idleback, attackingback, jumpingback}
    private Animator anim;
    private bool checkJump = false;
    private bool right = true;

    private void Start()
    {
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        sprite = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();
    }

    private void Update()
    {
        if (!IsOwner) return;
        float dirX = Input.GetAxisRaw("Horizontal");
        rb.velocity = new Vector2(dirX * 7f, rb.velocity.y);
        checkJump = (Input.GetKeyDown(KeyCode.W));
        if (checkJump)
        {
            rb.velocity = new Vector2(rb.velocity.x, 15f);
            if (right)
                state = MovementState.jumping;
            else
                state = MovementState.jumpingback;
        }
        else 
        {
            if (dirX > 0f)
            {
                    state = MovementState.running;
                    right = true;
            }
            else if (dirX < 0f)
            {
                state = MovementState.back;
                right = false;
            }
            else
            {
                if (right)
                    state = MovementState.idle;
                else
                    state = MovementState.idleback;
            }

            if (Input.GetKey(KeyCode.J))
            {
                if (right)
                    state = MovementState.attacking;
                else
                    state = MovementState.attackingback;
            } 
        }
        anim.SetInteger("state", (int)state);
    }

}
