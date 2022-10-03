namespace AI.States
{
    public interface IAIStates
    {
        void Tick();
        void OnEnter();
        void OnExit();
    }
}