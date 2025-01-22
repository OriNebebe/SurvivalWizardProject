using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum GeneralState{grounded,anything,falling,reset};
public enum Substate{none,jumping,interacting,running,crouching,sliding};
public class MovementCore : MonoBehaviour
{
// Start is called before the first frame update
    //[SerializeField] PlayerInputManager inputHandler;
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
    [SerializeField] GeneralState currentState;
    [SerializeField] Substate currentSubState;

    //state stuff
    //[SerializeField] private bool grounded;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        Cursor.lockState = CursorLockMode.Locked;
    }
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        //Debug.Log(inputHandler.MoveInput);



    }
    private void FixedUpdate()
    {
        if (Grounded())
        {
            //rounded = true;
            if(currentState!=GeneralState.anything)
            {
                currentState=GeneralState.grounded;
            }

        }else
        {
            if(currentState!=GeneralState.anything)
            {
                currentState=GeneralState.falling;
            }
        }
  
        switch(currentSubState)
        {
            case Substate.none:
            InDefault();
            break;
            case Substate.jumping:
            InJump();
            break;
            case Substate.interacting:
            InInteraction();
            //DoAnythingElse();
            break;
        }

        switch(currentState)
        {
            case GeneralState.grounded:
            DoOnGround();
            break;
            case GeneralState.falling:
            DoOnFall();
            break;
            case GeneralState.anything:
            DoAnythingElse();
            break;
        }

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

    void DoAnythingElse()
    {
        if(currentSubState == Substate.none)
        {
            currentState = GeneralState.reset;
        }//rb.AddForce(Vector3.up*20);
    }

    void DoOnFall()
    {
        if(rb.velocity.y >maxFallSpeed)
        {
            rb.velocity -= Vector3.down * Physics.gravity.y * Time.fixedDeltaTime * gravityMult;
        }
    }

    void InDefault()
    {
        Debug.Log("Doing nothing");
    }

    void InJump()
    {
        rb.AddForce(Vector3.up*40);
        if(jumpTimer>0)
        {
            currentState = GeneralState.anything;
            jumpTimer-=Time.deltaTime;
        }else
        {
            currentState=GeneralState.reset;
            currentSubState = Substate.none;
            jumpTimer=minJumpTime;

        }
    }

    void InInteraction()
    {

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

    /*
    moveVector = movementInput.ReadValue<Vector2>();
        moveVector = new Vector3(moveVector.x,0,moveVector.y);
        moveVector = Quaternion.AngleAxis(cam.eulerAngles.y, Vector3.up) * moveVector;

        Vector3 velo = rb.velocity;
        velo = new Vector3(velo.x, 0f, velo.z);

        //APPLY FORCES
        rb.AddForce(((moveVector * finalSpeed) - velo) * finalDrag / Time.fixedDeltaTime);

*/

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
}
