using UnityEngine;

public class SheepIdleState : BaseState
{
    private readonly SheepMechanic _sheepMechanic;
    public SheepIdleState(SheepMechanic sheepMechanic, SheepStateMachine sheepStateMachine, string stateName) :
        base(sheepStateMachine, stateName)
    {
        _sheepMechanic = sheepMechanic;
    }
    public override void Enter()
    {
        _sheepMechanic.AnimalAnimationController.ChangeAnimation(GetStateName());
    }

    public override void Tick(float deltaTime)
    {
    }

    public override void Exit()
    {
        //BaseStateMachine.ChangeState(_sheepMechanic.SheepMoveState);
    }

    
}