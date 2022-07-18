using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StateMachineCore
{
    public class State<TState, TSignal, TContext>
    {
        public TState Value { get; private set; }
        public StateMachine<TState, TSignal, TContext> Machine { get; private set; }
        public StateChangeEventHandleAsync<TState, TSignal, TContext> EntryEventAsyncHandler { get; set; }
        public StateChangeEventHandleAsync<TState, TSignal, TContext> ExitEventAsyncHandler { get; set; }
        public State(StateMachine<TState, TSignal, TContext> machine, TState value)
        {
            Value = value;
            Machine = machine;
        }
    }
}
