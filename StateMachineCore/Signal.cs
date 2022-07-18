using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StateMachineCore
{
    public class Signal<TState, TSignal, TContext>
    {
        public TSignal Value { get; private set; }
        public StateMachine<TState, TSignal, TContext> Machine { get; private set; }
        public Signal(StateMachine<TState, TSignal, TContext> machine, TSignal value)
        {
            Value = value;
            Machine = machine; ;
        }
    }
}
