using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StateMachineCore
{
    public class StateChangeEvent<TState, TSignal, TContext>
    {
        public StateChangeEvent(TContext context, TState from, TSignal action, TState to, StateMachine<TState, TSignal, TContext> stateMachine)
        {
            Context = context;
            From = from;
            Action = action;
            To = to;
            StateMachine = stateMachine ?? throw new ArgumentNullException(nameof(stateMachine));
        }
        public TContext Context { get; private set; }
        public TState From { get; private set; }
        public TSignal Action { get; private set; }
        public TState To { get; private set; }
        public StateMachine<TState, TSignal, TContext> StateMachine { get; private set; }
    }
}
