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
    private float attackDelay = 0.3f,walkSpeed = 5f;

    const string PLAYER_IDLE = "Player_idle", PLAYER_RUN = "Player_run", PLAYER_JUMP = "Player_jump", 
        PLAYER_ATTACK = "Player_attack", PLAYER_AIR_ATTACK = "Player_air_attack";

    void Start()
    {
        animator = GetComponent<Animator>();
        rb2d = GetComponent<Rigidbody2D>();
        groundMask = 1 << LayerMask.NameToLayer("Ground");
        animator.Play("player_idle");
    }
    void ChangeAnimationState(string newState)
    {
        if (currentState == newState) return;
        animator.Play(newState);
        currentState = newState;
    }
}
