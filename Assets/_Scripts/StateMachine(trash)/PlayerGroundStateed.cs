using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerGroundedState : PlayerBasestate
{
    //constructor (can make stuff on initialization, dont forget)
    public PlayerGroundedState(MovementCoreV2 curentContext,StateFactory stateFactory)
    :base (curentContext,stateFactory)
    {
        _isRootState = true;
        InitializeSubState();
    }
    public override void EnterState()
    {

    }
    public override void UpdateState() { }
    public override void FixedUpdateState() 
    { 
        CheckSwitchState();
    }
    public override void ExitState() { }
    public override void CheckSwitchState() 
    { 
        if(_ctx.jumpNeed)
        {
            SwitchState(_factory.Jump());
        }
    }
    public override void InitializeSubState() 
    {
        //setSubstate(_factory.state)
    }

}
