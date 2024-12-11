
public class StateFactory
{
    MovementCoreV2 _context;
    public StateFactory(MovementCoreV2 cContext)
    {
        _context = cContext;
    }

    public PlayerBasestate Idle()
    {
        return new PlayerIdleState(_context, this);
    }
    public PlayerBasestate Fall()
    {
        return new PlayerFallingState(_context, this);
    }
    public PlayerBasestate Grounded()
    {
        return new PlayerGroundedState(_context, this);
    }
    public PlayerBasestate Jump()
    {
        return new PlayerJumpingState(_context, this);
    }
}
