using System;
using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.Android;
using UnityEngine.Animations;

public class MovementBrain : MachineCore
{
    [Header("setup stuff")]
    public State startState;
    [SerializeField] Rigidbody rb;
    public Animator animator;
    public RaycastHit hit;
    public RaycastHit wallHit;

    [Header("Raycast setup stuff")]
    [SerializeField] public float groundRayLenght;
    [SerializeField] private float groundRayHeightOffset;
    [SerializeField] private float groundRayRadius;
    [SerializeField] private LayerMask groundLayer;


    [Header("Universal Movement Data")]
    [SerializeField] public Vector3 moveVector;
    [SerializeField] public bool groundCheck { get; private set; }
    [SerializeField] public bool wantJump;
    [SerializeField] GameObject visualPart;
    [SerializeField] Vector3 visualPartDefault;
    [SerializeField] public float playerHeightMultiplier;
    [SerializeField] public float moveMultiplier;
    [SerializeField] public bool runWant;
    public bool runeWant;
    [SerializeField] public bool crouchWant;

    [SerializeField] public bool climbWant;

    // Start is called before the first frame update
    void Awake()
    {
        visualPartDefault = visualPart.transform.localPosition;
        SetupInstances();
        sMachine.Set(startState, sMachine);
    }

    // Update is called once per frame
    void Update()
    {
        currentState.DoBranch();
    }

    private void FixedUpdate()
    {
        groundCheck = Grounded();
        currentState.FixedDoBranch();
    }

    public bool Grounded()
    {
        return Physics.SphereCast(transform.position + new Vector3(0, groundRayHeightOffset + groundRayRadius, 0), groundRayRadius, Vector3.down, out hit, groundRayLenght, groundLayer);
    }

    public RaycastHit RayCheck(Vector3 ofset, Vector3 checkDir,float checkLength,float checkradius)
    {
        RaycastHit hitt;
        Debug.DrawRay(ofset+transform.position + new Vector3(0, groundRayHeightOffset + checkradius, 0),checkDir*checkLength,Color.red,5f);
        Physics.SphereCast(ofset+transform.position + new Vector3(0, groundRayHeightOffset + checkradius, 0), checkradius, checkDir, out hitt, checkLength, groundLayer);
        return hitt;
    }

    public void Bounce(float _hoverHeight, float _spring, float _damp)
    {

        Vector3 vel = rb.velocity;
        //Vector3 dir = transform.TransformDirection(Vector3.down);
        Vector3 dir = Vector3.down;
        float dirVel = Vector3.Dot(dir, vel);
        float x = hit.distance - _hoverHeight * playerHeightMultiplier;
        float springForce = (x * _spring) - (dirVel * _damp);

        rb.AddForce(dir * springForce);
    }

    public void SpecialBounce(Vector3 bdir,float _hoverHeight, float _spring, float _damp)
    {
        Vector3 vel = rb.velocity;
        //Vector3 dir = transform.TransformDirection(Vector3.down);
        Vector3 dir = bdir;
        float dirVel = Vector3.Dot(dir, vel);
        float x = wallHit.distance - _hoverHeight * playerHeightMultiplier;
        float springForce = (x * _spring) - (dirVel * _damp);

        rb.AddForce(dir * springForce);
    }

    public void FallAccelerate(float _maxFallSpeed, float _gravityMult)
    {
        if (rb.velocity.y > -_maxFallSpeed)
        {
            rb.velocity -= Vector3.down * Physics.gravity.y * Time.fixedDeltaTime * _gravityMult;
        }
    }

    public void ClearMove()
    {
        wantJump = false;
        moveVector = Vector3.zero;
        sMachine.Set(startState, sMachine);
    }

    public void SetMoveVector(Vector3 moveInput)
    {
        moveVector = new Vector3(moveInput.x, 0, moveInput.z).normalized;
    }

    public void MoveCharacter(float _speed, float _drag)
    {
        Vector3 velo = rb.velocity;
        velo = new Vector3(velo.x, 0f, velo.z);

        //APPLY FORCES
        rb.AddForce(((moveVector * _speed * moveMultiplier) - velo) * _drag / Time.fixedDeltaTime);
    }

