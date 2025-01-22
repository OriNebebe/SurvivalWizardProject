using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleStunnState : MoveStateOverride
{
    public float waitTime;
    public string animName;
    public State DoAfter;

    public override void Enter()
    {

    }
    public override void Do()
    {
        if (time >= waitTime)
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

    }
    }
