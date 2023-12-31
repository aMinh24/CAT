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
    public bool isJumping;
    public AnimationController controller;
    public ParticleSystem dustParticle;
    public ParticleSystem fallParticle;
    public bool freezing;
    private void Awake()
    {
        freezing = false;
        Time.timeScale = 1.0f;
    }
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
        stateMachine.RegisterState(new CatStateInAir());
        stateMachine.RegisterState(new CatStateLanding());
        stateMachine.RegisterState(new CatStateClimbing());
        stateMachine.ChangeState(initState);
    }
    // Update is called once per frame
    void Update()
    {
        if(freezing) { inputMove = Vector2.zero; }
        else
        {
            inputMove = input.actions["Move"].ReadValue<Vector2>().normalized;
        }
        if (stateMachine != null)
        {
            stateMachine.UpdateInState();
        }
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
        RaycastHit2D hit = Physics2D.Raycast(transform.position - new Vector3(0.35f,0,0), Vector2.down, 0.2f,jumpableGround);
        RaycastHit2D hit2 = Physics2D.Raycast(transform.position + new Vector3(0.35f, 0, 0), Vector2.down, 0.2f, jumpableGround);
        return Physics2D.BoxCast(boxcollider.bounds.center, boxcollider.bounds.size, 0, Vector2.down, 0.05f, jumpableGround) && (hit ||hit2);
    }
    public bool IsLanding()
    {
        RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.down, 1f, jumpableGround);

        return Physics2D.BoxCast(boxcollider.bounds.center, boxcollider.bounds.size, 0, Vector2.down, 0.05f, jumpableGround) && hit;
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
