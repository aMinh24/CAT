using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.EnhancedTouch;


public class CatMovement : MonoBehaviour
{
    private Rigidbody2D rb;
    private BoxCollider2D boxcollider;
    [SerializeField]
    private LayerMask jumpableGround;
    public PlayerInput input;

    
    private Animator animator;
    private SpriteRenderer spriteRenderer;

    private float jumpHeight = 3.5f;
    private float speed = 10;
    private float speedJump = 10;
    private float gravityCat;

    private float xJump = 0;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        boxcollider = GetComponent<BoxCollider2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        EnhancedTouchSupport.Enable();
        
        
        if (DataManager.HasInstance)
        {
            jumpHeight = DataManager.Instance.Config.jumpHeight;
            speed = DataManager.Instance.Config.speed;
            speedJump = DataManager.Instance.Config.speedJump;
            gravityCat = DataManager.Instance.Config.gravity;
        }
    }
    // Update is called once per frame
    void Update()
    {
        float x = input.actions["Move"].ReadValue<Vector2>().normalized.x*speed;  
        float y = 0;
        float jspeed = 1;
        //if (IsGround())
        //{
        //    isJumping = false;
        //    xJump = x;
        //}
        if (input.actions["Jump"].triggered && IsGround())          //Jump
        {
            y = jumpHeight;
            xJump = x;
        }
        if (!IsGround())
        {
            if ( DataManager.HasInstance)
            {
                rb.sharedMaterial = DataManager.Instance.Config.slip;
            }

        }
        else rb.sharedMaterial = null;


        if (rb.velocity.y != 0 && !IsGround())
        {
            x = xJump;                                              //can't turn when in the air
            jspeed = speedJump / 10;
        }


        Debug.Log("is ground " + IsGround());
        if (x!= 0 && IsGround())
        {
            animator.SetBool("isWalking", true);
            if (x > 0)
            {
                spriteRenderer.flipX = false;
            }
            else if (x < 0)
            {
                spriteRenderer.flipX = true;
            }
        }
        else
        {
            animator.SetBool("isWalking", false);
            
        }
        //y += gravity();
        Vector2 v = new Vector2((rb.velocity.x+x)>speed ?speed : (rb.velocity.x + x)<-speed?-speed : (rb.velocity.x + x), rb.velocity.y+y);    //control velocity x in range(-10;10);
        rb.velocity = new Vector2 (v.x* jspeed, v.y);
        Debug.Log(rb.velocity);
        
    }
    

    private bool IsGround()
    {
        return Physics2D.BoxCast(boxcollider.bounds.center, boxcollider.bounds.size, 0, Vector2.down, 0.03f, jumpableGround);
    }
    private bool IsClimbing()
    {
        return IsTouchingWall() &&!IsGround();

    }
    private bool IsTouchingWall()
    {
        return Physics2D.BoxCast(boxcollider.bounds.center, boxcollider.bounds.size, 0, spriteRenderer.flipX ? Vector2.left : Vector2.right, 0.03f, jumpableGround);
    }
}
