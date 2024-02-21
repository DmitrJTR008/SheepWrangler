namespace StateMachine
{
    public interface IStateMachine
    {
        public void ChangeState(IState targetState);
    }
}