using Cinemachine;
using Spine.Unity;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.EnhancedTouch;


public class CatController : MonoBehaviour
{
    [HideInInspector]
    public Rigidbody2D rb;
    [HideInInspector]
    public BoxCollider2D boxcollider;
    public LayerMask jumpableGround;
    public LayerMask climbWall;
    public PlayerInput input;
    public Vector2 inputMove;
    [HideInInspector]
    public CatStateMachine stateMachine;
    public CatStateID initState = CatStateID.Idle;


    public float jumpHeight = 3.5f;
    public float speed = 10;
    public float speedJump = 10;
    public float speedClimb;
    [SerializeField]
    public float speedSlide;

    public bool isClimbing = false;
    public bool isJumping;
    //private float xJump = 0;
    //private float timeFalling = 0;
    public AnimationController controller;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        boxcollider = GetComponent<BoxCollider2D>();
        EnhancedTouchSupport.Enable();
        if (DataManager.HasInstance)
        {
            jumpHeight = DataManager.Instance.Config.jumpHeight;
            speed = DataManager.Instance.Config.speed;
            speedJump = DataManager.Instance.Config.speedJump;
            speedClimb = DataManager.Instance.Config.speedClimb;
            speedSlide = DataManager.Instance.Config.speedSlide;
        }
        stateMachine = new CatStateMachine(this);
        stateMachine.RegisterState(new CatStateIdle());
        stateMachine.RegisterState(new CatStateMove());
        stateMachine.RegisterState(new CatStateJump());
        stateMachine.RegisterState(new CatStateClimbing());
        stateMachine.ChangeState(initState);
    }
    // Update is called once per frame
    void Update()
    {
        inputMove = input.actions["Move"].ReadValue<Vector2>().normalized;
        if (!isClimbing)
        {
            if (inputMove.x > 0)
            {
                transform.rotation = Quaternion.Euler(0, 0, 0);
            }
            else if (inputMove.x < 0)
            {
                transform.rotation = Quaternion.Euler(0, 180, 0);
            }
        }
        if (stateMachine != null)
        {
            stateMachine.UpdateInState();
        }
        Debug.Log(IsTouchingWall());
        #region old_code
        //float x = inputMove.x * speed;
        //float y = 0;
        //float jspeed = 1;

        //if (!IsGround())
        //{
        //    if (DataManager.HasInstance)
        //    {
        //        rb.sharedMaterial = DataManager.Instance.Config.slip;
        //    }
        //}
        //else
        //{
        //    rb.sharedMaterial = null;
        //    isClimbing = false;
        //}
        //if (!IsTouchingWall())
        //{
        //    isClimbing = false;
        //}
        //if (IsClimbing())
        //{
        //    isClimbing = true;   
        //}
        //if (isClimbing)
        //{
        //    y = inputMove.y * speedClimb;
        //    if (input.actions["Jump"].triggered && (spriteRenderer.flipX?(x>0):(x<0)))
        //    {
        //        y = jumpHeight;
        //        rb.gravityScale = 3;
        //        isClimbing = false;
        //    }
        //    else
        //    {
        //        rb.gravityScale = 0;
        //        x = 0;
        //    }


        //    rb.velocity = Vector3.zero;
        //}
        //else
        //{
        //    rb.gravityScale = 3;
        //    if (input.actions["Jump"].triggered && IsGround())          //Jump
        //    {
        //        y = jumpHeight;
        //    }
        //}


        //if (rb.velocity.y != 0 && !IsGround())
        //{
        //    //x = xJump;                                              //can't turn when in the air
        //    jspeed = speedJump / 10;
        //}

        /*
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
        }*/
        //rb.velocity = new Vector2(x * jspeed, isClimbing?(y-speedSlide): rb.velocity.y + y);
        //Debug.Log(rb.velocity);
        #endregion
    }
    private void FixedUpdate()
    {
        if (stateMachine != null)
        {
            stateMachine.FixedUpdateInState();
        }
        
    }
    #region condition
    public bool IsGround()
    {
        return Physics2D.BoxCast(boxcollider.bounds.center, boxcollider.bounds.size, 0, Vector2.down, 0.02f, jumpableGround);
    }
    public bool IsClimbing()
    {
        return IsTouchingWall() && !IsGround() && rb.velocity.y < 0;

    }

    public bool IsTouchingWall()
    {
        return Physics2D.BoxCast(boxcollider.bounds.center, boxcollider.bounds.size, 0, transform.rotation.y!=0 ? Vector2.left : Vector2.right, 0.05f, climbWall);
    }
    #endregion
}
