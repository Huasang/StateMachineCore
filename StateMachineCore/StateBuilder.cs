using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StateMachineCore
{
    public class StateBuilder<TState, TSignal, TContext>
    {
        private TState _Value;
        private StateMachine<TState, TSignal, TContext> _machine;
        private State<TState, TSignal, TContext> _State;
        public StateBuilder(StateMachine<TState, TSignal, TContext> machine, TState value)
        {
            this._machine = machine;
            this._Value = value;
            _State = _machine.CreateState(_Value);
        }
        public StateBuilder<TState, TSignal, TContext> OnEntry(StateChangeEventHandleAsync<TState, TSignal, TContext> handle)
        {
            _State.EntryEventAsyncHandler = handle;
            return this;
        }
        public StateBuilder<TState, TSignal, TContext> OnExit(StateChangeEventHandleAsync<TState, TSignal, TContext> handle)
        {
            _State.ExitEventAsyncHandler = handle;
            return this;
        }
        public StateBuilder<TState, TSignal, TContext> AddRoute(TSignal signal, TState toState, StateChangeEventHandleAsync<TState, TSignal, TContext> handle = null)
        {
            _machine.CreateRoute(_Value, signal, toState, handle);
            return this;
        }
    }
}
