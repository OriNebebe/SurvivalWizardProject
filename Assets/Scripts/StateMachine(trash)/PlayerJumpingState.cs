using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerJumpingState : PlayerBasestate
{
    public PlayerJumpingState(MovementCoreV2 curentContext,StateFactory stateFactory)
    :base (curentContext,stateFactory){}
     public override void EnterState()
     {
        Debug.Log("jumping");
        _ctx.AddDirectionalForce(Vector3.up,50);
     }
    public override void UpdateState(){}
    public override void FixedUpdateState()
    {
        CheckSwitchState();
    }    
    public override void ExitState(){}
    public override void CheckSwitchState(){}
    public override void InitializeSubState(){}
}
