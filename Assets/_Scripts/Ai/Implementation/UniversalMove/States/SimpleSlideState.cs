using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleSlideState : MoveStateOverride
{
    public float slideTime;
    public float heightMultiplier;
    public float speedMultiplier;
    public float slideSpeed,slidedrag;
    public string animName;
    public State DoAfter;
    public CapsuleCollider onec;
    public CapsuleCollider twoc;

    public Vector3 sampleMoveVector;
    public override void Enter()
    {
        onec.enabled = false;
        twoc.enabled = true;

        sampleMoveVector = brain.moveVector.normalized;
        
        brain.animator.SetBool(animName,true);

        //brain.playerHeightMultiplier = heightMultiplier;
        brain.SetBodyHeight(heightMultiplier);

        brain.moveMultiplier = speedMultiplier;

        if(sampleMoveVector == new Vector3(0,0,0))
        {
            Change(DoAfter);
        }
    }
    public override void Do()
    {
        
        if(brain.RayCheck(new Vector3(0,0,0),Vector3.up,1.5f,0.1f).transform != null)
        {
            //Debug.Log("hitting");
        }else
        {
            //Debug.Log("not hittin");
        }
        //Debug.DrawRay(brain.RayCheck(Vector3.up,1f,0.5f).distance ,Vector3.down);

        if (time >= slideTime && brain.RayCheck(new Vector3(0,0,0),Vector3.up,1.5f,0.1f).transform == null)
        {
            isComplete = true;

            Change(DoAfter);
            //Debug.Log("waitStop");
        }
    }
    public override void FixedDo()
    {
        // brain.SetMoveVector(sampleMoveVector);
        brain.ForceMoveCharacter(sampleMoveVector,slideSpeed,slidedrag);
    }
    public override void Exit()
    {
        brain.animator.SetBool(animName,false);
        onec.enabled = true;
        twoc.enabled = false;

        brain.playerHeightMultiplier = 1;
        brain.moveMultiplier = 1;
    }


    



    }



