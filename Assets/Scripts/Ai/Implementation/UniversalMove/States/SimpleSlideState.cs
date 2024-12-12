using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleSlideState : MoveStateOverride
{
    public float slideTime;
    public float heightMultiplier;
    public float speedMultiplier;
    public string animName;
    public State DoAfter;
    public CapsuleCollider onec;
    public BoxCollider twoc;
    public override void Enter()
    {
        onec.enabled = false;
        twoc.enabled = true;

        brain.playerHeightMultiplier = heightMultiplier;
        brain.moveMultiplier = speedMultiplier;
    }
    public override void Do()
    {
        if (time >= slideTime)
        {
            isComplete = true;

            Change(DoAfter);
            //Debug.Log("waitStop");
        }
    }
    public override void FixedDo()
    {
        brain.SetMoveVector(new Vector3(0,0,0));
        
    }
    public override void Exit()
    {
        onec.enabled = true;
        twoc.enabled = false;

        brain.playerHeightMultiplier = 1;
        brain.moveMultiplier = 1;
    }
    }
