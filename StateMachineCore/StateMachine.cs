using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StateMachineCore
{
    public class StateMachine<TState, TSignal, TContext>
    {
        private Dictionary<TState, State<TState, TSignal, TContext>> _states = new Dictionary<TState, State<TState, TSignal, TContext>>();
        private Dictionary<TSignal, Signal<TState, TSignal, TContext>> _signals = new Dictionary<TSignal, Signal<TState, TSignal, TContext>>();
        private Dictionary<TState, Dictionary<TSignal, StateRoute<TState, TSignal, TContext>>> _routes = new Dictionary<TState, Dictionary<TSignal, StateRoute<TState, TSignal, TContext>>>();
        public StateMachine(TState state, TContext context)
        {
            State = CreateState(state);
            Context = context;
        }
        public State<TState, TSignal, TContext> State { get; private set; }
        public TContext Context { get; private set; }
        public State<TState, TSignal, TContext> CreateState(TState stateValue)
        {
            if (_states.ContainsKey(stateValue)) return _states[stateValue];
            var state = new State<TState, TSignal, TContext>(this, stateValue);
            this._states.Add(stateValue, state);
            return state;
        }
        public Signal<TState, TSignal, TContext> CreateSignal(TSignal signalValue)
        {
            if (_signals.ContainsKey(signalValue)) return _signals[signalValue];
            var signal = new Signal<TState, TSignal, TContext>(this, signalValue);
            this._signals.Add(signalValue, signal);
            return signal;
        }
        public void CreateRoute(TState fromValue, TSignal signalValue, TState toValue, StateChangeEventHandleAsync<TState, TSignal, TContext> handle = null)
        {
            if (!_states.ContainsKey(fromValue)) CreateState(fromValue);
            if (!_states.ContainsKey(toValue)) CreateState(toValue);
            if (!_signals.ContainsKey(signalValue)) CreateSignal(signalValue);
            if (!_routes.ContainsKey(fromValue)) _routes.Add(fromValue, new Dictionary<TSignal, StateRoute<TState, TSignal, TContext>>());
            var signalRoutes = _routes[fromValue];
            if (signalRoutes.ContainsKey(signalValue))
            {
                signalRoutes[signalValue] = new StateRoute<TState, TSignal, TContext>(_states[fromValue], _signals[signalValue], _states[toValue], handle);
            }
            else
            {
                signalRoutes.Add(signalValue, new StateRoute<TState, TSignal, TContext>(_states[fromValue], _signals[signalValue], _states[toValue], handle));
            }
        }
        public StateMachine<TState, TSignal, TContext> Config(TState state, Action<StateBuilder<TState, TSignal, TContext>> action)
        {
            var builder = new StateBuilder<TState, TSignal, TContext>(this, state);
            action(builder);
            return this;
        }
        public async Task<bool> ChangeAsync(TSignal signalValue)
        {
            if (!_routes.ContainsKey(State.Value)) return false;
            if (!_routes[State.Value].ContainsKey(signalValue)) return false;
            var route = _routes[State.Value][signalValue];
            var e = new StateChangeEvent<TState, TSignal, TContext>(
                    Context,
                    route.From.Value,
                    route.Signal.Value,
                    route.To.Value,
                    this
            );
            var exit = route?.From?.ExitEventAsyncHandler;
            if(exit!=null)
            {
               await exit(e);
            }
            var action = route?.ActionAsync;
            if (action != null)
            {
                await action(e);
            }
            this.State = route.To;
            var entry = route?.To?.EntryEventAsyncHandler;
            if (entry != null)
            {
                await entry(e);
            }
            return true;
        }
    }
}
