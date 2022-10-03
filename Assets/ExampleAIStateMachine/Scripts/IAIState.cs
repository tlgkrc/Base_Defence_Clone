namespace ExampleAIStateMachine.Scripts
{
    //done
    public interface IAIState
    {
        void Tick();
        void OnEnter();
        void OnExit();
    }
}