using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DebugPlayerTester : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] PlayerInputManager inputHandler;
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
        Debug.Log(inputHandler.MoveInput);



    }
    private void FixedUpdate()
    {

        if (Physics.SphereCast(transform.position + new Vector3(0, groundRayHeight + groundRayRadius, 0), groundRayRadius, Vector3.down, out hit, groundRayLenght, groundLayer))
        {
            //rounded = true;
            Bounce();
        }
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
