using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StateMachineCore
{
    public delegate Task StateChangeEventHandleAsync<TState, TAction, TContext>(StateChangeEvent<TState, TAction, TContext> e);
}
