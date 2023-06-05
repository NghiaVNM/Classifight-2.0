using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private Rigidbody2D rg;
    private SpriteRenderer sprite;
    private Animator anim;
    private BoxCollider2D coll;
    [SerializeField] private LayerMask jumpableGround;

    private float dirX = 0f;
    private float MoveSpeed = 7f;
    [SerializeField] private float JumpForce = 14f;
    private bool checkJump = false;
    private bool checkAttack = false;
    private MovementState state;
    [SerializeField] private AudioSource soundMoving;
    [SerializeField] private AudioSource soundAttack;
    [SerializeField] private AudioSource soundJump;

    private enum MovementState { idle, running, jumping, attacking, jmpattacking }
    // Start is called before the first frame update
    void Start()
    {
        rg = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        coll = GetComponent<BoxCollider2D>();
        sprite = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        dirX = Input.GetAxisRaw("Horizontal");
        rg.velocity = new Vector2(dirX * MoveSpeed, rg.velocity.y);
        checkAttack = Input.GetKeyDown(KeyCode.J);
        checkJump = Input.GetKeyDown(KeyCode.Space);
        if (checkJump && IsGrounded())
        {
            rg.velocity = new Vector2(dirX * rg.velocity.x, JumpForce);
            if (checkAttack)
            {
                soundJump.Play();
                state = MovementState.jmpattacking;
            }
            else
            {
                soundJump.Play();
                state = MovementState.jumping;
            }
        }
        else
        {
            if (checkAttack)
            {
                if (IsGrounded())
                { 
                    soundAttack.Play();
                    state = MovementState.attacking;
                }
                else
                {   
                    soundAttack.Play();
                    soundJump.Play();
                    state = MovementState.jmpattacking;
                }
            }
            else
            {
                Move();
            }
        }
        anim.SetInteger("state", (int)state);
    }

    private void Move()
    {
        if (Input.GetKeyDown(KeyCode.J))
        {
            soundAttack.Play();
            state = MovementState.attacking;
        }
        if (dirX > 0f)
        {
            soundMoving.Play();
            state = MovementState.running;
            sprite.flipX = false;
        }
        else if (dirX < 0f)
        {
            soundMoving.Play(); 
            state = MovementState.running;
            sprite.flipX = true;
        }
        else
        {
            state = MovementState.idle;
        }

        if (rg.velocity.y > .1f || rg.velocity.y < -.1f)
        {
            state = MovementState.jumping;
        }

    }
    private bool IsGrounded()
    {
        return Physics2D.BoxCast(coll.bounds.center, coll.bounds.size, 0f, Vector2.down, .1f, jumpableGround);
    }
}
