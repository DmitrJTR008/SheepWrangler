
public interface IState
{
     string GetStateName();
     void Enter();
     void Tick(float deltaTime);
     void Exit();
}
