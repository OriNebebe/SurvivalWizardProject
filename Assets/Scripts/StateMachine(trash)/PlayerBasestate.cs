using System;

public abstract class PlayerBasestate
{
    protected bool _isRootState = false;
    protected MovementCoreV2 _ctx;
    protected StateFactory _factory;
    protected PlayerBasestate _currentSubState;
    protected PlayerBasestate _currentSuperState;
    public PlayerBasestate(MovementCoreV2 curentContext,StateFactory playerStatefactory)
    {
        _ctx = curentContext;
        _factory = playerStatefactory;
    }

    public abstract void EnterState();
    public abstract void UpdateState();
    public abstract void FixedUpdateState();
    public abstract void ExitState();
    public abstract void CheckSwitchState();
    public abstract void InitializeSubState();
   
    public void UpdateStates()
    {
        UpdateState();
        if(_currentSubState !=null)
        {
            _currentSubState.UpdateState();
        }
    }

    public void FixedUpdateStates()
    {
        FixedUpdateState();
        if(_currentSubState !=null)
        {
            _currentSubState.FixedUpdateState();
        }
    }

    public void ExitStates()
    {
        ExitState();
        if(_currentSubState !=null)
        {
            _currentSubState.ExitState();
        }
    }

    protected void SwitchState(PlayerBasestate newState)
    {
        ExitState();
        newState.EnterState();

        if(_isRootState)
        {
            _ctx.CurrentState = newState;
        } else if(_currentSuperState !=null)
        {
            _currentSubState.SetSubState(newState);
        }

        
    }
    protected void SetSuperState(PlayerBasestate newSuperState)
    {
        _currentSuperState = newSuperState;
    }
    protected void SetSubState(PlayerBasestate newSubState)
    {
        _currentSubState = newSubState;
        newSubState.SetSuperState(this);
    }
}
