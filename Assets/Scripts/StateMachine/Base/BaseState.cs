using StateMachine;

public abstract class BaseState : IState
{
    private readonly string _stateName;
    protected readonly IStateMachine BaseStateMachine;
    protected BaseState(IStateMachine machine,string stateName)
    {
        _stateName = stateName;
        BaseStateMachine = machine;
    }

    public string GetStateName()
    {
        return _stateName;
    }
    
    public abstract void Enter();
    public abstract void Tick(float deltaTime);
    public abstract void Exit();
}