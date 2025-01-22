using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class SimpleBodyState : MoveStateOverride
{

    [Header("stats... ")]
    [SerializeField] private float heightMultiplier;
    [SerializeField] private float speedMultiplier;
    [SerializeField] private float injuryTreshold;
    bool ctrlClick = false;
    bool shiftClick = false;

    bool climbClick = false;

    bool RuneClick = false;
    //[SerializeField] private bool switching;

    [Header("Same Layer states")]
    public State DoAfterCtrl;
    public State DoAfterOnShift;
    public State DoOnRune;

    public State DoOnClimb;

    [Header("ChildrenStates")]
    public State DefaultState;

    public string animName;
    public override void Enter()
    {
        //brain.playerHeightMultiplier = heightMultiplier;
        brain.SetBodyHeight(heightMultiplier);

        brain.moveMultiplier = speedMultiplier;
        SetChild(DefaultState);
        //brain.SetHeight(new Vector3(0, height, 0));
        ctrlClick = false;
        shiftClick = false;
        
        brain.animator.SetFloat("Blend",0);
        brain.animator.SetBool(animName,true);
        brain.animator.SetTrigger(animName);
    }
    public override void Do()
    {
        //Debug.Log("i am grounded yo");
        //Change(DoAfter);

            //skibidi




        if (ctrlClick)
        {
            if (brain.GetRunInput())
            {
                ctrlClick = false;
                Change(DoAfterCtrl);
                //switching = false;
                //Change(DoAfter);
            }
        }else
        {
            if(!brain.GetRunInput())
            {
                //Debug.Log("Released");
                ctrlClick = true;
            }
        }




        //yo wtf is that shit 
        if (shiftClick)
        {
            if (brain.GetCrouchInput())
            {
                shiftClick = false;
                Change(DoAfterOnShift);
                //switching = false;
                //Change(DoAfter);
            }
        }else
        {
            if(!brain.GetCrouchInput())
            {
                //Debug.Log("Released");
                shiftClick = true;
            }
        }


        if (RuneClick)
        {
            if (brain.GetRuneInput())
            {
                RuneClick = false;
                Change(DoOnRune);
                //switching = false;
                //Change(DoAfter);
            }
        }else
        {
            if(!brain.GetRuneInput())
            {
                //Debug.Log("Released");
                RuneClick = true;
            }
        }

        if(climbClick)
        {
            if(brain.GetClimbInput())
            {
                climbClick = false;
                if(DoOnClimb!= null)
                {
                    if(brain.CheckForViableClimb(2))
                    {
                        brain.ForceMasterState(DoOnClimb);

                    }
                }
            }
            //skibidi
            /*
            if(JumpState!= null)
            {brain.ForceMasterState(JumpState);}
            */
        }else
        {
            if(!brain.GetClimbInput())
            {
                ///Debug.Log("Released");
                climbClick = true;
            }
        }

        if(Input.GetKeyDown(KeyCode.Q))
        {Debug.Log("Skibo dibo di");}

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
        brain.animator.SetBool(animName,false);
        brain.playerHeightMultiplier = 1;
        brain.moveMultiplier = 1;
    }
}