    public void VerticalMoveCharacter(Vector3 AxisM,float _speed, float _drag)
    {
        Vector3 velo = rb.velocity;
        //velo = new Vector3(velo.x, velo.y, 0);

        //APPLY FORCES
        //Vector3 rebindVector3 = new Vector3(moveVector.x,moveVector.z,0);
        //Vector3 rebindRight = Vector3.Cross(AxisM,Vector3.up).normalized;
       //Vector3 rebindFront = Vector3.Cross(rebindRight, AxisM).normalized;
        //Vector3 rebindVector3 = moveVector.x * rebindRight + moveVector.z *rebindFront;

    
        //Vector3 mve = moveVector;
        //Vector3 mvee = new Vector3(1f,0f,0f);
        Vector3 mov = Vector3.Cross(AxisM, Vector3.up);
        mov = mov*-Input.GetAxis("Horizontal");
        mov += new Vector3(0,Input.GetAxis("Vertical"),0);
        //Vector3 planeee = Vector3.ProjectOnPlane(mvee,AxisM).normalized;
        
        //Debug.DrawRay(transform.position,planeee,Color.black,1f);
        rb.AddForce(((mov * _speed * moveMultiplier) - velo) * _drag / Time.fixedDeltaTime);
    }

    public void ForceMoveCharacter(Vector3 fdir,float _speed, float _drag)
    {
        Vector3 velo = rb.velocity;
        velo = new Vector3(velo.x, 0f, velo.z);

        //APPLY FORCES
        rb.AddForce(((fdir * _speed * moveMultiplier) - velo) * _drag / Time.fixedDeltaTime);
    }

    public void SetGravity(bool g)
    {
        rb.useGravity = g;
    }

    public void AddDirectionalForce(Vector3 v, float f)
    {
        rb.AddForce(v * f);
    }

    public void AddDirectionalImpulseForce(Vector3 v, float f)
    {
        rb.AddForce(v * f, ForceMode.Impulse);
    }

    public void ClearVerticalVelo()
    {
        rb.velocity = new Vector3(rb.velocity.x, 0, rb.velocity.z);
    }



    public void RotateTowardsVector(Vector3 _vect, float _speed)
    {
        visualPart.transform.rotation = Quaternion.Lerp(visualPart.transform.rotation, Quaternion.Euler(0, (float)Math.Atan2(_vect.x, _vect.z) * Mathf.Rad2Deg, 0), _speed * Time.deltaTime);
    }

    public void SetBodyHeight(float h)
    {
        playerHeightMultiplier = h;
        SetVisualHeight(new Vector3(0,1-h,0));
    }

    //cursed workaround | DONT USE THIS SHIT UNLES YOUR NAME IS ŁUKASZ
    public void ForceMasterState(State forceSt)
    {
        sMachine.Set(forceSt, sMachine);
    }

    public void SetVisualHeight(Vector3 _offset)
    {
        visualPart.transform.localPosition = visualPartDefault + _offset;
    }


    public void SetStunn(float StunnTime)
    {
        
    }

    //CLIMBING STUFF plz help  help help help help help 
    public bool CheckForViableClimb(float cl)
    {
        Debug.DrawRay(transform.position,Vector3.up,Color.blue,5f);
        Debug.DrawRay(transform.position,visualPart.transform.rotation * Vector3.forward,Color.green,5f);
       
        wallHit = RayCheck(new Vector3(0,0,0),visualPart.transform.rotation * Vector3.forward,cl,0.1f);
        Debug.DrawRay(wallHit.point,Vector3.up,Color.yellow,5f);
        if(wallHit.transform == null)
        {
            return false;
        }
        return true;
    }

    public bool GetCrouchInput()
    {
        return crouchWant;
    }
    public bool GetRunInput()
    {
        return runWant;
    }

    public bool GetClimbInput()
    {
        return climbWant;
    }
public bool GetRuneInput()
    {
        return runeWant;
    }
    
    

    // GIZMOS
    private void OnDrawGizmos()
    {
        Debug.DrawRay(transform.position + new Vector3(0, groundRayHeightOffset, 0), Vector2.down * groundRayLenght, Color.red);
        if (Physics.SphereCast(transform.position + new Vector3(0, groundRayHeightOffset + groundRayRadius, 0), groundRayRadius, Vector3.down, out hit, groundRayLenght, groundLayer))
        {
            Gizmos.color = Color.yellow;
            Gizmos.DrawWireSphere(hit.point, groundRayRadius);
        }
        else
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(transform.position + new Vector3(0, groundRayHeightOffset - groundRayLenght, 0), groundRayRadius);

        }
    }
}
