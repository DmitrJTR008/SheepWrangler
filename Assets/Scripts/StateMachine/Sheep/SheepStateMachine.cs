
using StateMachine;

public class SheepStateMachine : BaseStateMachine
{
    private SheepMechanic _sheep;
    private IStateMachine _machine;

    public void InitMachine(SheepMechanic sheep, IState idleState)
    {
        _sheep = sheep;
        ChangeState(idleState);
    }
    
    
}
