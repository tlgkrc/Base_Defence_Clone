using System;
using System.Collections.Generic;

namespace ExampleAIStateMachine.Scripts
{

    public class AIStateMachine
    {
        #region Self Variables

        #region Private Variables

        private IAIState _currentState;

        private Dictionary<Type, List<Transition>> _transitions = new Dictionary<Type, List<Transition>>();
        private List<Transition> _currentTransitions = new List<Transition>();
        private List<Transition> _anyTransitions = new List<Transition>();

        private static List<Transition> _emptyTransitions = new List<Transition>();

        #endregion

        #endregion

        private class Transition
        {
            public Func<bool> Condition { get; }
            public IAIState To { get; }

            public Transition(IAIState to, Func<bool> condition)
            {
                Condition = condition;
                To = to;
            }
        }

        private Transition GetTransition()
        {
            foreach (var transition in _anyTransitions)
                if (transition.Condition())
                    return transition;

            foreach (var transition in _currentTransitions)
                if (transition.Condition())
                    return transition;

            return null;
        }
        
        public void AddTransition(IAIState from, IAIState to, Func<bool> predicate)
        {
            if (_transitions.TryGetValue(from.GetType(), out var transitions) == false)
            {
                transitions = new List<Transition>();
                _transitions[from.GetType()] = transitions;
            }

            transitions.Add(new Transition(to, predicate));
        }

        public void AddAnyTransition(IAIState state, Func<bool> predicate)
        {
            _anyTransitions.Add(new Transition(state, predicate));
        }

        public void SetState(IAIState state)
        {
            if (state == _currentState)
                return;

            _currentState?.OnExit();
            _currentState = state;

            _transitions.TryGetValue(_currentState.GetType(), out _currentTransitions);
            if (_currentTransitions == null)
            {
                _currentTransitions = _emptyTransitions;
            }

            _currentState.OnEnter();
        }

        public void Tick()
        {
            var transition = GetTransition();
            if (transition != null)
                SetState(transition.To);

            _currentState?.Tick();
        }
    }
}