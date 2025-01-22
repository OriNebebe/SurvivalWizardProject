using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleRunState : MoveStateOverride
{

    [Header("stats... ")]
    [SerializeField] private float heightMultiplier;
    [SerializeField] private float speedMultiplier;
    [SerializeField] private float injuryTreshold;
    bool ctrlClick = false;

    [SerializeField] String animName;
    //[SerializeField] private bool switching;

    [Header("Same Layer states")]
    public State DoAfterCtrl;
    public State DoAfterOnShift;

    [Header("ChildrenStates")]
    public State DefaultState;


    public override void Enter()
    {
        brain.playerHeightMultiplier = heightMultiplier;
        brain.moveMultiplier = speedMultiplier;
        SetChild(DefaultState);
        //brain.SetHeight(new Vector3(0, height, 0));
        ctrlClick = false;

        brain.animator.ResetTrigger(animName);
        brain.animator.SetFloat("Blend",1);
        brain.animator.SetTrigger(animName);
    }
    public override void Do()
    {
        //Debug.Log("i am grounded yo");
        //Change(DoAfter);

        if (ctrlClick)
        {
            if (brain.GetCrouchInput())
            {
                ctrlClick = false;
                Change(DoAfterOnShift);
                //switching = false;
                //Change(DoAfter);
            }
        }else
        {
            if(!brain.GetCrouchInput())
            {
                Debug.Log("Released");
                ctrlClick = true;
            }
        }
        if(!brain.GetRunInput())
        {
            Change(DoAfterCtrl);
        }


        /*if(brain.wantJump)
        {
            Change(JumpState);
        }*/


    }
    public override void FixedDo()
    {
        if(brain.moveVector != Vector3.zero)
        {
            brain.animator.SetBool("Input",true);
        }else
        {
            brain.animator.SetBool("Input",false);
        }
        //brain.Bounce(hoverHeight,spring,damp);
    }
    public override void Exit()
    {
        brain.playerHeightMultiplier = 1;
        brain.moveMultiplier = 1;
    }
}

