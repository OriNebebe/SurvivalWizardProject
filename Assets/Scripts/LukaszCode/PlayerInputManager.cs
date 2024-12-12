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
    [SerializeField] private InputActionReference _shiftAction;
    [SerializeField] private InputActionReference _ctrlAction;

    //actions
    private InputAction moveAction;
    private InputAction lookAction;
    private InputAction jumpAction;
    private InputAction switchAction;

    private InputAction shiftAction;
    private InputAction ctrlAction;


    //accual variables that are used
    public Vector2 MoveInput { get; private set; }
    public Vector2 LookInput { get; private set; }
    public bool JumpInput { get; private set; }
    public bool SwitchInput { get; private set; }
    public bool  ShiftInput { get; private set; }
    public bool  CtrlInput { get; private set; }

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
        shiftAction = _shiftAction;
        ctrlAction = _ctrlAction;
    }

    //                  plz make sure to enable and disable all actions. plz dont forget u idiot
    private void OnEnable()
    {

        moveAction.Enable();
        lookAction.Enable();
        jumpAction.Enable();
        switchAction.Enable();
        shiftAction.Enable();
        ctrlAction.Enable();

        RegisterActions();
    }

    private void OnDisable() {
        moveAction.Disable();
        lookAction.Disable();
        jumpAction.Disable();
        switchAction.Disable();
        shiftAction.Disable();
        ctrlAction.Disable();
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


        shiftAction.performed += context => ShiftInput = true;
        shiftAction.canceled += context => ShiftInput = false;

        ctrlAction.performed += context => CtrlInput = true;
        ctrlAction.canceled += context => CtrlInput = false;
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