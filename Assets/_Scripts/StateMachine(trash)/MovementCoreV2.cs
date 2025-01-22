using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementCoreV2 : MonoBehaviour
{
    private Rigidbody rb;
    RaycastHit hit;

    [Header("Ray cast set up stuff")]
    [SerializeField] private float groundRayLenght;
    [SerializeField] private float groundRayHeight;
    [SerializeField] private float groundRayRadius;
    [SerializeField] private LayerMask groundLayer;

    [Header("Movement stats (the important part)")]
    [SerializeField] private float spring;
    [SerializeField] private float damp;
    [SerializeField] private float hoverHeight;

    [SerializeField] private float speed;

    [SerializeField] private float drag;
    [SerializeField] private float gravityMult;
    [SerializeField] private float maxFallSpeed;
    [SerializeField] private float minJumpTime;
    [SerializeField] float jumpTimer;

    [Header("Movement Data")]
    [SerializeField] private Vector3 moveVector;
    //[SerializeField] GeneralState currentState;
    //[SerializeField] Substate currentSubState;


    //fuck state machines i hate them
    PlayerBasestate _currentState;
    StateFactory _states;
    //state stuff
    //[SerializeField] private bool grounded;

    //getter and setter
    public PlayerBasestate CurrentState {get {return _currentState;} set {_currentState = value;}}

    
    public bool jumpNeed;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        Cursor.lockState = CursorLockMode.Locked;

        _states = new StateFactory(this);
        _currentState = _states.Grounded();
        _currentState.EnterState();
    }
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        //Debug.Log(inputHandler.MoveInput);
        _currentState.UpdateStates();


    }
    private void FixedUpdate()
    {

       _currentState.FixedUpdateStates();


        /*if (Grounded())
        {
            DoOnGround();

        }else
        {
            DoOnFall();
        } */      

        Vector3 velo = rb.velocity;
        velo = new Vector3(velo.x, 0f, velo.z);

        //APPLY FORCES
        rb.AddForce(((moveVector * speed) - velo) * drag / Time.fixedDeltaTime);

    }

    //states i guess
    void DoOnGround()
    {
        Bounce();
    }

    void DoOnFall()
    {
        if(rb.velocity.y >maxFallSpeed)
        {
            rb.velocity -= Vector3.down * Physics.gravity.y * Time.fixedDeltaTime * gravityMult;
        }
    }

    void WantToJump()
    {
        jumpNeed = true;
    }

    //cool bouncing script
    void Bounce()
    {

        Vector3 vel = rb.velocity;
        //Vector3 dir = transform.TransformDirection(Vector3.down);
        Vector3 dir = Vector3.down;
        float dirVel = Vector3.Dot(dir, vel);
        float x = hit.distance - hoverHeight;
        float springForce = (x * spring) - (dirVel * damp);

        rb.AddForce(dir * springForce);
    }

    public void MoveCharacter(Vector3 moveInput)
    {
        moveVector = new Vector3(moveInput.x,0,moveInput.z).normalized;
        
    }

    public void ClearMove()
    {
        moveVector = Vector3.zero;
    }

    public bool Grounded()
    {
        return Physics.SphereCast(transform.position + new Vector3(0, groundRayHeight + groundRayRadius, 0), groundRayRadius, Vector3.down, out hit, groundRayLenght, groundLayer);
        
    }

    //debug stuff, will not show/work in game (i hope so)
    private void OnDrawGizmos()
    {
        Debug.DrawRay(transform.position + new Vector3(0, groundRayHeight, 0), Vector2.down * groundRayLenght, Color.red);
        if (Physics.SphereCast(transform.position + new Vector3(0, groundRayHeight + groundRayRadius, 0), groundRayRadius, Vector3.down, out hit, groundRayLenght, groundLayer))
        {
            Gizmos.color = Color.yellow;
            Gizmos.DrawWireSphere(hit.point, groundRayRadius);
        }
        else
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(transform.position + new Vector3(0, groundRayHeight - groundRayLenght, 0), groundRayRadius);

        }
    }

    public void AddDirectionalForce(Vector3 v,float f)
    {
        rb.AddForce(v*f);
    }


}
