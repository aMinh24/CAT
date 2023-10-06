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
    [SerializeField]
    private LayerMask climbWall;
    public PlayerInput input;


    private Animator animator;
    private SpriteRenderer spriteRenderer;

    private float jumpHeight = 3.5f;
    private float speed = 10;
    private float speedJump = 10;
    private float speedClimb;
    [SerializeField]
    private float speedSlide;

    private bool isClimbing = false;
    //private bool isJumping;
    //private float xJump = 0;
    //private float timeFalling = 0;
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
            speedClimb = DataManager.Instance.Config.speedClimb;
            speedSlide = DataManager.Instance.Config.speedSlide;
        }
    }
    // Update is called once per frame
    void Update()
    {
        float x = input.actions["Move"].ReadValue<Vector2>().normalized.x * speed;
        float y = 0;
        float jspeed = 1;

        if (!IsGround())
        {
            if (DataManager.HasInstance)
            {
                rb.sharedMaterial = DataManager.Instance.Config.slip;
            }
        }
        else
        {
            rb.sharedMaterial = null;
            isClimbing = false;
        }
        if (!IsTouchingWall())
        {
            isClimbing = false;
        }
        if (IsClimbing())
        {
            isClimbing = true;   
        }
        if (isClimbing)
        {
            y = input.actions["Move"].ReadValue<Vector2>().normalized.y * speedClimb;
            if (input.actions["Jump"].triggered && (spriteRenderer.flipX?(x>0):(x<0)))
            {
                y = jumpHeight;
                rb.gravityScale = 3;
                isClimbing = false;
            }
            else
            {
                rb.gravityScale = 0;
                x = 0;
            }
            
            
            rb.velocity = Vector3.zero;
        }
        else
        {
            rb.gravityScale = 3;
            if (input.actions["Jump"].triggered && IsGround())          //Jump
            {
                y = jumpHeight;
                //xJump = x;
            }
        }
        

        if (rb.velocity.y != 0 && !IsGround())
        {
            //x = xJump;                                              //can't turn when in the air
            jspeed = speedJump / 10;
        }

        
        //Debug.Log("is ground " + IsGround()+" wall "+ IsTouchingWall());
        if (x != 0)
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
        rb.velocity = new Vector2(x * jspeed, isClimbing?(y-speedSlide): rb.velocity.y + y);
        Debug.Log(rb.velocity);

    }


    private bool IsGround()
    {
        return Physics2D.BoxCast(boxcollider.bounds.center, boxcollider.bounds.size, 0, Vector2.down, 0.03f, jumpableGround);
    }
    private bool IsClimbing()
    {
        return IsTouchingWall() && !IsGround() && rb.velocity.y < 0;

    }

    private bool IsTouchingWall()
    {
        return Physics2D.BoxCast(boxcollider.bounds.center, boxcollider.bounds.size, 0, spriteRenderer.flipX ? Vector2.left : Vector2.right, 0.03f, climbWall);
    }
}