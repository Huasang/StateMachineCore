using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StateMachineCore
{
    public class StateRoute<TState, TSignal, TContext>
    {
        public StateRoute(State<TState, TSignal, TContext> from, Signal<TState, TSignal, TContext> signal, State<TState, TSignal, TContext> to, StateChangeEventHandleAsync<TState, TSignal, TContext> actionAsync)
        {
            From = from;
            Signal = signal;
            To = to;
            ActionAsync = actionAsync;
        }
        public State<TState, TSignal, TContext> From { get; private set; }
        public Signal<TState, TSignal, TContext> Signal { get; private set; }
        public State<TState, TSignal, TContext> To { get; private set; }
        public StateChangeEventHandleAsync<TState, TSignal, TContext> ActionAsync { get; set; }
    }
}
