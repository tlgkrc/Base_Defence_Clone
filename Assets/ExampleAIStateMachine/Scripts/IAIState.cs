namespace ExampleAIStateMachine.Scripts
{
    public interface IAIState
    {
        void Tick();
        void OnEnter();
        void OnExit();
    }
}