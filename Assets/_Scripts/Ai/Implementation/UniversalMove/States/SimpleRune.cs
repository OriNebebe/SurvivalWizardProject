using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleRune : MoveStateOverride
{
    
    [Header("Jump stats")]
    
    [SerializeField] private float RuneTime;
    [SerializeField] private float RuneTimer;
    
    public GameObject RuneMaker;
    public Vector3 RuneOffset;
    [Header("Same Layer states")]
    public State DoAfter;
    [Header("children State")]
    public State DefaultState;
    public override void Enter()
    {
        brain.ClearVerticalVelo();
       // Debug.Log("i am jumpingg");
        RuneTimer = RuneTime;
        
        brain.SetGravity(true);
        //brain.AddDirectionalImpulseForce(Vector3.up,jumpForce);
        //SetChild(DefaultState);
        
        brain.animator.SetBool("Rune",true);
        //brain.animator.SetTrigger("Jump");
        //SetChild(DefaultState);
    }
    public override void Do()
    {
        //Debug.Log("i goung up");
        //Change(DoAfter);
        
        RuneTimer-=Time.deltaTime;
        
        
        if(RuneTimer<=0)
        {
            Destroy(Instantiate(RuneMaker,transform.position+RuneOffset,transform.rotation),10f);
            Change(DoAfter);
        }

    }
    public override void FixedDo()
    {
        brain.SetMoveVector(new Vector3(0,0,0));
        brain.MoveCharacter(1,0.5f);
    }
    public override void Exit()
    {
brain.animator.SetBool("Rune",false);
    }
}
