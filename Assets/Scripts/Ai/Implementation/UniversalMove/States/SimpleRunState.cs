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
    }
    public override void Do()
    {
        //Debug.Log("i am grounded yo");
        //Change(DoAfter);

        if (ctrlClick)
        {
            if (brain.GetShiftInput())
            {
                ctrlClick = false;
                Change(DoAfterOnShift);
                //switching = false;
                //Change(DoAfter);
            }
        }else
        {
            if(!brain.GetShiftInput())
            {
                Debug.Log("Released");
                ctrlClick = true;
            }
        }
        if(!brain.GetCtrInput())
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
        //brain.Bounce(hoverHeight,spring,damp);
    }
    public override void Exit()
    {
        brain.playerHeightMultiplier = 1;
        brain.moveMultiplier = 1;
    }
}

