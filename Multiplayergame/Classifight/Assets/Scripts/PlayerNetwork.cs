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
    private enum MovementState { idle, running, jumping, attacking}
    private Animator anim;
    private bool checkJump = false;

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
            rb.velocity = new Vector2(rb.velocity.x, 7f);
            state = MovementState.jumping;
        }
        else 
        {
            if (dirX > 0f)
            {
                state = MovementState.running;
                sprite.flipX = false;
            }
            else if (dirX < 0f)
            {
                state = MovementState.running;
                sprite.flipX = true;
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
        anim.SetInteger("state", (int)state);
    }

}
