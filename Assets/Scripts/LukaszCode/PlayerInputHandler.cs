using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using System;
using System.Runtime.InteropServices;

public enum SPlayer { one = 0, two = 1 };
public class PlayerInputHandler : MonoBehaviour
{
    [SerializeField] public PlayerInputManager pInput;
    [SerializeField] public SPlayer selectedPlayer;
    [SerializeField] public CameraSwitch camSwitch;
    [SerializeField] PlayerCore[] player;
    [SerializeField] Transform mainCam;
    float cameraRotation;

    public void HandleSwitch()
    {
        switch (selectedPlayer)
        {
            case SPlayer.one:
                if(camSwitch.SwitchCamera(player[(int)SPlayer.two].GetCamera()))
                {
                    player[(int)selectedPlayer].GetMovementC().ClearMove();
                    selectedPlayer = SPlayer.two;
                    player[(int)selectedPlayer].GetMovementC().SetStunn(camSwitch.SwitchTime);
                }
                break;
            case SPlayer.two:             
                if(camSwitch.SwitchCamera(player[(int)SPlayer.one].GetCamera()))
                {
                    player[(int)selectedPlayer].GetMovementC().ClearMove();
                    selectedPlayer = SPlayer.one;
                    player[(int)selectedPlayer].GetMovementC().SetStunn(camSwitch.SwitchTime);
                }

                break;
        }
        //camSwitch.SwitchCamera(player[(int)selectedPlayer].getCamera());
    }
    private void Update()
    {
        GetCameraRotation();
        
        HandleMovement(pInput.MoveInput);
        
        HandleJump();

        HandleRandomInputBullshit();
    }

    public void HandleMovement(Vector2 dir)
    {
        Vector3 moveVector = new Vector3(dir.x, 0, dir.y);
        //Debug.Log(moveVector);
        moveVector = Quaternion.AngleAxis(cameraRotation, Vector3.up) * moveVector;
        player[(int)selectedPlayer].GetMovementC().SetMoveVector(moveVector);
    }

    public void HandleJump()
    {
       
 
        player[(int)selectedPlayer].GetMovementC().wantJump = pInput.JumpInput;
        
        
    }

    public void HandleRandomInputBullshit()
    {
        player[(int)selectedPlayer].GetMovementC().runWant = pInput.RunInput;
        player[(int)selectedPlayer].GetMovementC().crouchWant = pInput.CrouchInput;
        player[(int)selectedPlayer].GetMovementC().climbWant = pInput.ClimbInput;
        player[(int)selectedPlayer].GetMovementC().runeWant = pInput.RuneInput;
    }

    public void GetCameraRotation()
    {
        cameraRotation = mainCam.eulerAngles.y;
    }

    public SPlayer SwitchPlayerFrom(SPlayer _player)
    {
        return _player;
    }

    
}
