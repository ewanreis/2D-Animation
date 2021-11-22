using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerController : MonoBehaviour
{
    Animator animator;
    private string currentState;
    private float xAxis, yAxis, jumpForce = 850f;
    private Rigidbody2D rb2d;
    private int groundMask;
    private bool isGrounded, isAttackPressed, isAttacking, isJumpPressed;

    [SerializeField]
    private float attackDelay = 0.3f, walkSpeed = 5f;

    const string PLAYER_IDLE = "Player_idle", PLAYER_RUN = "Player_run", PLAYER_JUMP = "Player_jump",
        PLAYER_ATTACK = "Player_attack", PLAYER_AIR_ATTACK = "Player_air_attack";

    void Start()
    {
        animator = GetComponent<Animator>();
        rb2d = GetComponent<Rigidbody2D>();
        groundMask = 1 << LayerMask.NameToLayer("Ground");
        animator.Play("player_idle");
    }
    void Update()
    {
        xAxis = Input.GetAxisRaw("Horizontal");
        if (Input.GetKeyDown(KeyCode.Space))
            isJumpPressed = true;
        if (Input.GetKeyDown(KeyCode.RightControl))
            isAttackPressed = true;
    }
    private void FixedUpdate()
    {
        RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.down, 0.1f, groundMask);
        if (hit.collider != null)
            isGrounded = true;
        else
            isGrounded = false;
        Vector2 vel = new Vector2(0, rb2d.velocity.y);
        if (xAxis < 0)
        {
            vel.x = -walkSpeed;
            transform.localScale = new Vector2(-1, 1);
        } else if (xAxis > 0)
        {
            vel.x = walkSpeed;
            transform.localScale = new Vector2(1, 1);
        } else
            vel.x = 0;
        if (isJumpPressed && isGrounded)
        {
            rb2d.AddForce(new Vector2(0, jumpForce));
            isJumpPressed = true;
        }
        if (isAttackPressed)
        {
            isAttackPressed = false;
            if (!isAttacking)
                isAttacking = true;
        }

    }
    void ChangeAnimationState(string newState)
    {
        if (currentState == newState) return;
        animator.Play(newState);
        currentState = newState;
    }
}
