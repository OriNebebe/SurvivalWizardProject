using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerIdleState : PlayerBasestate
{
    public PlayerIdleState(MovementCoreV2 curentContext,StateFactory stateFactory)
    :base (curentContext,stateFactory){}
     public override void EnterState(){}
    public override void UpdateState(){}
    public override void FixedUpdateState()
    {
        CheckSwitchState();
    }    
    public override void ExitState(){}
    public override void CheckSwitchState(){}
    public override void InitializeSubState(){}
}
