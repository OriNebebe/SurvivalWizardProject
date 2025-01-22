using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerControllerTest : MonoBehaviour
{
/*
    private InputMaster playerInput;
    private InputAction movementInput;

    public Vector3 moveVector;
    private Vector3 finalMoveVector;

    Rigidbody rb;
    RaycastHit hit;

    public Transform cam;
    public LayerMask groundLayer;

    //set up
    [Header("Set up stuff")]
    public float playerH;
    public float hoverH;
    public float spring;
    public float damp;

    //speed stuff
    [Header("set speed values")]
    public float finalSpeed;
    public float walkSpeed;
    public float runSpeed;
    public float airSpeed;

    //drags 
    [Header("set drag values")]
    public float finalDrag;
    public float stopDrag;
    public float moveDrag;
    public float airMoveDrag;
    public float airDrag;
    public float gravityMult;

    //jump Stuff
    [Header("jumpp stuff")]
    public float jumpForce;
    public float jumpMinTime;
    public float jumpMaxTime;
    public float jumpTimer;
    

    //bools
    public bool grounded, hasMoveInput, sprintHeld, jumpHeld, inJump;
    
   
    private void Awake()
    {
        
        playerInput = new InputMaster();
    }

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        Cursor.lockState = CursorLockMode.Locked;
    }

    private void OnEnable()
    {
        playerInput.Player.Enable();
        movementInput = playerInput.Player.Move;

        //VOIDS    
        playerInput.Player.Move.started += ctx => HandleMoveStart();
        playerInput.Player.Move.canceled += ctx => HandleMoveEnd();

        playerInput.Player.Sprint.started += ctx => sprintHeld = true;
        playerInput.Player.Sprint.canceled += ctx => sprintHeld = false;
        //playerInput.Player.Move.performed += x => speed = 10;

        //jump
        playerInput.Player.Jump.started += ctx => handleJumpStart();
        playerInput.Player.Jump.canceled += ctx => jumpHeld = false;

    }

    private void OnDisable()
    {
        playerInput.Player.Disable();

        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (!hasMoveInput)
        {
            Debug.DrawRay(transform.position,Vector3.up, Color.red,1f);
        }

        if (inJump)
        {
            Debug.DrawRay(transform.position, Vector3.right, Color.blue, 5f);
        }

        // input and move direction
        moveVector = movementInput.ReadValue<Vector2>();
        moveVector = new Vector3(moveVector.x,0,moveVector.y);
        moveVector = Quaternion.AngleAxis(cam.eulerAngles.y, Vector3.up) * moveVector;

        //GROUND CHECK
        if (Physics.SphereCast(transform.position + new Vector3(0, 0.45f/2, 0),0.45f, Vector3.down, out hit, playerH, groundLayer))
        {
            
            grounded = true;
            rb.useGravity = false;

            if (!inJump)
            {
                //BOUNCE
                Bounce();
            }

            if (!hasMoveInput)
            {
                if (finalDrag == airDrag || finalDrag == airMoveDrag * 2)
                {
                    finalDrag = airMoveDrag * 4;
                }
                else
                {
                    finalDrag = stopDrag;
                }
                
            }
        }
        else
        {
            grounded = false;

            if (!inJump)
            {


                rb.useGravity = true;

                if (rb.velocity.y > -20)
                {
                    rb.velocity -= Vector3.down * Physics.gravity.y * Time.fixedDeltaTime * gravityMult;
                }
                

                /*if (rb.velocity.y < 0f && rb.velocity.y <= -30)
                {
                    rb.velocity -= Vector3.down * Physics.gravity.y * Time.fixedDeltaTime * gravityMult;
                } */
           /* }

            if (!hasMoveInput)
            {
                finalDrag = airDrag;
            }
        }

        //getVel
        Vector3 velo = rb.velocity;
        velo = new Vector3(velo.x, 0f, velo.z);

        //APPLY FORCES
        rb.AddForce(((moveVector * finalSpeed) - velo) * finalDrag / Time.fixedDeltaTime);


    }

    void Bounce()
    {
        //Debug.DrawLine(transform.position, transform.position + Vector3.down * playerHoverHeight, Color.red);
        Vector3 vel = rb.velocity;
        //Vector3 dir = transform.TransformDirection(Vector3.down);

        Vector3 dir = Vector3.down;

        float dirVel = Vector3.Dot(dir, vel);

        float x = hit.distance - hoverH;

        float springForce = (x * spring) - (dirVel * damp);

        rb.AddForce(dir * springForce);
    }


    void handleJumpStart()
    {
        if (!inJump && !jumpHeld)
        {
            jumpHeld = true;
            
            StartCoroutine(Jump());
        }
        
       
    }

    IEnumerator Jump()
    {
        if (grounded)
        {
            inJump = true;
            jumpTimer = 0;
        }

        while ((!jumpHeld && jumpTimer <= jumpMinTime) || (jumpHeld && jumpTimer <= jumpMaxTime))
        {

            

            jumpTimer += Time.fixedDeltaTime;

            rb.AddForce(((Vector3.up * jumpForce) -  new Vector3(0,rb.velocity.y,0 ) ) * rb.mass / Time.fixedDeltaTime);
            //rb.AddForce(Vector3.up * jumpForce);

            yield return new WaitForFixedUpdate();
        }

        inJump = false;
       
    }

    

    //  MOVE STUFF

    void HandleMoveStart()
    {
        hasMoveInput = true;
        StartCoroutine(MoveControll());
    }

    IEnumerator MoveControll()
    {
        while (hasMoveInput)
        {
            if (grounded)
            {
                if (sprintHeld)
                {
                    finalSpeed = runSpeed;
                }
                else
                {
                    finalSpeed = walkSpeed;
                }
                
                finalDrag = moveDrag;
            }
            else
            {
                finalSpeed = airSpeed;
                finalDrag = airMoveDrag;
            }
            yield return new WaitForFixedUpdate();
        }

        Debug.Log("END");
            
    }

    void HandleMoveEnd()
    {
        hasMoveInput = false;
    }

    // NO MORE MOVE STUFF
*/}
