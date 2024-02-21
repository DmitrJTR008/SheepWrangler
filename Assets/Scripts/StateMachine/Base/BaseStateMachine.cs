
using StateMachine;

public abstract class BaseStateMachine : IStateMachine
{
    private IState _currentState;
    public void ChangeState(IState targetState)
    {
        _currentState?.Exit();
        _currentState = targetState;
        _currentState?.Enter();
    }

    public void Tick(float time)
    {
        _currentState?.Tick(time);
    }
    
}
