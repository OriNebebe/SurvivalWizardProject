using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInputManager : MonoBehaviour
{
    [SerializeField] private PlayerInputHandler inputHandler;

    // action references
    [SerializeField] private InputActionReference _moveAction;
    [SerializeField] private InputActionReference _lookAction;
    [SerializeField] private InputActionReference _jumpAction;
    [SerializeField] private InputActionReference _switchAction;
    [SerializeField] private InputActionReference _crouchAction;
    [SerializeField] private InputActionReference _runAction;

    [SerializeField] private InputActionReference _climbAction;
    [SerializeField] private InputActionReference _interactAction;
    [SerializeField] private InputActionReference _runeAction;// i think this is redundant (like runes should be activated by interacting with item, idk tho)
    [SerializeField] private InputActionReference _dropAction;
    //[SerializeField] private InputActionReference _cancelAction;
    
    //actions
    private InputAction moveAction;
    private InputAction lookAction;
    private InputAction jumpAction;
    private InputAction switchAction;
    private InputAction crouchAction;
    private InputAction runAction;
    private InputAction climbAction;
    private InputAction interactAction;
    private InputAction runeAction;
    private InputAction dropAction;

    //accual variables that are used
    public Vector2 MoveInput { get; private set; }
    public Vector2 LookInput { get; private set; }
    public bool JumpInput { get; private set; }
    public bool SwitchInput { get; private set; }
    public bool  CrouchInput { get; private set; }
    public bool  RunInput { get; private set; }
    public bool  ClimbInput { get; private set; }
    public bool  InteractInput { get; private set; }
    public bool  RuneInput { get; private set; }
    public bool  DropInput { get; private set; }

    //idk. singleton or some shit.
    //public static PlayerInputManager Instance { get; private set; }

    private void Awake()
    {
        Cursor.lockState = CursorLockMode.Locked;
        // set up action maps or smth. i forgor
        moveAction = _moveAction;
        lookAction = _lookAction;
        jumpAction = _jumpAction;
        switchAction = _switchAction;
        crouchAction = _crouchAction;
        runAction = _runAction;
        climbAction = _climbAction;
        interactAction = _interactAction;
        runeAction = _runeAction;
        dropAction = _dropAction;
        
    }

    //                  plz make sure to enable and disable all actions. plz dont forget u idiot
    private void OnEnable()
    {

        moveAction.Enable();
        lookAction.Enable();
        jumpAction.Enable();
        switchAction.Enable();
        crouchAction.Enable();
        runAction.Enable();

        climbAction.Enable();
        interactAction.Enable();
        runeAction.Enable();
        dropAction.Enable();

        RegisterActions();
    }

    private void OnDisable() {
        moveAction.Disable();
        lookAction.Disable();
        jumpAction.Disable();
        switchAction.Disable();
        crouchAction.Disable();
        runAction.Disable();

        climbAction.Disable();
        interactAction.Disable();
        runeAction.Disable();
        dropAction.Disable();

    }

    // registering all actions...  here asign scripts activated by buttons
    public void RegisterActions()
    {   //what happens on pressing and canceling
        //moveAction.performed += context => MoveInput = context.ReadValue<Vector2>();
        //moveAction.canceled += context => MoveInput = Vector2.zero;

        moveAction.performed += context => MoveInput = context.ReadValue<Vector2>();
        moveAction.canceled += context => MoveInput = Vector2.zero;

        //moveAction.performed += context => inputHandler.HandleMovement(MoveInput);
        //inputHandler.HandleMovement(MoveInput);

        lookAction.performed += context => LookInput = context.ReadValue<Vector2>();
        lookAction.canceled += context => LookInput = Vector2.zero;

        jumpAction.performed += context => JumpInput = true;
        jumpAction.canceled += context => JumpInput = false;

        switchAction.started += context => inputHandler.HandleSwitch();


        crouchAction.performed += context => CrouchInput = true;
        crouchAction.canceled += context => CrouchInput = false;

        
        runAction.performed += context => RunInput = true;
        runAction.canceled += context => RunInput = false;

        
        
        climbAction.performed += context => ClimbInput = true;
        climbAction.canceled += context => ClimbInput = false;

        runeAction.performed += context => RuneInput = true;
        runeAction.canceled += context => RuneInput = false;
        //switchAction.canceled += context => SwitchInput = false;
    }
}




/* most basic version


using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInputManager : MonoBehaviour
{

    //[SerializeField] private InputActionAsset playerControll;

    // action references
    [SerializeField] private InputActionReference _moveAction;
    [SerializeField] private InputActionReference _lookAction;
    [SerializeField] private InputActionReference _jumpAction;
    [SerializeField] private InputActionReference _switchAction;

    //actions
    private InputAction moveAction;
    private InputAction lookAction;
    private InputAction jumpAction;
    private InputAction switchAction;


    //accual variables that are used
    public Vector2 MoveInput { get; private set; }
    public Vector2 LookInput { get; private set; }
    public bool JumpInput { get; private set; }
    public bool SwitchInput { get; private set; }

    //idk. singleton or some shit.
    //public static PlayerInputManager Instance { get; private set; }

    private void Awake()
    {

        // instance stuff, dont ask
        /*
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }*/
/*
        // set up action maps or smth. i forgor
        moveAction = _moveAction;
        lookAction = _lookAction;
        jumpAction = _jumpAction;
        switchAction = _switchAction;

    }

    //                  plz make sure to enable and disable all actions. plz dont forget u idiot
    private void OnEnable()
    {

        moveAction.Enable();
        lookAction.Enable();
        jumpAction.Enable();
        switchAction.Enable();

        RegisterActions();
    }

    private void OnDisable() {
        moveAction.Disable();
        lookAction.Disable();
        jumpAction.Disable();
        switchAction.Disable();
    }

    // registering all actions... 
    public void RegisterActions()
    {   //what happens on pressing and canceling
        moveAction.performed += context => MoveInput = context.ReadValue<Vector2>();
        moveAction.canceled += context => MoveInput = Vector2.zero;

        lookAction.performed += context => LookInput = context.ReadValue<Vector2>();
        lookAction.canceled += context => LookInput = Vector2.zero;

        jumpAction.performed += context => JumpInput = true;
        jumpAction.canceled += context => JumpInput = false;

        switchAction.started += context => SwitchInput = true;
        switchAction.canceled += context => SwitchInput = false;
    }
}

*/