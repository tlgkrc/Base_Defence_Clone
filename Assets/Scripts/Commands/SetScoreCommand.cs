using Signals;

namespace Commands
{
    public class SetScoreCommand
    {
        #region Private Variables

        private int _score;

        #endregion
        public SetScoreCommand(ref int score)
        {
            _score = score;
        }
        
        public void Execute(int value)
        {
            _score = value;
            UISignals.Instance.onSetScoreText?.Invoke(_score);
        }
    }
}