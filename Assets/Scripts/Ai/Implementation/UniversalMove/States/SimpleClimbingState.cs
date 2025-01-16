using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleClimbingState : MoveStateOverride
{
    public Transform pos;
    public float vectorRotationSpeed;
    public string animName;
    public float veSpeed;
    public float veDrag;
     [Header("Same layer state")]
    public State DoAfter;
    public State Jump;
     [Header("ChildrenStates")]
    public State DefaultState;
    public override void Enter()
    {
        brain.SetGravity(false);
        brain.animator.SetTrigger(animName);
        //SetChild(DefaultState);
    }
    public override void Do()
    {
        //Debug.Log("i am falling");
        //Change(DoAfter);
        if(brain.GetCrouchInput()||brain.GetRunInput())
        {
            Change(DoAfter);
        }

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

        if(brain.CheckForViableClimb(1))
        {
            Debug.DrawRay(brain.wallHit.point + new Vector3(0,0.5f,0.5f),brain.wallHit.normal.normalized,Color.green,10f);
            brain.RotateTowardsVector(-brain.wallHit.normal.normalized,vectorRotationSpeed);
            brain.SpecialBounce(-brain.wallHit.normal.normalized,0.5f,200,40);
            brain.VerticalMoveCharacter(-brain.wallHit.normal.normalized,veSpeed,veDrag);

            if(brain.RayCheck(pos.localPosition,-brain.wallHit.normal, 2,0.1f).transform== null)
            {
                brain.AddDirectionalForce(Vector3.up,100);
                Change(Jump);
                //Debug.Log("HeadEmpty");
            }else
            {
                //Debug.Log("Stick");
            }

        }else
        {
            Change(DoAfter);
        }
        //Debug.Log("Climbin");
    }
    public override void Exit()
    {

    }
}
