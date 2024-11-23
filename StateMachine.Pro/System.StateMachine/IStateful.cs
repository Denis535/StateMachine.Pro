namespace System.StateMachine;
using System;
using System.Collections.Generic;
using System.Text;

public interface IStateful<T> where T : StateBase<T> {

    // State
    protected T? State { get; }

    // SetState
    protected internal void SetState(T? state, object? argument = null);

    // Helpers
    protected static void SetState(IStateful<T> stateful, Action<IStateful<T>, T?> stateSetter, T? state, object? argument) {
        if (stateful.State != null) {
            var prevState = stateful.State;
            prevState.Detach( stateful, argument );
            stateSetter( stateful, null );
            prevState.DisposeWhenRemove( argument );
        }
        if (state != null) {
            stateSetter( stateful, state );
            state.Attach( stateful, argument );
        }
    }

}
