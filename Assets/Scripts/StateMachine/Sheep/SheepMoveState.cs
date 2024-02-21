
    using UnityEngine;

    public class SheepMoveState : BaseState
    {
        private readonly SheepMechanic _sheepMechanic;

        public SheepMoveState(SheepMechanic sheepMechanic, SheepStateMachine sheepStateMachine, string stateName) :
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
            _sheepMechanic.Move();
        }

        public override void Exit()
        {
            //BaseStateMachine.ChangeState(_sheepMechanic.SheepIdleState);
        }
    }
